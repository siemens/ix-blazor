// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Components;

namespace SiemensIXBlazor.Components.HelperText
{
    public abstract class HelperTextBase : IXBaseComponent
    {
        [Parameter]
        public string? Id { get; set; }

        [Parameter]
        public string? HtmlFor { get; set; }

        [Parameter]
        public string? HelperText { get; set; }

        [Parameter]
        public string? InfoText { get; set; }

        [Parameter]
        public string? InvalidText { get; set; }

        [Parameter]
        public string? ValidText { get; set; }

        [Parameter]
        public string? WarningText { get; set; }
    }
}
