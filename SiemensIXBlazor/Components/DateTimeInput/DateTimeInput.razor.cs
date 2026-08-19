// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.Input;
using SiemensIXBlazor.Interops;
using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Components.DateTimeInput;

public partial class DateTimeInput
{
    private BaseInterop? _interop;

    [Parameter, EditorRequired] public string Id { get; set; } = string.Empty;
    [Parameter] public string? Name { get; set; }
    [Parameter] public string? Placeholder { get; set; }
    [Parameter] public string? Value { get; set; } = string.Empty;
    [Parameter] public string Format { get; set; } = "yyyy/LL/dd HH:mm:ss";
    [Parameter] public string? Locale { get; set; }
    [Parameter] public bool Required { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public bool Readonly { get; set; }
    [Parameter] public string? MinDate { get; set; }
    [Parameter] public string? MaxDate { get; set; }
    [Parameter] public string? MinTime { get; set; }
    [Parameter] public string? MaxTime { get; set; }
    [Parameter] public string? Label { get; set; }
    [Parameter] public string? HelperText { get; set; }
    [Parameter] public string? InvalidText { get; set; }
    [Parameter] public string? InfoText { get; set; }
    [Parameter] public string? WarningText { get; set; }
    [Parameter] public string? ValidText { get; set; }
    [Parameter] public bool ShowTextAsTooltip { get; set; }
    [Parameter] public string I18nErrorDateTimeUnparsable { get; set; } = "Date time is not valid";
    [Parameter] public string I18nDone { get; set; } = "Confirm";
    [Parameter] public string I18nTime { get; set; } = "Time";
    [Parameter] public string AriaLabelPreviousMonthButton { get; set; } = "Previous month";
    [Parameter] public string AriaLabelNextMonthButton { get; set; } = "Next month";
    [Parameter] public string AriaLabelCalendarButton { get; set; } = "Toggle calendar";
    [Parameter] public bool ShowWeekNumbers { get; set; }
    [Parameter] public int WeekStartIndex { get; set; }
    [Parameter] public bool SuppressSubmitOnEnter { get; set; }
    [Parameter] public InputTextAlignment TextAlignment { get; set; } = InputTextAlignment.Start;
    [Parameter] public bool EnableTopLayer { get; set; }
    [Parameter] public RenderFragment? StartSlot { get; set; }
    [Parameter] public RenderFragment? EndSlot { get; set; }
    [Parameter] public EventCallback<string?> ValueChangeEvent { get; set; }
    [Parameter] public EventCallback<DateTimeInputValidityState> ValidityStateChangeEvent { get; set; }
    [Parameter] public EventCallback<string?> ChangeEvent { get; set; }
    [Parameter] public EventCallback IxFocusEvent { get; set; }
    [Parameter] public EventCallback IxBlurEvent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        _interop = new(JSRuntime);
        await _interop.AddEventListener(this, Id, "valueChange", nameof(ValueChange));
        await _interop.AddEventListener(this, Id, "validityStateChange", nameof(ValidityStateChange));
        await _interop.AddEventListener(this, Id, "ixChange", nameof(Change));
        await _interop.AddEventListener(this, Id, "ixFocus", nameof(IxFocus), includeDetail: false);
        await _interop.AddEventListener(this, Id, "ixBlur", nameof(IxBlur), includeDetail: false);
    }

    [JSInvokable]
    public async Task ValueChange(JsonElement value)
    {
        Value = ReadNullableString(value);
        await ValueChangeEvent.InvokeAsync(Value);
        StateHasChanged();
    }

    [JSInvokable]
    public async Task ValidityStateChange(JsonElement value)
    {
        var state = value.Deserialize<DateTimeInputValidityState>(JsonOptions);
        if (state != null)
        {
            await ValidityStateChangeEvent.InvokeAsync(state);
        }
    }

    [JSInvokable]
    public async Task Change(JsonElement value)
    {
        await ChangeEvent.InvokeAsync(ReadNullableString(value));
    }

    [JSInvokable]
    public Task IxFocus() => IxFocusEvent.InvokeAsync();

    [JSInvokable]
    public Task IxBlur() => IxBlurEvent.InvokeAsync();

    private static string? ReadNullableString(JsonElement value) =>
        value.ValueKind is JsonValueKind.Null or JsonValueKind.Undefined ? null : value.GetString();

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
}
