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

namespace SiemensIXBlazor.Components
{
    public partial class Toggle : IAsyncDisposable
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public bool Checked { get; set; } = false;
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public bool HideText { get; set; } = false;
        [Parameter]
        public bool Indeterminate { get; set; } = false;
        [Parameter]
        public string? Name { get; set; }
        [Parameter]
        public bool Required { get; set; }
        [Parameter]
        public string TextIndeterminate { get; set; } = "Mixed";
        [Parameter]
        public string TextOff { get; set; } = "Off";
        [Parameter]
        public string TextOn { get; set; } = "On";
        [Parameter]
        public string Value { get; set; } = "on";
        [Parameter]
        public EventCallback<bool> CheckedChangeEvent { get; set; }
        [Parameter]
        public EventCallback IxBlurEvent { get; set; }

        private BaseInterop? _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "checkedChange", nameof(CheckedChanged));
                await _interop.AddEventListener(this, Id, "ixBlur", nameof(Blurred), includeDetail: false);
            }
        }

        [JSInvokable]
        public Task CheckedChanged(bool value)
        {
            Checked = value;
            return CheckedChangeEvent.InvokeAsync(value);
        }

        [JSInvokable]
        public Task Blurred() => IxBlurEvent.InvokeAsync();

        public async ValueTask DisposeAsync()
        {
            if (_interop is not null)
            {
                await _interop.DisposeAsync();
            }
        }
    }
}
