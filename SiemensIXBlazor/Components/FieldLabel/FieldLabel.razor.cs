// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components.FieldLabel
{
    public partial class FieldLabel
    {
        [Parameter]
        public string? Id { get; set; }

        [Parameter]
        public string? HtmlFor { get; set; }

        [Parameter]
        public bool? Required { get; set; }

        [Parameter]
        public bool IsInvalid { get; set; } = false;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
