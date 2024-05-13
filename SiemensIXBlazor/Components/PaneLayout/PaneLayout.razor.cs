// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.Pane;

namespace SiemensIXBlazor.Components
{
    public partial class PaneLayout
    {
        [Parameter]
        public bool Borderless { get; set; } = false;
        [Parameter]
        public string Layout { get; set; } = "full-vertical";
        [Parameter]
        public PaneVariant Variant { get; set; } = PaneVariant.inline;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

    }
}
