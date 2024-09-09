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
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components.Menu
{
    public partial class Menu
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string ApplicationDescription { get; set; } = string.Empty;
        [Parameter]
        public string? ApplicationName { get; set; }
        [Parameter]
        public bool EnableMapExpand { get; set; } = false;
        [Parameter]
        public bool EnableSettings { get; set; } = false;
        [Parameter]
        public bool EnableToggleTheme { get; set; } = false;
        [Parameter]
        public bool Expand { get; set; } = false;
        [Parameter]
        public string I18NCollapse { get; set; } = "Collapse";
        [Parameter]
        public string I18NExpand { get; set; } = "Expand";
        [Parameter]
        public string I18NLegal { get; set; } = "About & legal information";
        [Parameter]
        public string I18NMore { get; set; } = "More...";
        [Parameter]
        public string I18NSettings { get; set; } = "Settings";
        [Parameter]
        public string I18NToggleTheme { get; set; } = "Toggle theme";
        [Parameter]
        public int MaxVisibleMenuItems { get; set; } = 9;
        [Parameter]
        public bool ShowAbout { get; set; } = false;
        [Parameter]
        public bool ShowSettings { get; set; } = false;
        [Parameter]
        public EventCallback<bool> ExpandChangedEvent { get; set; }
        [Parameter]
        public EventCallback<bool> MapExpandChangedEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "expandChange", "ExpandChanged");
                await _interop.AddEventListener(this, Id, "mapExpandChange", "MapExpandChanged");
            }
        }

        [JSInvokable]
        public async Task ExpandChanged(bool value)
        {
            await ExpandChangedEvent.InvokeAsync(value);
        }

        [JSInvokable]
        public async Task MapExpandChanged(bool value)
        {
            await MapExpandChangedEvent.InvokeAsync(value);
        }
    }
}
