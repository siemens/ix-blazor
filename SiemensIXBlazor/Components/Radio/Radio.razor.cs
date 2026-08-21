using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;
using System.Text.Json;

namespace SiemensIXBlazor.Components.Radio
{
    public partial class Radio
    {
        private BaseInterop? _interop;

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        [Parameter]
        public bool Checked { get; set; } = false;

        [Parameter]
        public bool Disabled { get; set; } = false;

        [Parameter]
        public string? Label { get; set; }

        [Parameter]
        public string? Name { get; set; }

        [Parameter]
        public bool Required { get; set; } = false;

        [Parameter]
        public string? Value { get; set; }

        [Parameter]
        public string? CssClass { get; set; }

        [Parameter]
        public EventCallback<bool> CheckedChangeEvent { get; set; }

        [Parameter]
        public EventCallback IxBlurEvent {  get; set; }

        [Parameter]
        public EventCallback<string> ValueChangeEvent { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop ??= new(JSRuntime);
                await _interop.AddEventListener(this, Id, "checkedChange", "CheckedChange");
                await _interop.AddEventListener(this, Id, "ixBlur", "IxBlur", includeDetail: false);
                await _interop.AddEventListener(this, Id, "valueChange", "ValueChange");
            }
        }

        [JSInvokable]
        public async Task CheckedChange(JsonElement checkedState)
        {
            bool newChecked = checkedState.GetBoolean();
            Checked = newChecked;
            await CheckedChangeEvent.InvokeAsync(newChecked);
            StateHasChanged();
        }

        [JSInvokable]
        public async Task IxBlur()
        {
            await IxBlurEvent.InvokeAsync();
        }

        [JSInvokable]
        public async Task ValueChange(JsonElement valueState)
        {
            string newValue = valueState.GetString();
            Value = newValue;
            await ValueChangeEvent.InvokeAsync(newValue);
            StateHasChanged();
        }
    }
}
