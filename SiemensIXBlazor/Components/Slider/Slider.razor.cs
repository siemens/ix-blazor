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
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components.Slider
{
    public partial class Slider
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public dynamic? Error { get; set; }
        [Parameter]
        public double[]? Marker { get; set; }
        [Parameter]
        public double Max { get; set; } = 100;
        [Parameter]
        public double Min { get; set; } = 0;
        [Parameter]
        public double? Step { get; set; }
        [Parameter]
        public bool Trace { get; set; } = false;
        [Parameter]
        public double TraceReference { get; set; } = 0;
        [Parameter]
        public double Value { get; set; } = 0;
        [Parameter]
        public EventCallback<double> ValueChangeEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "valueChange", "ValueChanged");
            }
        }

        [JSInvokable]
        public async void ValueChanged(double value)
        {
            await ValueChangeEvent.InvokeAsync(value);
        }
    }
}
