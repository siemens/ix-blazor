// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;
using System.Text.Json;

namespace SiemensIXBlazor.Components
{
    public partial class SelectItem
	{
		[Parameter, EditorRequired]
		public string Id { get; set; } = string.Empty;
		[Parameter]
		public string? Label { get; set; }
		[Parameter]
		public bool Selected { get; set; } = false;
		[Parameter]
		public bool Disabled { get; set; } = false;
		[Parameter]
		public string? Value { get; set; }
		[Parameter]
		public EventCallback<string> ItemClickEvent { get; set; }

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
        public async Task ItemClicked(JsonElement label)
        {
            var value = label.ValueKind == JsonValueKind.String
                ? label.GetString()
                : label.TryGetProperty("value", out var property) && property.ValueKind == JsonValueKind.String
                    ? property.GetString()
                    : string.Empty;
            await ItemClickEvent.InvokeAsync(value ?? string.Empty);
        }
    }
}
