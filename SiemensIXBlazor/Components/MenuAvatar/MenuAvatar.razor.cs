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

namespace SiemensIXBlazor.Components.MenuAvatar
{
    public partial class MenuAvatar
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string? Bottom { get; set; }
        [Parameter]
        public string I18NLogout { get; set; } = "Logout";
        [Parameter]
        public string? Image { get; set; }
        [Parameter]
        public string? Initials { get; set; }
        [Parameter]
        public bool HideLogoutButton { get; set; } = false;
        [Parameter]
        public string? Top { get; set; }
        [Parameter]
        public EventCallback LogoutClickedEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "logoutClick", "LogoutClicked");
            }
        }

        [JSInvokable]
        public async Task LogoutClicked()
        {
            await LogoutClickedEvent.InvokeAsync();
        }
    }
}
