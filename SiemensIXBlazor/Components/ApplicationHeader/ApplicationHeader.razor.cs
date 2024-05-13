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
    public partial class ApplicationHeader
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public string? Name { get; set; }
    }
}
