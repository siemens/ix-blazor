// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;
using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Components.MenuSettings
{
    public partial class MenuSettings
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string? ActiveTabKey { get; set; }
        [Parameter]
        public string AriaLabelCloseButton { get; set; } = "Close Settings";
        [Parameter]
        public string Label { get; set; } = "Settings";
        [Parameter]
        public bool SuppressLegacyTabs { get; set; } = false;
        [Parameter]
        public EventCallback<MenuCloseEvent> ClosedEvent { get; set; }
        [Parameter]
        public EventCallback<string> TabChangedEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "close", "Closed");

                await _interop.AddEventListener(this, Id, "tabChange", "TabChanged");
            }
        }

        [JSInvokable]
        public async Task Closed(MenuCloseEvent args)
        {
            await ClosedEvent.InvokeAsync(args);
        }

        [JSInvokable]
        public async Task TabChanged(string tabKey)
        {
            await TabChangedEvent.InvokeAsync(tabKey);
        }
    }
}
