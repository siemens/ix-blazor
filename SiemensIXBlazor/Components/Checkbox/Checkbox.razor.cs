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
        private Lazy<Task<IJSObjectReference>>? moduleTask;
        private BaseInterop? _interop;
        private readonly List<(string FunctionName, string Parameter)> pendingParameterUpdates = [];

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
        public EventCallback<bool> CheckedChangeEvent { get; set; }

        [Parameter]
        public EventCallback IxBlurEvent { get; set; }

        [Parameter]
        public EventCallback<string> ValueChangedEvent { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop ??= new(JSRuntime);
                await _interop.AddEventListener(this, Id, "checkedChange", "CheckedChanged");
                await _interop.AddEventListener(this, Id, "ixBlur", "IxBlur", includeDetail: false);
                await _interop.AddEventListener(this, Id, "valueChange", "ValueChanged");
            }

            if (pendingParameterUpdates.Count == 0 || moduleTask is null)
            {
                return;
            }

            var module = await moduleTask.Value;
            RegisterDisposable(module);

            foreach (var (functionName, parameter) in pendingParameterUpdates)
            {
                await module.InvokeVoidAsync(functionName, Id, parameter);
            }

            pendingParameterUpdates.Clear();
        }

        [JSInvokable]
        public async Task CheckedChanged(JsonElement checkState)
        {
            bool isChecked = checkState.GetBoolean();
            _checked = isChecked;
            await CheckedChangeEvent.InvokeAsync(isChecked);
        }

        [JSInvokable]
        public async Task IxBlur()
        {
            await IxBlurEvent.InvokeAsync();
        }

        [JSInvokable]
        public async Task ValueChanged(JsonElement valueState)
        {
            string value = valueState.GetString() ?? "on";
            _value = value;
            await ValueChangedEvent.InvokeAsync(value);
        }

        private void InitialParameter(string functionName, object param)
        {
            moduleTask ??= new(() => JSRuntime.InvokeAsync<IJSObjectReference>(
                "import", $"./_content/Siemens.IX.Blazor/js/siemens-ix/interops/checkboxInterop.js").AsTask());
            pendingParameterUpdates.Add((functionName, JsonConvert.SerializeObject(param)));
        }

    }

}
