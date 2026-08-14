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
using SiemensIXBlazor.Objects.Breadcrumb;
using System.Text.Json;

namespace SiemensIXBlazor.Components
{
    public partial class Breadcrumb
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public EventCallback<BreadcrumbClick> ItemClicked { get; set; }
        [Parameter]
        public EventCallback<BreadcrumbNextClick> NextItemClicked { get; set; }
        [Parameter]
        public bool Subtle { get; set; } = false;
        [Parameter]
        public bool EnableTopLayer { get; set; } = false;
        [Parameter]
        public string AriaLabelPreviousButton { get; set; } = "Show previous breadcrumb items";
        [Parameter]
        public BreadcrumbClick[] NextItems { get; set; } = Array.Empty<BreadcrumbClick>();
        [Parameter]
        public int VisibleItemCount { get; set; } = 9;

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "itemClick", "BreadcrumbItemClicked");
                await _interop.AddEventListener(this, Id, "nextClick", "BreadcrumbNextItemClicked");

                if (NextItems != null && NextItems.Length > 0)
                {
                    await _interop.SetElementProperty(Id, "nextItems", NextItems);
                }
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if (_interop != null && NextItems != null)
            {
                await _interop.SetElementProperty(Id, "nextItems", NextItems);
            }
        }

        [JSInvokable]
        public async Task BreadcrumbItemClicked(JsonElement item)
        {
            var breadcrumbClick = item.Deserialize<BreadcrumbClick>();
            if (breadcrumbClick is not null)
            {
                await ItemClicked.InvokeAsync(breadcrumbClick);
            }
        }

        [JSInvokable]
        public async Task BreadcrumbNextItemClicked(JsonElement item)
        {
            var nextClick = item.Deserialize<BreadcrumbNextClick>();
            if (nextClick is not null)
            {
                await NextItemClicked.InvokeAsync(nextClick);
            }
        }
    }
}
