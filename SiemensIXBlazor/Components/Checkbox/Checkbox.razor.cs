using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using SiemensIXBlazor.Interops;
using System.Text.Json;

namespace SiemensIXBlazor.Components.Checkbox
{
    public partial class Checkbox
    {
        private bool _checked = false;
        private bool _indeterminate = false;
        private string _value = "on";
        private ValidationState _validationState = ValidationState.None;
        private Lazy<Task<IJSObjectReference>>? moduleTask;
        private BaseInterop? _interop;

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        [Parameter]
        public bool Checked
        {
            get => _checked;
            set
            {
                _checked = value;
                InitialParameter("setChecked", _checked);
            }
        }

        [Parameter]
        public bool Disabled { get; set; } = false;

        [Parameter]
        public bool Indeterminate
        {
            get => _indeterminate;
            set
            {
                _indeterminate = value;
                InitialParameter("setIndeterminate", _indeterminate);
            }
        }

        [Parameter]
        public string? Label { get; set; }

        [Parameter]
        public string? Name { get; set; }

        [Parameter]
        public bool Required { get; set; } = false;

        [Parameter]
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                InitialParameter("setValue", _value);
            }
        }

        [Parameter]
        public ValidationState ValidationState
        {
            get => _validationState;
            set
            {
                _validationState = value;
                UpdateValidationClass(); 
            }
        }

        [Parameter]
        public EventCallback<bool> CheckedChangeEvent { get; set; }

        [Parameter]
        public EventCallback IxBlurEvent { get; set; }

        [Parameter]
        public EventCallback<string> ValueChangedEvent { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                Task.Run(async () =>
                {
                    await _interop.AddEventListener(this, Id, "checkedChanged", "CheckedChanged");
                    await _interop.AddEventListener(this, Id, "ixBlur", "IxBlur");
                    await _interop.AddEventListener(this, Id, "valueChanged", "ValueChanged");
                });
            }
        }

        [JSInvokable]
        public async void CheckedChanged(JsonElement checkState)
        {
            bool isChecked = checkState.GetBoolean();
            _checked = isChecked;
            await CheckedChangeEvent.InvokeAsync(isChecked);
        }

        [JSInvokable]
        public async void IxBlur()
        {
            await IxBlurEvent.InvokeAsync(true);
        }

        [JSInvokable]
        public async void ValueChanged(JsonElement valueState)
        {
            string value = valueState.GetString() ?? "on";
            _value = value;
            await ValueChangedEvent.InvokeAsync(value);
        }

        private void UpdateValidationClass()
        {
            var validationClass = _validationState switch
            {
                ValidationState.Info => "ix-info",
                ValidationState.Warning => "ix-warning",
                ValidationState.Invalid => "ix-invalid",
                ValidationState.Valid => "ix-valid",
                _ => ""
            };

            if (!string.IsNullOrEmpty(validationClass))
            {
                Class = string.IsNullOrEmpty(Class) ? validationClass : $"{Class} {validationClass}";
            }
        }

        private void InitialParameter(string functionName, object param)
        {
            moduleTask = new(() => JSRuntime.InvokeAsync<IJSObjectReference>(
                "import", $"./_content/Siemens.IX.Blazor/js/siemens-ix/interops/checkboxInterop.js").AsTask());

            Task.Run(async () =>
            {
                var module = await moduleTask.Value;
                if (module != null)
                {
                    await module.InvokeVoidAsync(functionName, Id, JsonConvert.SerializeObject(param));
                }
            });
        }

    }



    public enum ValidationState
    {
        None,     
        Info,     
        Warning,  
        Invalid,  
        Valid     
    }
}