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
using SiemensIXBlazor.Enums.TimePicker;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components;

public partial class TimePicker
{
    [Parameter, EditorRequired] public string Id { get; set; } = string.Empty;
    [Parameter] public string Format { get; set; } = "TT";
    [Parameter] public TimePickerCorners Corners { get; set; } = TimePickerCorners.Rounded;
    [Parameter] public bool Embedded { get; set; }
    [Parameter] public bool HideHeader { get; set; }
    [Parameter] public int HourInterval { get; set; } = 1;
    [Parameter] public int MinuteInterval { get; set; } = 1;
    [Parameter] public int SecondInterval { get; set; } = 1;
    [Parameter] public int MillisecondInterval { get; set; } = 100;
    [Parameter] public string? Time { get; set; }
    [Parameter] public string? MinTime { get; set; }
    [Parameter] public string? MaxTime { get; set; }
    [Parameter] public string I18nConfirmTime { get; set; } = "Confirm";
    [Parameter] public string I18nHeader { get; set; } = "Time";
    [Parameter] public string I18nHourColumnHeader { get; set; } = "hr";
    [Parameter] public string I18nMinuteColumnHeader { get; set; } = "min";
    [Parameter] public string I18nSecondColumnHeader { get; set; } = "sec";
    [Parameter] public string I18nMillisecondColumnHeader { get; set; } = "ms";
    [Parameter] public EventCallback<string> TimeSelectEvent { get; set; }
    [Parameter] public EventCallback<string> TimeChangeEvent { get; set; }

    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
    private BaseInterop? _interop;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        _interop = new(JSRuntime);
        await _interop.AddEventListener(this, Id, "timeSelect", nameof(TimeSelected));
        await _interop.AddEventListener(this, Id, "timeChange", nameof(TimeChanged));
    }

    public Task<string?> GetCurrentTime() =>
        _interop?.InvokeElementMethodAsync<string>(Id, "getCurrentTime")
        ?? throw new InvalidOperationException("The time picker has not rendered yet.");

    [JSInvokable]
    public async Task TimeSelected(string value)
    {
        await TimeSelectEvent.InvokeAsync(value);
    }

    [JSInvokable]
    public async Task TimeChanged(string value)
    {
        await TimeChangeEvent.InvokeAsync(value);
    }
}
