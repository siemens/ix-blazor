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
using SiemensIXBlazor.Enums.DatePicker;
using SiemensIXBlazor.Interops;
using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Components;

public partial class DatePicker
{
    private BaseInterop? _interop;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    [Parameter, EditorRequired] public string Id { get; set; } = string.Empty;
    [Parameter] public string Format { get; set; } = "yyyy/LL/dd";
    [Parameter] public bool SingleSelection { get; set; }
    [Parameter] public DatePickerCorners Corners { get; set; } = DatePickerCorners.Rounded;
    [Parameter] public string? From { get; set; }
    [Parameter] public string? To { get; set; }
    [Parameter] public string MinDate { get; set; } = string.Empty;
    [Parameter] public string MaxDate { get; set; } = string.Empty;
    [Parameter] public string I18nDone { get; set; } = "Done";
    [Parameter] public string AriaLabelPreviousMonthButton { get; set; } = "Previous month";
    [Parameter] public string AriaLabelNextMonthButton { get; set; } = "Next month";
    [Parameter] public string AriaLabelMonthSelection { get; set; } = "Select month";
    [Parameter] public string AriaLabelYearSelection { get; set; } = "Select year";
    [Parameter] public int WeekStartIndex { get; set; }
    [Parameter] public string? Locale { get; set; }
    [Parameter] public bool ShowWeekNumbers { get; set; }
    [Parameter] public bool EnableTopLayer { get; set; }
    [Parameter] public EventCallback<DatePickerResponse> DateChangeEvent { get; set; }
    [Parameter] public EventCallback<DatePickerResponse> DateRangeChangeEvent { get; set; }
    [Parameter] public EventCallback<DatePickerResponse> DateSelectEvent { get; set; }

    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        _interop = new BaseInterop(JSRuntime);
        await _interop.AddEventListener(this, Id, "dateChange", nameof(DateChange));
        await _interop.AddEventListener(this, Id, "dateRangeChange", nameof(DateRangeChange));
        await _interop.AddEventListener(this, Id, "dateSelect", nameof(DateSelect));
    }

    public async Task<DatePickerResponse> GetCurrentDate()
    {
        if (_interop == null)
        {
            throw new InvalidOperationException("The date picker has not rendered yet.");
        }

        return await _interop.InvokeElementMethodAsync<DatePickerResponse>(Id, "getCurrentDate")
            ?? new DatePickerResponse();
    }

    [JSInvokable]
    public async Task DateChange(JsonElement data)
    {
        var response = data.Deserialize<DatePickerResponse>(JsonOptions);
        if (response != null)
        {
            await DateChangeEvent.InvokeAsync(response);
        }
    }

    [JSInvokable]
    public async Task DateRangeChange(JsonElement data)
    {
        var response = data.Deserialize<DatePickerResponse>(JsonOptions);
        if (response != null)
        {
            await DateRangeChangeEvent.InvokeAsync(response);
        }
    }

    [JSInvokable]
    public async Task DateSelect(JsonElement data)
    {
        var response = data.Deserialize<DatePickerResponse>(JsonOptions);
        if (response != null)
        {
            await DateSelectEvent.InvokeAsync(response);
        }
    }
}
