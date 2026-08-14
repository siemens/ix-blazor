// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.RangeField;

namespace SiemensIXBlazor.Components.RangeField;

public partial class RangeField
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public RangeFieldType? Type { get; set; }
    [Parameter] public bool HideArrow { get; set; }
}
