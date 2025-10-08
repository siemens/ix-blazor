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
using SiemensIXBlazor.Enums.DropdownButton;

namespace SiemensIXBlazor.Components
{
    public partial class DropdownButton
    {
        [Parameter]
        public string? AriaLabelDropdownButton { get; set; }    
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public bool Ghost { get; set; } = false;
        [Parameter]
        public string? Icon { get; set; }
        [Parameter]
        public string Label { get; set; } = string.Empty;
        [Parameter]
        public bool Outline { get; set; } = false;
        [Parameter]
        public DropdownButtonPlacement? Placement { get; set; }
        [Parameter]
        public ButtonVariant Variant { get; set; } = ButtonVariant.primary;
        [Parameter]
        public DropdownButtonCloseBehavior CloseBehavior { get; set; } = DropdownButtonCloseBehavior.both;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        
    }
}
