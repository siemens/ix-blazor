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
using SiemensIXBlazor.Enums.MessageBar;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class MessageBar
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public bool Persistent { get; set; } = false;

        [Parameter]
        public MessageBarType Type { get; set; } = MessageBarType.Info;
        [Parameter]
        public EventCallback ClosedChangeEvent { get; set; }
        [Parameter]
        public EventCallback CloseAnimationCompletedEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "closedChange", "ClosedChange");
                await _interop.AddEventListener(this, Id, "closeAnimationCompleted", "CloseAnimationCompleted");
            }
        }

        [JSInvokable]
        public async Task ClosedChange()
        {
            await ClosedChangeEvent.InvokeAsync();
        }

        [JSInvokable]
        public async Task CloseAnimationCompleted()
        {
            await CloseAnimationCompletedEvent.InvokeAsync();
        }
    }
}
