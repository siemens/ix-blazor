using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;
using System.Text.Json;

namespace SiemensIXBlazor.Components.Input
{
    public partial class Input
    {
        private string _value = "";
        private string _type = "text";
        private bool _disabled = false;
        private bool _readonly = false;
        private string? _placeholder;
        private string? _label;
        private string? _helperText;
        private string? _infoText;
        private string? _warningText;
        private string? _validText;
        private string? _invalidText;
        private string? _allowedCharactersPattern;
        private int? _maxLength;
        private BaseInterop? _interop;

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        [Parameter]
        public string? Placeholder
        {
            get => _placeholder;
            set => _placeholder = value;
        }

        [Parameter]
        public string Value
        {
            get => _value;
            set => _value = value ?? "";
        }

        [Parameter]
        public bool Disabled
        {
            get => _disabled;
            set => _disabled = value;
        }

        [Parameter]
        public bool Readonly
        {
            get => _readonly;
            set => _readonly = value;
        }

        [Parameter]
        public string Type
        {
            get => _type;
            set => _type = value;
        }

        [Parameter]
        public string? Label
        {
            get => _label;
            set => _label = value;
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
        public string? WarningText
        {
            get => _warningText;
            set => _warningText = value;
        }

        [Parameter]
        public string? ValidText
        {
            get => _validText;
            set => _validText = value;
        }

        [Parameter]
        public string? InvalidText
        {
            get => _invalidText;
            set => _invalidText = value;
        }

        [Parameter]
        public string? AllowedCharactersPattern
        {
            get => _allowedCharactersPattern;
            set => _allowedCharactersPattern = value;
        }

        [Parameter]
        public int? MaxLength
        {
            get => _maxLength;
            set => _maxLength = value;
        }

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
