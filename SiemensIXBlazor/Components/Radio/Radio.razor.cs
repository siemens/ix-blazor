using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;
using System.Text.Json;

namespace SiemensIXBlazor.Components.Radio
{
    public partial class Radio
    {
        private bool _checked = false;
        private bool _disabled = false;
        private string? _label;
        private string? _name;
        private bool _required = false;
        private string? _value;
        private string? _cssClass;
        private BaseInterop? _interop;

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        [Parameter]
        public bool Checked
        {
            get => _checked;
            set => _checked = value;
        }

        [Parameter]
        public bool Disabled
        {
            get => _disabled;
            set => _disabled = value;
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
        public bool Required
        {
            get => _required;
            set => _required = value;
        }

        [Parameter]
        public string? Value
        {
            get => _value;
            set => _value = value;
        }

        [Parameter]
        public string? CssClass
        {
            get => _cssClass;
            set => _cssClass = value;
        }

        [Parameter]
        public EventCallback<bool> CheckedChangeEvent { get; set; }

        [Parameter]
        public EventCallback IxBlurEvent {  get; set; }

        [Parameter]
        public EventCallback<string> ValueChangeEvent { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                Task.Run(async () =>
                {
                    await _interop.AddEventListener(this, Id, "checkedChange", "CheckedChange");
                    await _interop.AddEventListener(this, Id, "ixBlur", "IxBlur");
                    await _interop.AddEventListener(this, Id, "valueChange", "ValueChange");
                });
            }
        }

        // EVENT HANDLERS
        [JSInvokable]
        public async void CheckedChange(JsonElement checkedState)
        {
            bool newChecked = checkedState.GetBoolean();
            _checked = newChecked;
            await CheckedChangeEvent.InvokeAsync(newChecked);
            StateHasChanged();
        }

        [JSInvokable]
        public async void IxBlur()
        {
            await IxBlurEvent.InvokeAsync();
        }

        [JSInvokable]
        public async void ValueChange(JsonElement valueState)
        {
            string newValue = valueState.GetString();
            _value = newValue;
            await ValueChangeEvent.InvokeAsync(newValue);
            StateHasChanged();
        }
    }
}
