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
    internal class TabsInterop
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public TabsInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", $"./_content/Siemens.IX.Blazor/js/interops/tabsInterop.js").AsTask());
        }

        public async Task InitialComponent(string id)
        {
            var module = await moduleTask.Value;
            await module.InvokeAsync<string>("initalTable", id);
        }

        public async Task SubscribeEvents(object classObject, string id, string eventName, string methodName)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("subscribeEvents", DotNetObjectReference.Create(classObject), id, eventName, methodName);
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
