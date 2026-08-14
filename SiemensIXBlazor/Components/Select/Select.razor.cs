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
    private string? _valueSnapshot;
    private bool _valueChanged = true;

    [Parameter, EditorRequired]
    public string Id { get; set; } = string.Empty;
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public bool AllowClear { get; set; } = false;
    [Parameter]
    public string? AriaLabelClearIconButton { get; set; }   
    [Parameter]
    public string AriaLabelAddItem { get; set; } = "Add item";
    [Parameter]
    public bool Disabled { get; set; } = false;
    [Parameter]
    public bool Editable { get; set; } = false;
    [Parameter]
    public bool EnableTopLayer { get; set; } = false;
    [Parameter]
    public string I18nPlaceholder { get; set; } = "Select an option";
    [Parameter]
    public string I18nPlaceholderEditable { get; set; } = "Type of select option";
    [Parameter]
    public string I18nSelectListHeader { get; set; } = "Select an option";
    [Parameter]
    public SelectMode Mode { get; set; } = SelectMode.Single;
    [Parameter]
    public bool Readonly { get; set; } = false;
    [Parameter]
    public object? Value { get; set; }
    [Parameter]
    public bool HideListHeader { get; set; } = false;
    [Parameter]
    public string I18nNoMatches { get; set; } = "No matches";
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
    public EventCallback<object?> ValueChangeEvent { get; set; }
    [Parameter]
    public EventCallback<string> InputChangeEvent { get; set; }
    [Parameter]
    public EventCallback<object> BlurEvent { get; set; }
    [Parameter]
    public string I18nMoreItems { get; set; } = "{count} more";
    [Parameter]
    public string I18nAllSelected { get; set; } = "All";
    [Parameter]
    public string I18nRemoveSelectedItem { get; set; } = "Remove";
    [Parameter]
    public bool CollapseMultipleSelection { get; set; } = false;

    private BaseInterop? _interop;

    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        TrackValueChange();
        if (firstRender)
        {
            _interop = new(JSRuntime);
            await _interop.AddEventListener(this, Id, "addItem", nameof(AddItemChanged));
            await _interop.AddEventListener(this, Id, "valueChange", nameof(ValueChanged));
            await _interop.AddEventListener(this, Id, "inputChange", nameof(InputChanged));
            await _interop.AddEventListener(this, Id, "ixBlur", nameof(Blurred));
        }

        if (_valueChanged)
        {
            _valueChanged = false;
            await _interop!.SetElementProperty(Id, "value", Value ?? string.Empty);
        }
    }

    [JSInvokable]
    public async Task AddItemChanged(JsonElement label)
    {
        if (AddItemEvent.HasDelegate)
        {
            await AddItemEvent.InvokeAsync(GetStringPayload(label));
        }
    }

    [JSInvokable]
    public async Task InputChanged(JsonElement input)
    {
        if (InputChangeEvent.HasDelegate)
        {
            await InputChangeEvent.InvokeAsync(GetStringPayload(input));
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

    private void TrackValueChange()
    {
        var snapshot = JsonSerializer.Serialize(Value);
        if (!string.Equals(_valueSnapshot, snapshot, StringComparison.Ordinal))
        {
            _valueSnapshot = snapshot;
            _valueChanged = true;
        }
    }

    private static string GetStringPayload(JsonElement payload)
    {
        if (payload.ValueKind == JsonValueKind.String)
        {
            return payload.GetString() ?? string.Empty;
        }

        if (payload.ValueKind == JsonValueKind.Object)
        {
            foreach (var propertyName in new[] { "value", "label", "text" })
            {
                if (payload.TryGetProperty(propertyName, out var property) &&
                    property.ValueKind == JsonValueKind.String)
                {
                    return property.GetString() ?? string.Empty;
                }
            }
        }

        return string.Empty;
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
