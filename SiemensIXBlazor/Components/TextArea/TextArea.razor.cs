using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;
using System.Text.Json;

namespace SiemensIXBlazor.Components.TextArea
{
    public partial class TextArea
    {
        private BaseInterop? _interop;

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        [Parameter]
        public string Value { get; set; } = "";

        [Parameter]
        public bool Disabled { get; set; } = false;

        [Parameter]
        public bool Readonly { get; set; } = false;

        [Parameter]
        public bool Required { get; set; } = false;

        [Parameter]
        public bool? ShowTextAsTooltip { get; set; }

        [Parameter]
        public string? HelperText { get; set; }

        [Parameter]
        public string? InfoText { get; set; }

        [Parameter]
        public string? InvalidText { get; set; }

        [Parameter]
        public string? Label { get; set; }

        [Parameter]
        public string? Name { get; set; }

        [Parameter]
        public string? Placeholder { get; set; }

        [Parameter]
        public string? ValidText { get; set; }

        [Parameter]
        public string? WarningText { get; set; }

        [Parameter]
        public string? CssClass { get; set; }

        [Parameter]
        public int? MaxLength { get; set; }

        [Parameter]
        public int? MinLength { get; set; }

        [Parameter]
        public int? TextareaCols { get; set; }

        [Parameter]
        public int? TextareaRows { get; set; }

        [Parameter]
        public string? TextareaHeight { get; set; }

        [Parameter]
        public string? TextareaWidth { get; set; }

        [Parameter]
        public string ResizeBehavior { get; set; } = "both";

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

        [JSInvokable]
        public async void ValueChange(JsonElement valueState)
        {
            string newValue = valueState.GetString() ?? "";
            Value = newValue;
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
