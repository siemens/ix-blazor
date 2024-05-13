// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components
{
    public partial class EventList
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public bool Animated { get; set; } = true;
        [Parameter]
        public bool? Chevron { get; set; }
        [Parameter]
        public bool Compact { get; set; } = false;
        [Parameter]
        public string ItemHeight { get; set; } = "S";
    }
}
