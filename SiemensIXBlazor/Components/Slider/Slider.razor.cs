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

namespace SiemensIXBlazor.Components.Slider
{
    public partial class Slider
    {
        private Lazy<Task<IJSObjectReference>>? _moduleTask;

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public dynamic? Error { get; set; }
        private string? ErrorAsString => Error switch
        {
            bool b => b.ToString().ToLowerInvariant(),
            string s => s,
            null => null,
            _ => Error?.ToString()
        };

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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _moduleTask = new(() => JSRuntime.InvokeAsync<IJSObjectReference>(
                    "import", "./_content/Siemens.IX.Blazor/js/siemens-ix/interops/sliderInterop.js").AsTask());

                var module = await _moduleTask.Value;

                await module.InvokeAsync<string>(
                    "listenEvent",
                    DotNetObjectReference.Create(this),
                    Id,
                    "valueChange",
                    "ValueChanged"
                );

                if (Marker is { Length: > 0 })
                {
                    await module.InvokeAsync<object>("setMarker", Id, Marker);
                }
            }
        }

        [JSInvokable]
        public async Task ValueChanged(double value)
        {
            await ValueChangeEvent.InvokeAsync(value);
        }

        public async ValueTask DisposeAsync()
        {
            if (_moduleTask?.IsValueCreated == true)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
