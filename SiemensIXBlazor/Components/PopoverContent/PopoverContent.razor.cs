// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components;

public partial class PopoverContent
{
    [Parameter, EditorRequired]
    public string Id { get; set; } = string.Empty;

    [Parameter]
    public bool NoPadding { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
