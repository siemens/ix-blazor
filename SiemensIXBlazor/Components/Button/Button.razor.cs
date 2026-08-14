// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.Button;

namespace SiemensIXBlazor.Components
{
    public partial class Button
    {
        [Parameter]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public ButtonVariant Variant { get; set; } = ButtonVariant.primary;
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public string? Icon { get; set; }
        [Parameter]
        public string? IconRight { get; set; }
        [Parameter]
        public bool Loading { get; set; } = false;
        [Parameter]
        public ButtonType Type { get; set; } = ButtonType.Button;
        [Parameter]
        public string? Form { get; set; }
        [Parameter]
        public string? Href { get; set; }
        [Parameter]
        public ButtonTarget Target { get; set; } = ButtonTarget._self;
        [Parameter]
        public string? Rel { get; set; }
        [Parameter]
        public EventCallback ClickEvent { get; set; }

        private Task Clicked()
        {
            return ClickEvent.InvokeAsync();
        }
    }
}
