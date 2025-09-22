using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;
using System.Text.Json;

namespace SiemensIXBlazor.Components.NumberInput
{
    public partial class NumberInput
    {
        private bool _disabled = false;
        private string? _helperText;
        private string? _infoText;
        private string? _invalidText;
        private string? _label;
        private object? _max;
        private object? _min;
        private string? _name;
        private string? _pattern;
        private string? _allowedCharactersPattern;
        private string? _placeholder;
        private bool _readonly = false;
        private bool _required = false;
        private bool? _showStepperButtons;
        private bool? _showTextAsTooltip;
        private object? _step;
        private string? _validText;
        private double _value = 0;
        private string? _warningText;
        private string? _cssClass;
        private BaseInterop? _interop;

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        [Parameter]
        public string? AllowedCharactersPattern
        {
            get => _allowedCharactersPattern;
            set => _allowedCharactersPattern = value;
        }

        [Parameter]
        public bool Disabled
        {
            get => _disabled;
            set => _disabled = value;
        }

        [Parameter]
        public string? HelperText
        {
            get => _helperText;
            set => _helperText = value;
        }

        [Parameter]
        public string? InfoText
        {
            get => _infoText;
            set => _infoText = value;
        }

        [Parameter]
        public string? InvalidText
        {
            get => _invalidText;
            set => _invalidText = value;
        }

        [Parameter]
        public string? Label
        {
            get => _label;
            set => _label = value;
        }

        [Parameter]
        public object? Max
        {
            get => _max;
            set => _max = value;
        }

        [Parameter]
        public object? Min
        {
            get => _min;
            set => _min = value;
        }

        [Parameter]
        public string? Name
        {
            get => _name;
            set => _name = value;
        }

        [Parameter]
        public string? Pattern
        {
            get => _pattern;
            set => _pattern = value;
        }

        [Parameter]
        public string? Placeholder
        {
            get => _placeholder;
            set => _placeholder = value;
        }

        [Parameter]
        public bool Readonly
        {
            get => _readonly;
            set => _readonly = value;
        }

        [Parameter]
        public bool Required
        {
            get => _required;
            set => _required = value;
        }

        [Parameter]
        public bool? ShowStepperButtons
        {
            get => _showStepperButtons;
            set => _showStepperButtons = value;
        }

        [Parameter]
        public bool? ShowTextAsTooltip
        {
            get => _showTextAsTooltip;
            set => _showTextAsTooltip = value;
        }

        [Parameter]
        public object? Step
        {
            get => _step;
            set => _step = value;
        }

        [Parameter]
        public string? ValidText
        {
            get => _validText;
            set => _validText = value;
        }

        [Parameter]
        public double Value
        {
            get => _value;
            set => _value = value;
        }

        [Parameter]
        public string? WarningText
        {
            get => _warningText;
            set => _warningText = value;
        }

        [Parameter]
        public string? CssClass
        {
            get => _cssClass;
            set => _cssClass = value;
        }

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

        // EVENT HANDLERS 
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
            _value = newValue;
            await ValueChangeEvent.InvokeAsync(newValue);
            StateHasChanged();
        }
    }
}
