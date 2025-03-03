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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using SiemensIXBlazor.Interops;
using SiemensIXBlazor.Objects.DateDropdown;
using System.Text.Json;

namespace SiemensIXBlazor.Components;

public partial class DateDropdown
{
    private BaseInterop _interop = null!;
    private Lazy<Task<IJSObjectReference>>? _moduleTask;

    [Parameter, EditorRequired]
    public string Id { get; set; } = string.Empty;
    [Parameter]
    public bool CustomRangeAllowed { get; set; } = true;
    [Parameter]
    public string DateRangeId { get; set; } = "custom";
    [Parameter]
    public EventCallback<string> DateRangeIdChanged { get; set; }
    [Parameter]
    public DateDropdownOption[]? DateRangeOptions { get; set; }
    [Parameter]
    public string Format { get; set; } = "yyyy/LL/dd";
    [Parameter]
    public string? From { get; set; }
    [Parameter]
    public string I18NCustomItem { get; set; } = "Custom...";
    [Parameter]
    public string I18NDone { get; set; } = "Done";
    [Parameter]
    public string I18NNoRange { get; set; } = "No range set";
    [Parameter]
    public string? MaxDate { get; set; }
    [Parameter]
    public string? MinDate { get; set; }
    [Parameter]
    public bool Range { get; set; } = true;
    [Parameter]
    public string? To { get; set; }
    [Parameter]
    public bool Disabled { get; set; } = false;
    [Parameter]
    public string? Locale { get; set; }
    [Parameter]
    public int? WeekStartIndex { get; set; } = 0;
    [Parameter]
    public EventCallback<DateDropdownResponse> DateRangeChangeEvent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await InitialParameterAsync("setDateRangeOptions", DateRangeOptions);
            _interop = new BaseInterop(JsRuntime);

            await _interop.AddEventListener(this, Id, "dateRangeChange", "DateRangeChange");
        }
    }

    private async Task InitialParameterAsync(string functionName, object? param)
    {
        _moduleTask = new Lazy<Task<IJSObjectReference>>(() => JsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Siemens.IX.Blazor/js/siemens-ix/interops/dateDropdownInterop.js").AsTask());

        var dateRangeOptions = JsonConvert.SerializeObject(param, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });

        var module = await _moduleTask.Value;
        if (module != null)
            await module.InvokeVoidAsync(functionName, Id, dateRangeOptions);
    }

    [JSInvokable]
    public async void DateRangeChange(JsonElement data)
    {
        var jsonDataText = data.GetRawText();
        var jsonData = JObject.Parse(jsonDataText)
            .ToObject<DateDropdownResponse>();

        await DateRangeChangeEvent.InvokeAsync(jsonData);
    }
}
