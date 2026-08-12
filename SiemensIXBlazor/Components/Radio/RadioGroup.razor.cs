using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Enums.Radio;
using SiemensIXBlazor.Interops;
using System.Text.Json;

namespace SiemensIXBlazor.Components.Radio
{
    public partial class RadioGroup
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

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
        public bool ShowTextAsTooltip { get; set; } = false;

        [Parameter]
        public string? Value { get; set; }

        [Parameter]
        public RadioGroupDirection Direction { get; set; } = RadioGroupDirection.Column;

        [Parameter]
        public EventCallback<string> ValueChangeEvent { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        private BaseInterop? _interop;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);
                await _interop.AddEventListener(this, Id, "valueChange", "ValueChange");
            }
        }

        [JSInvokable]
        public async Task ValueChange(JsonElement valueState)
        {
            var newValue = valueState.GetString() ?? string.Empty;
            Value = newValue;
            await ValueChangeEvent.InvokeAsync(newValue);
            await InvokeAsync(StateHasChanged);
        }
    }
}
