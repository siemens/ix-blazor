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
using SiemensIXBlazor.Enums.Chip;
using SiemensIXBlazor.Enums.Tooltip;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class Tooltip
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public bool Interactive { get; set; } = false;
        [Parameter]
        public string? TitleContent { get; set; }
        [Parameter]
        public TooltipVariant Placement { get; set; } = TooltipVariant.top;
        [Parameter]
        public string? For { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        private BaseInterop _interop;
    }
}

