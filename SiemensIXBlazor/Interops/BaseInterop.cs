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
    public class BaseInterop
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        private readonly List<(IDisposable Reference, string ListenerId)> listeners = [];

        public BaseInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", $"./_content/Siemens.IX.Blazor/js/siemens-ix/interops/baseJsInterop.js").AsTask());
        }

        public async Task AddEventListener(
            object classObject,
            string id,
            string eventName,
            string callbackFunctionName,
            bool includeDetail = true)
        {
            var module = await moduleTask.Value;
            var objectReference = DotNetObjectReference.Create(classObject);
            try
            {
                string listenerId = await module.InvokeAsync<string>(
                    "listenEvent", objectReference, id, eventName, callbackFunctionName, includeDetail);
                listeners.Add((objectReference, listenerId));
            }
            catch
            {
                objectReference.Dispose();
                throw;
            }
        }

        public async Task SetElementProperty(string id, string propertyName, object propertyValue)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("setElementProperty", id, propertyName, propertyValue);
        }

        public async Task<T?> InvokeElementMethodAsync<T>(string id, string methodName)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<T>("invokeElementMethod", id, methodName);
        }

        public async Task InvokeElementMethodAsync(string id, string methodName)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("invokeElementMethod", id, methodName);
        }

        public async ValueTask<T> InvokeMethod<T>(string id, string methodName)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<T>("invokeMethod", id, methodName);
        }

        public async ValueTask InvokeVoidMethod(string id, string methodName)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("invokeVoidMethod", id, methodName);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                try
                {
                    var module = await moduleTask.Value;
                    foreach ((IDisposable reference, string listenerId) in listeners)
                    {
                        try
                        {
                            await module.InvokeVoidAsync("removeEventListener", listenerId);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed to remove event listener: {ex.Message}");
                        }
                        finally
                        {
                            reference.Dispose();
                        }
                    }
                    listeners.Clear();
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
