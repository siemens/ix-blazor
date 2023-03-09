using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Components.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class DatePicker
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string Class { get; set; } = string.Empty;
        [Parameter]
        public string Corners { get; set; } = "rounded";
        [Parameter]
        public string EventDelimiter { get; set; } = " - ";
        [Parameter]
        public static string Format { get; set; } = "yyyy/MM/dd";
        [Parameter]
        public string From { get; set; } = DateTime.Now.ToString(Format);
        [Parameter]
        public string? MaxDate { get; set; }
        [Parameter]
        public string? MinDate { get; set; }
        [Parameter]
        public bool Range { get; set; } = true;
        [Parameter]
        public string TextSelectDate { get; set; } = "Done";
        [Parameter]
        public string? To { get; set; }
        [Parameter]
        public EventCallback<string> DateChangeEvent { get; set; }
        [Parameter]
        public EventCallback<string> DateRangeChangeEvent { get; set; }
        [Parameter]
        public EventCallback<string> DateSelectEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "dateChange", "DateChange");
                await _interop.AddEventListener(this, Id, "dateRangeChange", "DateRangeChange");
                await _interop.AddEventListener(this, Id, "dateSelect", "DateSelect");
            }
        }

        [JSInvokable]
        public async void DateChange(string date)
        {
            await DateChangeEvent.InvokeAsync(date);
        }

        [JSInvokable]
        public async void DateRangeChange(string date)
        {
            await DateRangeChangeEvent.InvokeAsync(date);
        }

        [JSInvokable]
        public async void DateSelect(string date)
        {
            await DateSelectEvent.InvokeAsync(date);
        }
    }
}
