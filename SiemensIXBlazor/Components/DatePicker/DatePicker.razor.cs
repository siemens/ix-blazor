// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

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
        public DatePickerCorners Corners { get; set; } = DatePickerCorners.Rounded;
   
        [Parameter]
        public string Format { get; set; } = "yyyy/MM/dd";
        [Parameter]
        public string? From { get; set; } 
        [Parameter]
        public string? MaxDate { get; set; }
        [Parameter]
        public string? MinDate { get; set; }
        [Parameter]
        public string? AriaLabelNextMonthButton { get; set; }
        [Parameter]
        public string? AriaLabelPreviousMonthButton { get; set; }
        [Parameter]
        public bool SingleSelection { get; set; } = false;
        [Parameter]
        public string I18nDone { get; set; } = "Done";
        [Parameter]
        public string? Locale { get; set; }
        [Parameter]
        public int WeekStartIndex { get; set; } = 0;
        [Parameter]
        public string? To { get; set; }
        [Parameter]
        public EventCallback<DatePickerResponse?> DateRangeChangeEvent { get; set; }
        [Parameter]
        public EventCallback<DatePickerResponse?> DateChangeEvent { get; set; }
        [Parameter]

        public EventCallback<DatePickerResponse> DateSelectEvent { get; set; }

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
        public async void DateRangeChange(JsonElement data)
        {
            string jsonDataText = data.GetRawText();
            DatePickerResponse? jsonData = JObject.Parse(jsonDataText)
                                                  .ToObject<DatePickerResponse>();
            await DateRangeChangeEvent.InvokeAsync(jsonData);
        }


        [JSInvokable]
        public async void DateChange(JsonElement data)
        {
            string jsonDataText = data.GetRawText();
            DatePickerResponse? jsonData = JObject.Parse(jsonDataText)
                                                  .ToObject<DatePickerResponse>();
            await DateChangeEvent.InvokeAsync(jsonData);
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
