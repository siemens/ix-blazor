using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;
using System.Text.Json;

namespace SiemensIXBlazor.Components.DateInput
{
    public partial class DateInput
    {
        private string _i18nErrorDateUnparsable = "Date is not valid";
        private BaseInterop? _interop;

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        [Parameter]
        public string? AriaLabelCalendarButton { get; set; }

        [Parameter]
        public string? AriaLabelNextMonthButton { get; set; }

        [Parameter]
        public string? AriaLabelPreviousMonthButton { get; set; }

        [Parameter]
        public bool Disabled { get; set; } = false;

        [Parameter]
        public string Format { get; set; } = "yyyy/LL/dd";

        [Parameter]
        public string? HelperText { get; set; }

        [Parameter]
        public string? InfoText { get; set; }

        [Parameter]
        public string? InvalidText { get; set; }

        [Parameter]
        public string? Label { get; set; }

        [Parameter]
        public string? Locale { get; set; }

        [Parameter]
        public string MaxDate { get; set; } = "";

        [Parameter]
        public string MinDate { get; set; } = "";

        [Parameter]
        public string? Name { get; set; }

        [Parameter]
        public string? PlaceHolder { get; set; }

        [Parameter]
        public bool Readonly { get; set; }= false;

        [Parameter]
        public bool? Required { get; set; }

        [Parameter]
        public bool? ShowTextAsTooltip { get; set; }

        [Parameter]
        public bool ShowWeekNumbers { get; set; } = false;

        [Parameter]
        public string? ValidText { get; set; }

        [Parameter]
        public string Value { get; set; } = "";

        [Parameter]
        public string? WarningText { get; set; }

        [Parameter]
        public int WeekStartIndex { get; set; } = 0;

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
