// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components.CustomField
{
    public partial class CustomField
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public string? HelperText { get; set; }

        [Parameter]
        public string? InfoText { get; set; }

        [Parameter]
        public string? InvalidText { get; set; }

        [Parameter]
        public string? Label { get; set; }

        [Parameter]
        public bool Required { get; set; } = false;

        [Parameter]
        public bool? ShowTextAsTooltip { get; set; }

        [Parameter]
        public string? ValidText { get; set; }

        [Parameter]
        public string? WarningText { get; set; }

    }
}
