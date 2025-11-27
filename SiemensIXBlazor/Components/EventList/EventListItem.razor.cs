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
using SiemensIXBlazor.Enums.EventList;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class EventListItem
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public bool? Chevron { get; set; }
        [Parameter]
        public string? ItemColor { get; set; }
        [Parameter]
        public bool? Disabled { get; set; }
        [Parameter]
        public int Opacity { get; set; } = 1;
        [Parameter]
        public bool? Selected { get; set; }
        [Parameter]
        public EventListVariant Variant { get; set; } = EventListVariant.outline;

        [Parameter]
        public EventCallback ItemClickEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "itemClick", "ItemClicked");
            }
        }

        [JSInvokable]
        public async void ItemClicked()
        {
            await ItemClickEvent.InvokeAsync();
        }
    }
}
