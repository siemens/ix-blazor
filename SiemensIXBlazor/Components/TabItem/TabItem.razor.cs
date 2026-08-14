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
using SiemensIXBlazor.Objects.Tabs;
using System.Text.Json;

namespace SiemensIXBlazor.Components.TabItem
{
    /// <summary>
    /// Siemens IX Tab Item component for individual tab content
    /// </summary>
    public partial class TabItem : IXBaseComponent
    {
        [Parameter] public string? Id { get; set; }
        [Parameter, EditorRequired] public string TabKey { get; set; } = string.Empty;
        [Parameter] public string? Icon { get; set; }
        [Parameter] public bool Disabled { get; set; } = false;
        [Parameter] public bool Selected { get; set; } = false;
        [Parameter] public int? Counter { get; set; }
        [Parameter] public bool Closable { get; set; } = false;
        [Parameter] public string? Label { get; set; }
        [Parameter] public string AriaLabelCloseButton { get; set; } = "Close tab";
        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] public EventCallback<TabClickDetail> TabClickEvent { get; set; }
        [Parameter] public EventCallback<TabClickDetail> TabCloseEvent { get; set; }

        private readonly string _generatedId = $"tab-item-{Guid.NewGuid():N}";
        private BaseInterop? _interop;

        private string EffectiveId => string.IsNullOrWhiteSpace(Id) ? _generatedId : Id;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, EffectiveId, "tabClick", "TabClicked");
                await _interop.AddEventListener(this, EffectiveId, "tabClose", "TabClosed");
            }
        }

        [JSInvokable]
        public async Task TabClicked(JsonElement data)
        {
            await TabClickEvent.InvokeAsync(DeserializeDetail(data));
        }

        [JSInvokable]
        public async Task TabClosed(JsonElement data)
        {
            await TabCloseEvent.InvokeAsync(DeserializeDetail(data));
        }

        private static TabClickDetail DeserializeDetail(JsonElement data)
        {
            return JsonSerializer.Deserialize<TabClickDetail>(data.GetRawText())
                ?? new TabClickDetail();
        }
    }
}
