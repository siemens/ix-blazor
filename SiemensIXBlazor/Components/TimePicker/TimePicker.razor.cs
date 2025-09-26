﻿// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Enums.DatePicker;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class TimePicker
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public DatePickerCorners Corners { get; set; } = DatePickerCorners.Rounded;
        [Parameter]
        public string Format { get; set; } = "yyyy/MM/dd";

        [Parameter]
        public bool HideHeader { get; set; } = false;

        [Parameter]
        public int HourInterval { get; set; } = 1;

        [Parameter]
        public int MillisecondInterval { get; set; } = 1;

        [Parameter]
        public int MinuteInterval { get; set; } = 1;

        [Parameter]
        public int SecondInterval { get; set; } = 1;

        [Parameter]
        public bool ShowHour { get; set; } = false;
        [Parameter]
        public bool ShowMinutes { get; set; } = false;
        [Parameter]
        public bool ShowSeconds { get; set; } = false;
        [Parameter]
        public string TextSelectTime { get; set; } = "Done";

        [Parameter]
        public string Time { get; set; }
        protected override void OnInitialized()
        {
            if (string.IsNullOrEmpty(Time))
            {
                Time = DateTime.Now.ToString(Format);
            }
        }

        [Parameter]
        public EventCallback<string> DoneEvent { get; set; }
        [Parameter]
        public EventCallback<string> TimeChangeEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "done", "Done");
                await _interop.AddEventListener(this, Id, "timeChange", "TimeChanged");
            }
        }

        [JSInvokable]
        public async void Done(string date)
        {
            await DoneEvent.InvokeAsync(date);
        }

        [JSInvokable]
        public async void TimeChanged(string date)
        {
            await TimeChangeEvent.InvokeAsync(date);
        }
    }
}
