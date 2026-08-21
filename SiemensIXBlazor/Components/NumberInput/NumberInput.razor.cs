// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Enums.Input;
using SiemensIXBlazor.Interops;
using SiemensIXBlazor.Objects;
using System.Text.Json;

namespace SiemensIXBlazor.Components.NumberInput
{
    public partial class NumberInput
    {
        private BaseInterop? _interop;

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        [Parameter]
        public string? AllowedCharactersPattern { get; set; }

        [Parameter]
        public bool Disabled { get; set; } = false;

        [Parameter]
        public string? HelperText { get; set; }

        [Parameter]
        public string? InfoText { get; set; }

        [Parameter]
        public string? InvalidText { get; set; }

        [Parameter]
        public string? Label { get; set; }

        [Parameter]
        public object? Max { get; set; }

        [Parameter]
        public object? Min { get; set; }

        [Parameter]
        public string? Name { get; set; }

        [Parameter]
        public string? Pattern { get; set; }

        [Parameter]
        public string? Placeholder { get; set; }

        [Parameter]
        public bool Readonly { get; set; } = false;

        [Parameter]
        public bool Required { get; set; } = false;

        [Parameter]
        public bool? ShowStepperButtons { get; set; }

        [Parameter]
        public bool? ShowTextAsTooltip { get; set; }

        [Parameter]
        public object? Step { get; set; }

        [Parameter]
        public bool SuppressSubmitOnEnter { get; set; } = false;

        [Parameter]
        public TextAlignment TextAlignment { get; set; } = TextAlignment.End;

        [Parameter]
        public bool AllowEmptyValueChange { get; set; } = false;

        [Parameter]
        public string? ValidText { get; set; }

        [Parameter]
        public double? Value { get; set; } = 0;

        [Parameter]
        public string? WarningText { get; set; }

        [Parameter]
        public RenderFragment? StartSlot { get; set; }

        [Parameter]
        public RenderFragment? EndSlot { get; set; }

        [Parameter]
        public EventCallback IxBlurEvent { get; set; }

        [Parameter]
        public EventCallback<ValidityState> ValidityStateChangeEvent { get; set; }

        [Parameter]
        public EventCallback<double?> ValueChangeEvent { get; set; }

        [Parameter]
        public EventCallback<double?> IxChangeEvent { get; set; }

        public async Task FocusInputAsync()
        {
            _interop ??= new(JSRuntime);
            await _interop.InvokeVoidMethod(Id, "focusInput");
        }

        public async Task<IJSObjectReference?> GetNativeInputElementAsync()
        {
            _interop ??= new(JSRuntime);
            return await _interop.InvokeElementMethodAsync<IJSObjectReference>(Id, "getNativeInputElement");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop ??= new(JSRuntime);
                await _interop.AddEventListener(this, Id, "ixBlur", "IxBlur", includeDetail: false);
                await _interop.AddEventListener(this, Id, "validityStateChange", "ValidityStateChange");
                await _interop.AddEventListener(this, Id, "valueChange", "ValueChange");
                await _interop.AddEventListener(this, Id, "ixChange", "IxChange");
            }
        }

        [JSInvokable]
        public async Task IxBlur()
        {
            await IxBlurEvent.InvokeAsync();
        }

        [JSInvokable]
        public async Task ValidityStateChange(ValidityState validityState)
        {
            await ValidityStateChangeEvent.InvokeAsync(validityState);
        }

        [JSInvokable]
        public async Task ValueChange(JsonElement valueState)
        {
            double? newValue = valueState.ValueKind switch
            {
                JsonValueKind.Null or JsonValueKind.Undefined => null,
                JsonValueKind.Number => valueState.GetDouble(),
                JsonValueKind.String when double.TryParse(valueState.GetString(), out var value) => value,
                _ => null
            };

            Value = newValue;
            await ValueChangeEvent.InvokeAsync(newValue);
            await InvokeAsync(StateHasChanged);
        }

        [JSInvokable]
        public async Task IxChange(JsonElement valueState)
        {
            double? newValue = valueState.ValueKind switch
            {
                JsonValueKind.Null or JsonValueKind.Undefined => null,
                JsonValueKind.Number => valueState.GetDouble(),
                JsonValueKind.String when double.TryParse(valueState.GetString(), out var value) => value,
                _ => null
            };

            await IxChangeEvent.InvokeAsync(newValue);
        }
    }
}
