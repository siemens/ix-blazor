// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Enums.TimePicker;
using SiemensIXBlazor.Interops;
using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Components;

public partial class DateTimePicker
{
    private BaseInterop? _interop;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    [Parameter, EditorRequired] public string Id { get; set; } = string.Empty;
    [Parameter] public bool SingleSelection { get; set; }
    [Parameter] public string? MinDate { get; set; }
    [Parameter] public string? MaxDate { get; set; }
    [Parameter] public string DateFormat { get; set; } = "yyyy/LL/dd";
    [Parameter] public string TimeFormat { get; set; } = "HH:mm:ss";
    [Parameter] public string? MinTime { get; set; }
    [Parameter] public string? MaxTime { get; set; }
    [Parameter] public string? From { get; set; }
    [Parameter] public string? To { get; set; }
    [Parameter] public string? Time { get; set; }
    [Parameter] public bool ShowTimeReference { get; set; }
    [Parameter] public TimeReference? TimeReference { get; set; }
    [Parameter] public string I18nDone { get; set; } = "Done";
    [Parameter] public string I18nTime { get; set; } = "Time";
    [Parameter] public string AriaLabelPreviousMonthButton { get; set; } = "Previous month";
    [Parameter] public string AriaLabelNextMonthButton { get; set; } = "Next month";
    [Parameter] public int WeekStartIndex { get; set; }
    [Parameter] public string? Locale { get; set; }
    [Parameter] public bool ShowWeekNumbers { get; set; }
    [Parameter] public EventCallback<string> TimeChangeEvent { get; set; }
    [Parameter] public EventCallback<DateTimeDateChangeEvent> DateChangeEvent { get; set; }
    [Parameter] public EventCallback<DateTimePickerResponse> DateSelectEvent { get; set; }

    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        _interop = new BaseInterop(JSRuntime);
        await _interop.AddEventListener(this, Id, "dateChange", nameof(DateChange));
        await _interop.AddEventListener(this, Id, "timeChange", nameof(TimeChange));
        await _interop.AddEventListener(this, Id, "dateSelect", nameof(DateSelect));
    }

    [JSInvokable]
    public async Task DateChange(JsonElement data)
    {
        var response = new DateTimeDateChangeEvent();
        if (data.ValueKind == JsonValueKind.String)
        {
            response.Value = data.GetString();
        }
        else if (data.ValueKind == JsonValueKind.Object)
        {
            var range = data.Deserialize<DatePickerResponse>(JsonOptions);
            response.From = range?.From;
            response.To = range?.To;
        }

        await DateChangeEvent.InvokeAsync(response);
    }

    [JSInvokable]
    public async Task TimeChange(string value)
    {
        await TimeChangeEvent.InvokeAsync(value);
    }

    [JSInvokable]
    public async Task DateSelect(JsonElement data)
    {
        var response = data.Deserialize<DateTimePickerResponse>(JsonOptions);
        if (response != null)
        {
            await DateSelectEvent.InvokeAsync(response);
        }
    }
}
