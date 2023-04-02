using System;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Components.Interops;

namespace SiemensIXBlazor.Components
{
	public partial class Select
	{
		[Parameter, EditorRequired]
		public string Id { get; set; } = string.Empty;
        [Parameter]
        public string Class { get; set; } = string.Empty;
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
		public string Mode { get; set; } = "single";
		[Parameter]
		public bool Readonly { get; set; } = false;
		[Parameter]
		public dynamic? SelectedIndices { get; set; }
		[Parameter]
		public EventCallback<string> AddItemEvent { get; set; }
        [Parameter]
        public EventCallback<dynamic> ItemSelectionChangeEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "addItem", "AddItemChanged");
                await _interop.AddEventListener(this, Id, "itemSelectionChange", "ItemSelectionChanged");
            }
        }

        [JSInvokable]
		public async void AddItemChanged(string label)
		{
			await AddItemEvent.InvokeAsync(label);
		}

        [JSInvokable]
        public async void ItemSelectionChanged(dynamic labels)
        {
			if(labels is string)
			{
                await ItemSelectionChangeEvent.InvokeAsync(labels);
            }
			else if(labels is JsonElement)
			{
				JsonElement jsonText = labels;
				string[] labelArray = jsonText.Deserialize<string[]>()!;
                await ItemSelectionChangeEvent.InvokeAsync(labelArray);
            }
            
        }
    }
}

