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
    internal class FileUploadInterop
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public FileUploadInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", $"./_content/Siemens.IX.Blazor/js/siemens-ix/interops/fileUploadInterop.js").AsTask());
        }

        public async Task AddEventListener(object classObject, string id, string eventName, string callbackFunctionName)
        {
            var module = await moduleTask.Value;
            var objectReference = DotNetObjectReference.Create(classObject);
            await module.InvokeAsync<string>("fileUploadEventHandler", objectReference, id, eventName, callbackFunctionName);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                try
                {
                    var module = await moduleTask.Value;
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
