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

namespace SiemensIXBlazor.Components.Pagination
{
    public partial class Pagination
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public bool? Advanced { get; set; }
        [Parameter]
        public string? AriaLabelChevronLeftIconButton { get; set; }
        [Parameter]
        public string? AriaLabelChevronRightIconButton { get; set; }    
        [Parameter]
        public int? Count { get; set; }
        [Parameter]
        public string I18nItems { get; set; } = "Items";
        [Parameter]
        public string I18nOf { get; set; } = "of";
        [Parameter]
        public string I18nPage { get; set; } = "Page";
        [Parameter]
        public int ItemCount { get; set; } = 15;
        [Parameter]
        public int SelectedPage { get; set; } = 0;
        [Parameter]
        public bool HideItemCount { get; set; } = false;
        [Parameter]
        public EventCallback<int> ItemCountChangedEvent { get; set; }
        [Parameter]
        public EventCallback<int> PageSelectedEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "itemCountChanged", "ItemCountChanged");
                await _interop.AddEventListener(this, Id, "pageSelected", "PageSelected");
            }
        }

        [JSInvokable]
        public async Task ItemCountChanged(int count)
        {
            await ItemCountChangedEvent.InvokeAsync(count);
        }

        [JSInvokable]
        public async Task PageSelected(int page)
        {
            await PageSelectedEvent.InvokeAsync(page);
        }
    }
}
