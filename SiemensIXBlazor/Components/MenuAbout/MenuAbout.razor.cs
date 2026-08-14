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
using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Components.MenuAbout
{
	public partial class MenuAbout : IXBaseComponent
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Active tab. Default value is null.
        /// </summary>
        [Parameter]
        public string? ActiveTabKey { get; set; }
        /// <summary>
        /// Aria label for close button. Default value is: 'Close About'
        /// </summary>
        [Parameter]
        public string AriaLabelCloseButton { get; set; } = "Close About";
        /// <summary>
        /// Label of first tab. Default value is: 'About & legal information'
        /// </summary>
        [Parameter]
        public string Label { get; set; } = "About & legal information";
        /// <summary>
        /// Whether to use slotted tabs instead of legacy MenuAboutItem components.
        /// </summary>
        [Parameter]
        public bool SuppressLegacyTabs { get; set; } = false;
        /// <summary>
        /// About and Legal closed event. Return value is: MouseEventArgs
        /// </summary>
        [Parameter]
        public EventCallback<MenuCloseEvent> ClosedEvent { get; set; }
        [Parameter]
        public EventCallback<string> TabChangedEvent { get; set; }
        
        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "close", "Closed");

                await _interop.AddEventListener(this, Id, "tabChange", "TabChanged");
            }
        }

        [JSInvokable]
        public async Task Closed(MenuCloseEvent args)
        {
            await ClosedEvent.InvokeAsync(args);
        }

        [JSInvokable]
        public async Task TabChanged(string tabKey)
        {
            await TabChangedEvent.InvokeAsync(tabKey);
        }

    }
}
