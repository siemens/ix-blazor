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

namespace SiemensIXBlazor.Components.TimeInput;

public partial class TimeInput
{
    private BaseInterop? _interop;

    [Parameter, EditorRequired] public string Id { get; set; } = string.Empty;
    [Parameter] public string? Name { get; set; }
    [Parameter] public string? Placeholder { get; set; }
    [Parameter] public string Value { get; set; } = string.Empty;
    [Parameter] public string Format { get; set; } = "TT";
    [Parameter] public string? MinTime { get; set; }
    [Parameter] public string? MaxTime { get; set; }
    [Parameter] public bool? Required { get; set; }
    [Parameter] public string? HelperText { get; set; }
    [Parameter] public string? Label { get; set; }
    [Parameter] public string? InvalidText { get; set; }
    [Parameter] public bool Readonly { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public string? InfoText { get; set; }
    [Parameter] public string? WarningText { get; set; }
    [Parameter] public string? ValidText { get; set; }
    [Parameter] public bool? ShowTextAsTooltip { get; set; }
    [Parameter] public string I18nErrorTimeUnparsable { get; set; } = "Time is not valid";
    [Parameter] public int HourInterval { get; set; } = 1;
    [Parameter] public int MinuteInterval { get; set; } = 1;
    [Parameter] public int SecondInterval { get; set; } = 1;
    [Parameter] public int MillisecondInterval { get; set; } = 100;
    [Parameter] public string I18nSelectTime { get; set; } = "Confirm";
    [Parameter] public string I18nTime { get; set; } = "Time";
    [Parameter] public string I18nHourColumnHeader { get; set; } = "hr";
    [Parameter] public string I18nMinuteColumnHeader { get; set; } = "min";
    [Parameter] public string I18nSecondColumnHeader { get; set; } = "sec";
    [Parameter] public string I18nMillisecondColumnHeader { get; set; } = "ms";
    [Parameter] public bool SuppressSubmitOnEnter { get; set; }
    [Parameter] public bool HideHeader { get; set; }
    [Parameter] public InputTextAlignment TextAlignment { get; set; } = InputTextAlignment.Start;
    [Parameter] public bool EnableTopLayer { get; set; }
    [Parameter] public string AriaLabelTimeToggleButton { get; set; } = "Toggle time picker";
    [Parameter] public RenderFragment? StartSlot { get; set; }
    [Parameter] public RenderFragment? EndSlot { get; set; }
    [Parameter] public EventCallback<string> ValueChangeEvent { get; set; }
    [Parameter] public EventCallback<TimeInputValidityState> ValidityStateChangeEvent { get; set; }
    [Parameter] public EventCallback<string> ChangeEvent { get; set; }

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
        Value = ReadString(value);
        await ValueChangeEvent.InvokeAsync(Value);
        StateHasChanged();
    }

    [JSInvokable]
    public async Task Change(JsonElement value)
    {
        await ChangeEvent.InvokeAsync(ReadString(value));
    }

    [JSInvokable]
    public async Task ValidityStateChange(JsonElement value)
    {
        var state = value.Deserialize<TimeInputValidityState>(JsonOptions);
        if (state != null)
        {
            await ValidityStateChangeEvent.InvokeAsync(state);
        }
    }

    public Task FocusInput() =>
        _interop?.InvokeElementMethodAsync(Id, "focusInput")
        ?? throw new InvalidOperationException("The time input has not rendered yet.");

    public Task<IJSObjectReference?> GetNativeInputElementAsync() =>
        _interop?.InvokeElementMethodAsync<IJSObjectReference>(Id, "getNativeInputElement")
        ?? throw new InvalidOperationException("The time input has not rendered yet.");

    private static string ReadString(JsonElement value) =>
        value.ValueKind is JsonValueKind.Null or JsonValueKind.Undefined ? string.Empty : value.GetString() ?? string.Empty;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
}
