using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;
using System.Text.Json;

namespace SiemensIXBlazor.Components.TextArea
{
    public partial class TextArea
    {
        private string _value = "";
        private bool _disabled = false;
        private bool _readonly = false;
        private bool _required = false;
        private bool? _showTextAsTooltip;
        private string? _helperText;
        private string? _infoText;
        private string? _invalidText;
        private string? _label;
        private string? _name;
        private string? _placeholder;
        private string? _validText;
        private string? _warningText;
        private string? _cssClass;
        private int? _maxLength;
        private int? _minLength;
        private int? _textareaCols;
        private int? _textareaRows;
        private string? _textareaHeight;
        private string? _textareaWidth;
        private string _resizeBehavior = "both";
        private BaseInterop? _interop;

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

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
        public bool Required
        {
            get => _required;
            set => _required = value;
        }

        [Parameter]
        public bool? ShowTextAsTooltip
        {
            get => _showTextAsTooltip;
            set => _showTextAsTooltip = value;
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
        public string? Name
        {
            get => _name;
            set => _name = value;
        }

        [Parameter]
        public string? Placeholder
        {
            get => _placeholder;
            set => _placeholder = value;
        }

        [Parameter]
        public string? ValidText
        {
            get => _validText;
            set => _validText = value;
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
        public int? MaxLength
        {
            get => _maxLength;
            set => _maxLength = value;
        }

        [Parameter]
        public int? MinLength
        {
            get => _minLength;
            set => _minLength = value;
        }

        [Parameter]
        public int? TextareaCols
        {
            get => _textareaCols;
            set => _textareaCols = value;
        }

        [Parameter]
        public int? TextareaRows
        {
            get => _textareaRows;
            set => _textareaRows = value;
        }

        [Parameter]
        public string? TextareaHeight
        {
            get => _textareaHeight;
            set => _textareaHeight = value;
        }

        [Parameter]
        public string? TextareaWidth
        {
            get => _textareaWidth;
            set => _textareaWidth = value;
        }

        [Parameter]
        public string ResizeBehavior
        {
            get => _resizeBehavior;
            set => _resizeBehavior = value;
        }

        [Parameter]
        public EventCallback IxBlurEvent { get; set; }

        [Parameter]
        public EventCallback<JsonElement> ValidityStateChangeEvent { get; set; }

        [Parameter]
        public EventCallback<string> ValueChangeEvent { get; set; }

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
