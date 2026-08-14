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
using SiemensIXBlazor.Enums.Input;
using SiemensIXBlazor.Interops;
using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Components.DateInput;

public partial class DateInput
{
    private BaseInterop? _interop;

    [Parameter, EditorRequired] public string Id { get; set; } = string.Empty;
    [Parameter] public string? Name { get; set; }
    [Parameter] public string? Placeholder { get; set; }
    [Parameter] public string? Value { get; set; } = string.Empty;
    [Parameter] public string MinDate { get; set; } = string.Empty;
    [Parameter] public string MaxDate { get; set; } = string.Empty;
    [Parameter] public string? Locale { get; set; }
    [Parameter] public string Format { get; set; } = "yyyy/LL/dd";
    [Parameter] public bool? Required { get; set; }
    [Parameter] public string? HelperText { get; set; }
    [Parameter] public string? Label { get; set; }
    [Parameter] public string AriaLabelCalendarButton { get; set; } = "Open calendar";
    [Parameter] public string? InvalidText { get; set; }
    [Parameter] public bool Readonly { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public string? InfoText { get; set; }
    [Parameter] public string? WarningText { get; set; }
    [Parameter] public string? ValidText { get; set; }
    [Parameter] public bool? ShowTextAsTooltip { get; set; }
    [Parameter] public string I18nErrorDateUnparsable { get; set; } = "Date is not valid";
    [Parameter] public bool ShowWeekNumbers { get; set; }
    [Parameter] public int WeekStartIndex { get; set; }
    [Parameter] public string AriaLabelPreviousMonthButton { get; set; } = "Previous month";
    [Parameter] public string AriaLabelNextMonthButton { get; set; } = "Next month";
    [Parameter] public bool SuppressSubmitOnEnter { get; set; }
    [Parameter] public InputTextAlignment TextAlignment { get; set; } = InputTextAlignment.Start;
    [Parameter] public bool EnableTopLayer { get; set; }
    [Parameter] public RenderFragment? StartSlot { get; set; }
    [Parameter] public RenderFragment? EndSlot { get; set; }
    [Parameter] public EventCallback<string?> ValueChangeEvent { get; set; }
    [Parameter] public EventCallback<DateInputValidityState> ValidityStateChangeEvent { get; set; }
    [Parameter] public EventCallback<string?> ChangeEvent { get; set; }

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
    }

    [JSInvokable]
    public async Task ValueChange(JsonElement value)
    {
        Value = ReadNullableString(value);
        await ValueChangeEvent.InvokeAsync(Value);
        StateHasChanged();
    }

    [JSInvokable]
    public async Task Change(JsonElement value)
    {
        await ChangeEvent.InvokeAsync(ReadNullableString(value));
    }

    [JSInvokable]
    public async Task ValidityStateChange(JsonElement value)
    {
        var state = value.Deserialize<DateInputValidityState>(JsonOptions);
        if (state != null)
        {
            await ValidityStateChangeEvent.InvokeAsync(state);
        }
    }

    public Task FocusInput() =>
        _interop?.InvokeElementMethodAsync(Id, "focusInput")
        ?? throw new InvalidOperationException("The date input has not rendered yet.");

    private static string? ReadNullableString(JsonElement value) =>
        value.ValueKind is JsonValueKind.Null or JsonValueKind.Undefined ? null : value.GetString();

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
}
