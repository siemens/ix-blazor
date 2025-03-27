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
using SiemensIXBlazor.Enums.Chip;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class Chip
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public bool Active { get; set; } = true;
        [Parameter]
        public string? Background { get; set; }
        [Parameter]
        public bool Closable { get; set; } = false;
        [Parameter]
        public string? ChipColor { get; set; }
        [Parameter]
        public string? Icon { get; set; }
        [Parameter]
        public bool Outline { get; set; } = false;
        [Parameter]
        public ChipVariant Variant { get; set; } = ChipVariant.primary;
        [Parameter]
        public EventCallback ClosedEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "closeChip", "Closed");
            }
        }

        [JSInvokable]
        public async void Closed()
        {
            await ClosedEvent.InvokeAsync();
        }
    }
}
