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
using SiemensIXBlazor.Interops;
using SiemensIXBlazor.Objects;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SiemensIXBlazor.Components
{
    public partial class DateTimePicker
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string DateFormat { get; set; } = "yyyy/MM/dd";
        
        [Parameter]
        public string From { get; set; } = DateTime.Now.ToString("yyyy/MM/dd");
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
        public string ShowTimeReference { get; set; } = string.Empty;
        [Parameter]
        public string I18nDone { get; set; } = "Done";

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
        public EventCallback<DateTimePickerResponse> DateSelectEvent { get; set; }
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
        public async void DateSelect(JsonElement data)
        {
            string jsonDataText = data.GetRawText();
            DateTimePickerResponse? jsonData = JObject.Parse(jsonDataText)
                                                  .ToObject<DateTimePickerResponse>();
            await DateSelectEvent.InvokeAsync(jsonData);
        }

    }
}
