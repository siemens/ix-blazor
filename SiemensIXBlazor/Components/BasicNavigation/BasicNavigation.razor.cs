// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.BasicNavigation;

namespace SiemensIXBlazor.Components.BasicNavigation
{
    public partial class BasicNavigation
    {
        /// <summary>
        /// Application name. Default value is null.
        /// </summary>
        [Parameter]
        public string? ApplicationName { get; set; }
        /// <summary>
        /// Hide application header. Will disable responsive feature of basic navigation. Default value is: false
        /// </summary>
        [Parameter]
        public bool HideHeader { get; set; } = false;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public Breakpoint? ForceBreakpoint { get; set; }
    }
}
