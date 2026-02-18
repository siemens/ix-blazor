using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;
using System.Text.Json;

namespace SiemensIXBlazor.Components.TimeInput
{
    public partial class TimeInput
    {
        private int _hourInterval = 1;
        private string _i18nErrorDateUnparsable = "Time is not valid";
        private string _i18nHourColumnHeader = "hr";
        private string _i18nMillisecondColumnHeader = "ms";
        private string _i18nMinuteColumnHeader = "min";
        private string _i18nSecondColumnHeader = "sec";
        private string _i18nSelectTime = "Confirm";
        private string _i18nTime = "Time";
        private int _millisecondInterval = 100;
        private int _minuteInterval = 1;
        private int _secondInterval = 1;
        private BaseInterop? _interop;

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        [Parameter]
        public bool Disabled { get; set; } = false;

        [Parameter]
        public bool EnableTopLayer { get; set; } = false;

        [Parameter]
        public string Format { get; set; } = "TT";

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
        public string? PlaceHolder { get; set; }

        [Parameter]
        public bool Readonly { get; set; } = false;

        [Parameter]
        public bool? Required { get; set; }

        [Parameter]
        public bool? ShowTextAsTooltip { get; set; }

        [Parameter]
        public string? ValidText { get; set; }

        [Parameter]
        public string Value { get; set; } = "";

        [Parameter]
        public string? WarningText { get; set; }

        [Parameter]
        public RenderFragment? StartSlot { get; set; }

        [Parameter]
        public RenderFragment? EndSlot { get; set; }

        [Parameter]
        public EventCallback<string> ValueChangeEvent { get; set; }

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
        public async void ValidityStateChange(JsonElement validityState)
        {
            await ValidityStateChangeEvent.InvokeAsync(validityState);
        }
    }
}
