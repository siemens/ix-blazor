// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components.Avatar
{
    public partial class Avatar
    {
        [Parameter]
        public string? Image { get; set; }
        [Parameter]
        public string? Initials { get; set; }
    }
}
