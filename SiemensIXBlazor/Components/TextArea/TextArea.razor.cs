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
using SiemensIXBlazor.Enums.TextArea;
using SiemensIXBlazor.Interops;
using SiemensIXBlazor.Objects;
using System.Text.Json;

namespace SiemensIXBlazor.Components.TextArea
{
    public partial class TextArea
    {
        private BaseInterop? _interop;

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        [Parameter]
        public string Value { get; set; } = "";

        [Parameter]
        public bool Disabled { get; set; } = false;

        [Parameter]
        public bool Readonly { get; set; } = false;

        [Parameter]
        public bool Required { get; set; } = false;

        [Parameter]
        public bool? ShowTextAsTooltip { get; set; }

        [Parameter]
        public string? HelperText { get; set; }

        [Parameter]
        public string? InfoText { get; set; }

        [Parameter]
        public string? InvalidText { get; set; }

        [Parameter]
        public string? Label { get; set; }

        [Parameter]
        public string? Name { get; set; }

        [Parameter]
        public string? Placeholder { get; set; }

        [Parameter]
        public string? ValidText { get; set; }

        [Parameter]
        public string? WarningText { get; set; }

        [Parameter]
        public int? MaxLength { get; set; }

        [Parameter]
        public int? MinLength { get; set; }

        [Parameter]
        public int? TextareaCols { get; set; }

        [Parameter]
        public int? TextareaRows { get; set; }

        [Parameter]
        public string? TextareaHeight { get; set; }

        [Parameter]
        public string? TextareaWidth { get; set; }

        [Parameter]
        public TextAreaResizeBehavior ResizeBehavior { get; set; } = TextAreaResizeBehavior.Both;

        [Parameter]
        public EventCallback IxBlurEvent { get; set; }

        [Parameter]
        public EventCallback<ValidityState> ValidityStateChangeEvent { get; set; }

        [Parameter]
        public EventCallback<string> ValueChangeEvent { get; set; }

        [Parameter]
        public EventCallback<string> IxChangeEvent { get; set; }

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

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                Task.Run(async () =>
                {
                    await _interop.AddEventListener(this, Id, "valueChange", "ValueChange");
                    await _interop.AddEventListener(this, Id, "ixChange", "IxChange");
                    await _interop.AddEventListener(this, Id, "ixBlur", "IxBlur", includeDetail: false);
                    await _interop.AddEventListener(this, Id, "validityStateChange", "ValidityStateChange");
                });
            }
        }

        [JSInvokable]
        public async Task ValueChange(JsonElement valueState)
        {
            string newValue = valueState.GetString() ?? "";
            Value = newValue;
            await ValueChangeEvent.InvokeAsync(newValue);
            await InvokeAsync(StateHasChanged);
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
        public async Task IxChange(JsonElement valueState)
        {
            await IxChangeEvent.InvokeAsync(valueState.GetString() ?? string.Empty);
        }
    }
}
