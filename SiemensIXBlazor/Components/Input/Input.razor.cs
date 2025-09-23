using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;
using System.Text.Json;

namespace SiemensIXBlazor.Components.Input
{
    public partial class Input
    {
        private string _value = "";
        private BaseInterop? _interop;

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        [Parameter]
        public string? Placeholder {  get; set; } 

        [Parameter]
        public string Value
        {
            get => _value;
            set => _value = value ?? "";
        }

        [Parameter]
        public bool Disabled { get; set; } = false;

        [Parameter]
        public bool Readonly { get; set; } = false;

        [Parameter]
        public string Type { get; set; } = "text";

        [Parameter]
        public string? Label { get; set; }

        [Parameter]
        public string? HelperText { get; set; }

        [Parameter]
        public string? InfoText { get; set; }

        [Parameter]
        public string? WarningText { get; set; }

        [Parameter]
        public string? ValidText { get; set; }

        [Parameter]
        public string? InvalidText { get; set; }

        [Parameter]
        public string? AllowedCharactersPattern { get; set; }

        [Parameter]
        public int? MaxLength { get; set; }

        [Parameter]
        public RenderFragment? StartSlot { get; set; }

        [Parameter]
        public RenderFragment? EndSlot { get; set; }

        [Parameter]
        public EventCallback<string> ValueChangeEvent { get; set; }

        [Parameter]
        public EventCallback IxBlurEvent { get; set; }

        [Parameter]
        public EventCallback<JsonElement> ValidityStateChangeEvent { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                Task.Run(async () =>
                {
                    await _interop.AddEventListener(this, Id, "valueChange", "ValueChange");
                    await _interop.AddEventListener(this, Id, "ixBlur", "IxBlur");
                    await _interop.AddEventListener(this, Id, "validityStateChange", "ValidityStateChange");
                });
            }
        }

        // EVENT HANDLERS
        [JSInvokable]
        public async void ValueChange(JsonElement valueState)
        {
            string newValue = valueState.GetString() ?? "";
            _value = newValue;
            await ValueChangeEvent.InvokeAsync(newValue);
            StateHasChanged();
        }

        [JSInvokable]
        public async void IxBlur()
        {
            await IxBlurEvent.InvokeAsync();
        }

        [JSInvokable]
        public async void ValidityStateChange(JsonElement validityState)
        {
            await ValidityStateChangeEvent.InvokeAsync(validityState);
        }
    }
}
