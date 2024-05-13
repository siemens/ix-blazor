// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components.LayoutGrid
{
    public partial class Col
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public string? Size { get; set; }
        [Parameter]
        public string? SizeLg { get; set; }
        [Parameter]
        public string? SizeMd { get; set; }
        [Parameter]
        public string? SizeSm { get; set; }
    }
}
