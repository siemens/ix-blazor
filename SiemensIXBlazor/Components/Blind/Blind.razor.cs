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
using SiemensIXBlazor.Enums.Blind;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class Blind
    {
        [Parameter]
        public string? Label { get; set; }
        [Parameter]
        public string? SubLabel { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public bool Collapsed { get; set; } = false;
        [Parameter]
        public string? Icon { get; set; }
        [Parameter]
        public BlindVariant Variant { get; set; } = BlindVariant.insight;
        [Parameter]
        public EventCallback<bool> CollapsedChangedEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "collapsedChange", "CollapsedChanged");
            }
        }

        [JSInvokable]
        public async Task CollapsedChanged(bool value)
        {
            await CollapsedChangedEvent.InvokeAsync(value);
        }
    }
}
