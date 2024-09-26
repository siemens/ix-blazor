// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components.MenuAvatar
{
    public partial class MenuAvatarItem
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string? Icon { get; set; }
        [Parameter]
        public string? Label { get; set; }
        [Parameter]
        public EventCallback<MouseEventArgs> ItemClickedEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "itemClick", "ItemClicked");
            }
        }

        [JSInvokable]
        public async Task ItemClicked(MouseEventArgs args)
        {
            await ItemClickedEvent.InvokeAsync(args);
        }
    }
}
