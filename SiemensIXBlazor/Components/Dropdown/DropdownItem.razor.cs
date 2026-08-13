// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.Dropdown;

namespace SiemensIXBlazor.Components
{
    public partial class DropdownItem
    {
        [Parameter]
        public string? AriaLabelButton { get; set; }
        [Parameter]
        public string? AriaLabelIcon { get; set; }  
        [Parameter]
        public string? Label { get; set; }
        [Parameter]
        public string? Icon { get; set; }
        [Parameter]
        public bool Hover { get; set; } = false;
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public bool Checked { get; set; } = false;
        [Parameter]
        public DropdownItemRole ItemRole { get; set; } = DropdownItemRole.menuitem;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
