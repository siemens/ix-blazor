using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using SiemensIXBlazor.Interops;
using System.Text.Json;

namespace SiemensIXBlazor.Components.NumberInput
{
    public partial class NumberInput
    {
        private BaseInterop? _interop;

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        [Parameter]
        public string? AllowedCharactersPattern { get; set; }

        [Parameter]
        public bool Disabled { get; set; } = false;

        [Parameter]
        public string? HelperText { get; set; }

        [Parameter]
        public string? InfoText { get; set; }

        [Parameter]
        public string? InvalidText { get; set; }

        [Parameter]
        public string? Label { get; set; }

        [Parameter]
        public object? Max { get; set; }

        [Parameter]
        public object? Min { get; set; }

        [Parameter]
        public string? Name { get; set; }

        [Parameter]
        public string? Pattern { get; set; }

        [Parameter]
        public string? Placeholder { get; set; }

        [Parameter]
        public bool Readonly { get; set; } = false;

        [Parameter]
        public bool Required { get; set; } = false;

        [Parameter]
        public bool? ShowStepperButtons { get; set; }

        [Parameter]
        public bool? ShowTextAsTooltip { get; set; }

        [Parameter]
        public object? Step { get; set; }

        [Parameter]
        public string? ValidText { get; set; }

        [Parameter]
        public double Value { get; set; } = 0;

        [Parameter]
        public string? WarningText { get; set; }

        [Parameter]
        public string? CssClass { get; set; }

        [Parameter]
        public RenderFragment? StartSlot { get; set; }

        [Parameter]
        public RenderFragment? EndSlot { get; set; }

        [Parameter]
        public EventCallback IxBlurEvent { get; set; }

        [Parameter]
        public EventCallback<object> ValidityStateChangeEvent { get; set; }

        [Parameter]
        public EventCallback<double> ValueChangeEvent { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                Task.Run(async () =>
                {
                    await _interop.AddEventListener(this, Id, "ixBlur", "IxBlur");
                    await _interop.AddEventListener(this, Id, "validityStateChange", "ValidityStateChange");
                    await _interop.AddEventListener(this, Id, "valueChange", "ValueChange");
                });
            }
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

        [JSInvokable]
        public async void ValueChange(JsonElement valueState)
        {
            double newValue = valueState.GetDouble();
            Value = newValue;
            await ValueChangeEvent.InvokeAsync(newValue);
            StateHasChanged();
        }
    }
}
