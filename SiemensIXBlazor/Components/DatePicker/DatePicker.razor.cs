using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using SiemensIXBlazor.Enums.DatePicker;
using SiemensIXBlazor.Interops;
using SiemensIXBlazor.Objects;
using System.Text.Json;

namespace SiemensIXBlazor.Components
{
    public partial class DatePicker
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public DatePickerCorners? Corners { get; set; }
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
        public EventCallback<DatePickerResponse?> DateRangeChangeEvent { get; set; }
        [Parameter]
        public EventCallback<DatePickerResponse> DateSelectEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "dateRangeChange", "DateRangeChange");
                await _interop.AddEventListener(this, Id, "dateSelect", "DateSelect");
            }
        }

        [JSInvokable]
        public async void DateRangeChange(JsonElement data)
        {
            string jsonDataText = data.GetRawText();
            DatePickerResponse? jsonData = JObject.Parse(jsonDataText)
                                                  .ToObject<DatePickerResponse>();
            await DateRangeChangeEvent.InvokeAsync(jsonData);
        }

        [JSInvokable]
        public async void DateSelect(JsonElement data)
        {
            string jsonDataText = data.GetRawText();
            DatePickerResponse? jsonData = JObject.Parse(jsonDataText)
                                                  .ToObject<DatePickerResponse>();
            await DateSelectEvent.InvokeAsync(jsonData);
        }
    }
}
