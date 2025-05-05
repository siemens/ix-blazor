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
using Microsoft.JSInterop;

namespace SiemensIXBlazor.Components
{
    public partial class FlipTile
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public string? State { get; set; }
        [Parameter]
        public dynamic Height { get; set; } = 15.125;
        [Parameter]
        public dynamic Width { get; set; } = 16;

        [Parameter]
        public int Index { get; set; } = 0;

        [Parameter]
       public EventCallback<int> ToggleEvent { get; set; }

        private BaseInterop _interop;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);
                await _interop.AddEventListener(this, Id, "toggle", "ToggleClicked");

            }
        }

        [JSInvokable]
        public async Task ToggleClicked(int index)
        {
            await ToggleEvent.InvokeAsync(index);
        }
    }
}
