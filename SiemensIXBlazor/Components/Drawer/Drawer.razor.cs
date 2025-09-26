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
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class Drawer
    {
        [Parameter] 
        public RenderFragment? ChildContent { get; set; }
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public string? AriaLabelCloseButton { get; set; }   
        [Parameter]
        public bool CloseOnClickOutside { get; set; } = true;
        [Parameter]
        public bool FullHeight { get; set; } = false;
        [Parameter]
        public int MaxWidth { get; set; } = 28;
        [Parameter]
        public static int MinWidth { get; set; } = 16;
        [Parameter]
        public bool Show { get; set; } = false;
        [Parameter]
        public int Width { get; set; } = MinWidth;
        [Parameter]
        public EventCallback ClosedEvent { get; set; }
        [Parameter]
        public EventCallback OpenedEvent { get; set; }

        private BaseInterop _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                await _interop.AddEventListener(this, Id, "drawerClose", "Closed");
                await _interop.AddEventListener(this, Id, "open", "Opened");
            }
        }

        [JSInvokable]
        public async Task Closed()
        {
            await ClosedEvent.InvokeAsync();
        }

        [JSInvokable]
        public async Task Opened()
        {
            await OpenedEvent.InvokeAsync();
        }
    }
}
