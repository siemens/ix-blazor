using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Components.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class DateTimePicker
    {
        [Parameter]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public static string DateFormat { get; set; } = "yyyy/MM/dd";
        [Parameter]
        public string EventDelimiter { get; set; } = " - ";
        [Parameter]
        public string From { get; set; } = DateTime.Now.ToString(DateFormat);
        [Parameter]
        public string? MaxDate { get; set; }
        [Parameter]
        public string? MinDate { get; set; }
        [Parameter]
        public bool Range { get; set; } = true;
        [Parameter]
        public bool ShowHour { get; set; } = false;
        [Parameter]
        public bool ShowMinutes { get; set; } = false;
        [Parameter]
        public bool ShowSeconds { get; set; } = false;
        [Parameter]
        public string ShowTimeReference { get; set; } = string.Empty;
        [Parameter]
        public string TextSelectDate { get; set; } = "Done";
        [Parameter]
        public string? Time { get; set; }
        [Parameter]
        public string TimeFormat { get; set; } = "HH:mm:ss";
        [Parameter]
        public string? TimeReference { get; set; }
        [Parameter]
        public string? To { get; set; }
        [Parameter]
        public EventCallback<string> DateChangeEvent { get; set; }
        [Parameter]
        public EventCallback<string> DateSelectEvent { get; set; }
        [Parameter]
        public EventCallback<string> TimeChangeEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "dateChange", "DateChange");
                await _interop.AddEventListener(this, Id, "timeChange", "TimeChange");
                await _interop.AddEventListener(this, Id, "dateSelect", "DateSelect");
            }
        }

        [JSInvokable]
        public async void DateChange(string date)
        {
            await DateChangeEvent.InvokeAsync(date);
        }

        [JSInvokable]
        public async void TimeChange(string date)
        {
            await TimeChangeEvent.InvokeAsync(date);
        }

        [JSInvokable]
        public async void DateSelect(string date)
        {
            await DateSelectEvent.InvokeAsync(date);
        }
    }
}
