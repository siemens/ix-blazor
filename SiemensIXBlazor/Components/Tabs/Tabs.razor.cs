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
        public string? AriaLabelChevronLeftIconButton { get; set; }
        [Parameter]
        public string? AriaLabelChevronRightIconButton { get; set; }    
        [Parameter]
        public bool Rounded { get; set; } = false;
        [Parameter]
        public int Selected { get; set; } = 0;
        [Parameter]
        public bool Small { get; set; } = false;
        [Parameter]
        public EventCallback<int> SelectedChangeEvent { get; set; }

        TabsInterop _tabsInterop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _tabsInterop = new(JSRuntime);

                await _tabsInterop.SubscribeEvents(this, Id, "selectedChange", "SelectedChange");
            }
        }

        [JSInvokable]
        public void SelectedChange(int value)
        {
            SelectedChangeEvent.InvokeAsync(value);
        }
    }
}
