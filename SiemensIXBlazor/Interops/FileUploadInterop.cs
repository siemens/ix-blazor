// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.JSInterop;

namespace SiemensIXBlazor.Interops
{
    internal class FileUploadInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        private readonly List<(IDisposable Reference, string ListenerId)> listeners = [];
        private readonly object listenerLock = new();
        private bool disposed;

        public FileUploadInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", $"./_content/Siemens.IX.Blazor/js/siemens-ix/interops/fileUploadInterop.js").AsTask());
        }

        public async Task AddEventListener(object classObject, string id, string eventName, string callbackFunctionName)
        {
            lock (listenerLock)
            {
                ObjectDisposedException.ThrowIf(disposed, this);
            }

            var module = await moduleTask.Value;
            var objectReference = DotNetObjectReference.Create(classObject);
            try
            {
                var listenerId = await module.InvokeAsync<string?>(
                    "fileUploadEventHandler", objectReference, id, eventName, callbackFunctionName);
                if (listenerId is null)
                {
                    objectReference.Dispose();
                    return;
                }

                var disposeImmediately = false;
                lock (listenerLock)
                {
                    if (disposed)
                    {
                        disposeImmediately = true;
                    }
                    else
                    {
                        listeners.Add((objectReference, listenerId));
                    }
                }

                if (disposeImmediately)
                {
                    try
                    {
                        await module.InvokeVoidAsync("removeFileUploadEventHandler", listenerId);
                    }
                    finally
                    {
                        objectReference.Dispose();
                    }

                    return;
                }
            }
            catch
            {
                objectReference.Dispose();
                throw;
            }
        }

        public async Task SetFilesToUpload(string id, object files)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("setFilesToUpload", id, files);
        }

        public async ValueTask DisposeAsync()
        {
            List<(IDisposable Reference, string ListenerId)> listenersToDispose;
            lock (listenerLock)
            {
                if (disposed)
                {
                    return;
                }

                disposed = true;
                listenersToDispose = [.. listeners];
                listeners.Clear();
            }

            if (!moduleTask.IsValueCreated)
            {
                foreach (var (reference, _) in listenersToDispose)
                {
                    reference.Dispose();
                }

                return;
            }

            IJSObjectReference? module = null;
            try
            {
                module = await moduleTask.Value;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to load file upload interop module for disposal: {ex.Message}");
            }

            foreach ((IDisposable reference, string listenerId) in listenersToDispose)
            {
                try
                {
                    if (module is not null)
                    {
                        await module.InvokeVoidAsync("removeFileUploadEventHandler", listenerId);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to remove file upload event listener: {ex.Message}");
                }
                finally
                {
                    reference.Dispose();
                }
            }

            if (module is not null)
            {
                try
                {
                    await module.DisposeAsync();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to dispose module: {ex.Message}");
                }
            }
        }
    }
}
