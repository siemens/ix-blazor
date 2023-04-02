using System;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;

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
        public async void ItemClicked(string label)
        {
            await ItemClickEvent.InvokeAsync(label);
        }
    }
}

