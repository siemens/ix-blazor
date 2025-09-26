﻿// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Enums.ExpandingSearch;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class ExpandingSearch
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string? AriaLabelClearIconButton { get; set; }
        [Parameter]
        public string? AriaLabelSearchIconButton { get; set; }
        [Parameter]
        public string? AriaLabelSearchInput { get; set; }   
        [Parameter] 
        public string Icon { get; set; } = "search";
        [Parameter]
        public string Placeholder { get; set; } = "Enter text here";
        [Parameter]
        public string Value { get; set; } = string.Empty;
        [Parameter]
        public bool Ghost { get; set; } = true;
        [Parameter]
        public bool Outline { get; set; } = false;
        [Parameter]
        public bool FullWidth { get; set; } = false;
        [Parameter]
        public ExpandingSearchVariant Variant { get; set; } = ExpandingSearchVariant.primary;
        [Parameter]
        public EventCallback<string> ValueChangedEvent { get; set; }

        private BaseInterop? _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "valueChange", "ValueChanged");
            }
        }

        [JSInvokable]
        public async void ValueChanged(string value)
        {
            Value = value;
            await ValueChangedEvent.InvokeAsync(value);
        }
    }
}
