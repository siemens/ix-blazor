// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components.MenuCategory
{
    public partial class MenuCategory : IXBaseComponent
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public string? Icon { get; set; }
        [Parameter]
        public string? Label { get; set; }
        [Parameter]
        public int? Notifications { get; set; }
    }
}
