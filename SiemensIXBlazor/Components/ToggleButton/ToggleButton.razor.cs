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
using SiemensIXBlazor.Enums.ToggleButton;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components.ToggleButton
{
    public partial class ToggleButton
    {
        [Parameter, EditorRequired]
        public string Id { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public string? Icon { get; set; }
        [Parameter]
        public string? IconRight { get; set; }
        [Parameter]
        public bool Loading { get; set; } = false;
        [Parameter]
        public bool Pressed { get; set; } = false;
        [Parameter]
        public ToggleButtonVariant Variant { get; set; } = ToggleButtonVariant.subtle_primary;
        [Parameter]
        public EventCallback<bool> PressedChangeEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "pressedChange", "PressedChange");
            }
        }

        [JSInvokable]
        public async Task PressedChange(bool value)
        {
            await PressedChangeEvent.InvokeAsync(value);
        }
    }
}
