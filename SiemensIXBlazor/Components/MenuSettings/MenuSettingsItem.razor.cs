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
using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Components.MenuSettings
{
    public partial class MenuSettingsItem
    {
        [Parameter]
        public string? Label { get; set; }
        [Parameter, EditorRequired]
        public string TabKey { get; set; } = string.Empty;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public EventCallback<MenuLabelChangeEvent> LabelChangedEvent { get; set; }

        private BaseInterop? _interop;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && !string.IsNullOrWhiteSpace(Id))
            {
                _interop = new(JSRuntime);
                await _interop.AddEventListener(this, Id, "labelChange", "LabelChanged");
            }
        }

        [JSInvokable]
        public async Task LabelChanged(MenuLabelChangeEvent args)
        {
            await LabelChangedEvent.InvokeAsync(args);
        }
    }
}
