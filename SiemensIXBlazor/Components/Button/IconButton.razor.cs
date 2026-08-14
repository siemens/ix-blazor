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
    public partial class IconButton
    {
        [Parameter]
        public string? IconColor { get; set; }
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public string? Icon { get; set; }
        [Parameter]
        public bool Loading { get; set; } = false;
        [Parameter]
        public bool Oval { get; set; } = false;
        [Parameter]
        public IconButtonSize Size { get; set; } = IconButtonSize._24;
        [Parameter]
        public ButtonType Type { get; set; } = ButtonType.Button;
        [Parameter]
        public ButtonVariant Variant { get; set; } = ButtonVariant.subtle_primary;
        [Parameter]
        public EventCallback ClickEvent { get; set; }

        private Task Clicked()
        {
            return ClickEvent.InvokeAsync();
        }
    }
}
