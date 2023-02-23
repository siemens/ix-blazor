using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Components.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class ExpandingSearch
    {
        [Parameter]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string Icon { get; set; } = "search";
        [Parameter]
        public string Placeholder { get; set; } = "Enter text here";
        [Parameter]
        public string Value { get; set; } = string.Empty;
        [Parameter]
        public EventCallback<string> ValueChangedEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "valueChange", "ValueChanged");
            }
        }

        [JSInvokable]
        public async void ValueChanged(string value)
        {
            Value = value;
            await ValueChangedEvent.InvokeAsync(value);
        }
    }
}
