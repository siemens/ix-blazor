// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Enums.Select;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class Select
	{
		[Parameter, EditorRequired]
		public string Id { get; set; } = string.Empty;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
		[Parameter]
		public bool AllowClear { get; set; } = false;
		[Parameter]
		public bool Disabled { get; set; } = false;
		[Parameter]
		public bool Editable { get; set; } = false;
		[Parameter]
		public string i18nPlaceholder { get; set; } = "Select an option";
		[Parameter]
		public string i18nPlaceholderEditable { get; set; } = "Type of select option";
		[Parameter]
		public string i18nSelectListHeader { get; set; } = "Please select an option";
		[Parameter]
		public SelectMode Mode { get; set; } = SelectMode.Single;
		[Parameter]
		public bool Readonly { get; set; } = false;
		[Parameter]
		public dynamic? Value { get; set; }
		[Parameter]
		public bool HideListHeader { get; set; } = false;
		[Parameter]
		public string I18NNoMatches { get; set; } = "No matches";
		[Parameter]
		public EventCallback<string> AddItemEvent { get; set; }
        [Parameter]
        public EventCallback<dynamic> ValueChangeEvent { get; set; }
        [Parameter]
        public EventCallback<string> InputChangeEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "addItem", "AddItemChanged");
                await _interop.AddEventListener(this, Id, "valueChange", "ValueChanged");
                await _interop.AddEventListener(this, Id, "inputChange", "InputChanged");
            }
        }

        [JSInvokable]
		public async void AddItemChanged(string label)
		{
			await AddItemEvent.InvokeAsync(label);
		}

        [JSInvokable]
        public async void InputChanged(string input)
        {
            await InputChangeEvent.InvokeAsync(input);
        }

        [JSInvokable]
        public async void ValueChanged(dynamic labels)
        {
			if(labels is string)
			{
                await ValueChangeEvent.InvokeAsync(labels);
            }
			else if(labels is JsonElement)
			{
				JsonElement jsonText = labels;

				if (jsonText.ValueKind == JsonValueKind.Array) {
					string[] labelArray = jsonText.Deserialize<string[]>()!;
					await ValueChangeEvent.InvokeAsync(labelArray);
				} else
				{
                    await ValueChangeEvent.InvokeAsync(jsonText.GetString());
                }
            }
            
        }
    }
}

