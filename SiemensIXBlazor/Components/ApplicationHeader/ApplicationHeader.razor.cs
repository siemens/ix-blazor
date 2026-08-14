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


namespace SiemensIXBlazor.Components
{
    public partial class ApplicationHeader
    {
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
        private BaseInterop? _interop;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public RenderFragment? Secondary { get; set; }

        [Parameter]
        public RenderFragment? Overflow { get; set; }

        [Parameter]
        public RenderFragment? Logo { get; set; }

        [Parameter]
        public RenderFragment? Avatar { get; set; }

        [Parameter]
        public string? Name { get; set; }

        [Parameter]
        public string? NameSuffix { get; set; }

        [Parameter]
        public string? CompanyLogo { get; set; }

        [Parameter]
        public string? CompanyLogoAlt { get; set; }

        [Parameter]
        public string? AppIcon { get; set; }

        [Parameter]
        public string? AppIconAlt { get; set; }

        [Parameter]
        public bool AppIconOutline { get; set; } = false;

        [Parameter]
        public bool HideBottomBorder { get; set; } = false;

        [Parameter]
        public bool ShowMenu { get; set; } = false;

        [Parameter]
        public string? AriaLabelAppSwitchIconButton { get; set; }

        [Parameter]
        public string? AriaLabelMoreMenuIconButton { get; set; }

        [Parameter]
        public bool EnableTopLayer { get; set; } = false;

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        [Parameter]
        public EventCallback OpenAppSwitchEvent { get; set; }

        [Parameter]
        public EventCallback<bool> MenuToggleEvent { get; set; }



        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "openAppSwitch", "OpenAppSwitch");
                await _interop.AddEventListener(this, Id, "menuToggle", "MenuToggle");
            }
        }

        [JSInvokable]
        public async Task OpenAppSwitch()
        {
            await OpenAppSwitchEvent.InvokeAsync();
        }

        [JSInvokable]
        public async Task MenuToggle(bool expanded)
        {
            await MenuToggleEvent.InvokeAsync(expanded);
        }
}
}
