// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Enums.Select;
using SiemensIXBlazor.Interops;
using System.Text.Json;

namespace SiemensIXBlazor.Components;

public partial class Select
{
    [Parameter, EditorRequired]
    public string Id { get; set; } = string.Empty;
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public bool AllowClear { get; set; } = false;
    [Parameter]
    public string? AriaLabelChevronDownIconButton { get; set; }
    [Parameter]
    public string? AriaLabelClearIconButton { get; set; }   
    [Parameter]
    public bool Disabled { get; set; } = false;
    [Parameter]
    public bool Editable { get; set; } = false;
    [Parameter]
    public string i18nPlaceholder { get; set; } = "Select an option";
    [Parameter]
    public string i18nPlaceholderEditable { get; set; } = "Type of select option";
    [Parameter]
    public string i18nSelectListHeader { get; set; } = "Please select an option";
    [Parameter]
    public SelectMode Mode { get; set; } = SelectMode.Single;
    [Parameter]
    public bool Readonly { get; set; } = false;
    [Parameter]
    public string? Value { get; set; }
    [Parameter]
    public bool HideListHeader { get; set; } = false;
    [Parameter]
    public string I18NNoMatches { get; set; } = "No matches";
    [Parameter]
    public string? DropdownMaxWidth { get; set; }
    [Parameter]
    public string? DropdownWidth { get; set; }
    [Parameter]
    public string? HelperText { get; set; }
    [Parameter]
    public string? InfoText { get; set; }
    [Parameter]
    public string? InvalidText { get; set; }
    [Parameter]
    public string? ValidText { get; set; }
    [Parameter]
    public string? WarningText { get; set; }
    [Parameter]
    public string? Label { get; set; }
    [Parameter]
    public string? Name { get; set; }
    [Parameter]
    public bool Required { get; set; } = false;
    [Parameter]
    public bool ShowTextAsTooltip { get; set; } = false;
    [Parameter]
    public EventCallback<string> AddItemEvent { get; set; }
    [Parameter]
    public EventCallback<dynamic> ValueChangeEvent { get; set; }
    [Parameter]
    public EventCallback<string> InputChangeEvent { get; set; }
    [Parameter]
    public EventCallback<object> BlurEvent { get; set; }

    private BaseInterop? _interop;

    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _interop = new(JSRuntime);
            await _interop.AddEventListener(this, Id, "addItem", nameof(AddItemChanged));
            await _interop.AddEventListener(this, Id, "valueChange", nameof(ValueChanged));
            await _interop.AddEventListener(this, Id, "inputChange", nameof(InputChanged));
            await _interop.AddEventListener(this, Id, "ixBlur", nameof(Blurred));
        }
    }

    [JSInvokable]
    public async Task AddItemChanged(string label)
    {
        if (AddItemEvent.HasDelegate)
        {
            await AddItemEvent.InvokeAsync(label);
        }
    }

    [JSInvokable]
    public async Task InputChanged(string input)
    {
        if (InputChangeEvent.HasDelegate)
        {
            await InputChangeEvent.InvokeAsync(input);
        }
    }

    [JSInvokable]
    public async Task ValueChanged(JsonElement labels)
    {
        if (labels.ValueKind == JsonValueKind.String)
        {
            await ValueChangeEvent.InvokeAsync(labels.GetString());
        }
        else if (labels.ValueKind == JsonValueKind.Array)
        {
            var labelArray = labels.Deserialize<string[]>();
            await ValueChangeEvent.InvokeAsync(labelArray);
        }
    }

    [JSInvokable]
    public async Task Blurred()
    {
        if (BlurEvent.HasDelegate)
        {
            await BlurEvent.InvokeAsync(null);
        }
    }
}
