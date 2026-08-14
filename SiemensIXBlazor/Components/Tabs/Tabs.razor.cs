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
using System.Text.Json;

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
        public bool Rounded { get; set; } = false;
        [Parameter]
        public string? ActiveTabKey { get; set; }
        [Parameter]
        public string AriaLabelMoreTabs { get; set; } = "Show all tabs";
        [Parameter]
        public TabsKeyboardNavigation KeyboardNavigation { get; set; } = TabsKeyboardNavigation.Automatic;
        [Parameter]
        public bool Small { get; set; } = false;
        [Parameter]
        public EventCallback<string?> TabChangeEvent { get; set; }
        [Parameter]
        public EventCallback<string?> TabCloseEvent { get; set; }

        private BaseInterop? _interop;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "tabChange", "TabChanged");
                await _interop.AddEventListener(this, Id, "tabClose", "TabClosed");
            }
        }

        [JSInvokable]
        public async Task TabChanged(string? value)
        {
            ActiveTabKey = value;
            await TabChangeEvent.InvokeAsync(value);
        }

        [JSInvokable]
        public async Task TabClosed(JsonElement data)
        {
            var value = data.ValueKind == JsonValueKind.String
                ? data.GetString()
                : JsonSerializer.Deserialize<SiemensIXBlazor.Objects.Tabs.TabClickDetail>(data.GetRawText())?.TabKey;

            await TabCloseEvent.InvokeAsync(value);
        }
    }
}
