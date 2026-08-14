// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Enums.Tabs;
using SiemensIXBlazor.Helpers;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class Tabs
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public TabsLayout Layout { get; set; } = TabsLayout.Auto;
        [Parameter]
        public TabsPlacement Placement { get; set; } = TabsPlacement.Bottom;
        [Parameter]
        public bool Rounded { get; set; } = false;
        [Parameter]
        public string? ActiveTabKey { get; set; }
        [Parameter]
        public string AriaLabelMoreTabs { get; set; } = "Show all tabs";
        [Parameter]
        public TabsKeyboardNavigation KeyboardNavigation { get; set; } = TabsKeyboardNavigation.Automatic;
        [Parameter]
        public bool Small { get; set; } = false;
        [Parameter]
        public EventCallback<string?> TabChangedEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "tabChange", "TabChanged");
            }
        }

        [JSInvokable]
        public async Task TabChanged(string? value)
        {
            await TabChangedEvent.InvokeAsync(value);
        }
    }
}
