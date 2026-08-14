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
using SiemensIXBlazor.Enums.Button;
using SiemensIXBlazor.Interops;
using SiemensIXBlazor.Objects.DateDropdown;

namespace SiemensIXBlazor.Components;

public partial class DateDropdown
{
    private BaseInterop? _interop;
    private string? _dateRangeOptionsJson;

    [Parameter, EditorRequired] public string Id { get; set; } = string.Empty;
    [Parameter] public string DateRangeId { get; set; } = "custom";
    [Parameter] public DateDropdownOption[] DateRangeOptions { get; set; } = [];
    [Parameter] public string Format { get; set; } = "yyyy/LL/dd";
    [Parameter] public string From { get; set; } = string.Empty;
    [Parameter] public string I18nDone { get; set; } = "Done";
    [Parameter] public string I18nNoRange { get; set; } = "No range set";
    [Parameter] public string MaxDate { get; set; } = string.Empty;
    [Parameter] public string MinDate { get; set; } = string.Empty;
    [Parameter] public bool SingleSelection { get; set; }
    [Parameter] public string To { get; set; } = string.Empty;
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public bool EnableTopLayer { get; set; }
    [Parameter] public bool ShowWeekNumbers { get; set; }
    [Parameter] public string? Locale { get; set; }
    [Parameter] public int WeekStartIndex { get; set; }
    [Parameter] public bool Loading { get; set; }
    [Parameter] public ButtonVariant Variant { get; set; } = ButtonVariant.primary;
    [Parameter] public EventCallback<DateDropdownResponse> DateRangeChangeEvent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        _interop ??= new BaseInterop(JsRuntime);

        var optionsJson = JsonSerializer.Serialize(DateRangeOptions);
        if (_dateRangeOptionsJson != optionsJson)
        {
            _dateRangeOptionsJson = optionsJson;
            await _interop.SetElementProperty(Id, "dateRangeOptions", DateRangeOptions);
        }

        if (firstRender)
        {
            await _interop.AddEventListener(this, Id, "dateRangeChange", nameof(DateRangeChange));
        }
    }

    public async Task<DateDropdownResponse> GetDateRange()
    {
        if (_interop == null)
        {
            throw new InvalidOperationException("The date dropdown has not rendered yet.");
        }

        return await _interop.InvokeElementMethodAsync<DateDropdownResponse>(Id, "getDateRange")
            ?? new DateDropdownResponse();
    }

    [JSInvokable]
    public async Task DateRangeChange(JsonElement data)
    {
        var response = data.Deserialize<DateDropdownResponse>(JsonOptions);
        if (response != null)
        {
            await DateRangeChangeEvent.InvokeAsync(response);
        }
    }

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
}
