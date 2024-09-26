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

        public BaseInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", $"./_content/Siemens.IX.Blazor/js/interops/baseJsInterop.js").AsTask());
        }

        public async Task AddEventListener(object classObject, string id, string eventName, string callbackFunctionName)
        {
            var module = await moduleTask.Value;
            await module.InvokeAsync<string>("listenEvent", DotNetObjectReference.Create(classObject), id, eventName, callbackFunctionName);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
