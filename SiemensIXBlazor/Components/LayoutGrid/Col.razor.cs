// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.LayoutGrid;

namespace SiemensIXBlazor.Components.LayoutGrid
{
    public partial class Col
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public ColumnSize? Size { get; set; }
        [Parameter]
        public ColumnSize? SizeLg { get; set; }
        [Parameter]
        public ColumnSize? SizeMd { get; set; }
        [Parameter]
        public ColumnSize? SizeSm { get; set; }
    }
}
