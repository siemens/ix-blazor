// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.ProgressIndicator;

namespace SiemensIXBlazor.Components
{
    public partial class ProgressIndicator
    {
        /// <summary>
        /// The id attribute for the progress indicator.
        /// </summary>
        [Parameter]
        public string? Id { get; set; }

        /// <summary>
        /// The helper text for the progress indicator.
        /// </summary>
        [Parameter]
        public string? HelperText { get; set; }

        /// <summary>
        /// The label for the progress indicator.
        /// </summary>
        [Parameter]
        public string? Label { get; set; }

        /// <summary>
        /// The maximum value of the progress indicator.
        /// </summary>
        [Parameter]
        public double Max { get; set; } = 100;

        /// <summary>
        /// The minimum value of the progress indicator.
        /// </summary>
        [Parameter]
        public double Min { get; set; } = 0;

        /// <summary>
        /// Show the helper text as a tooltip
        /// </summary>
        [Parameter]
        public bool ShowTextAsTooltip { get; set; } = false;

        /// <summary>
        /// The size of the progress indicator.
        /// </summary>
        [Parameter]
        public ProgressIndicatorSize Size { get; set; } = ProgressIndicatorSize.md;

        /// <summary>
        /// The state of the progress indicator. This is used to indicate the current state of the progress indicator.
        /// </summary>
        [Parameter]
        public ProgressIndicatorStatus Status { get; set; } = ProgressIndicatorStatus._default;

        /// <summary>
        /// The text alignment for the helper text. Can be 'left', 'center', or 'right'.
        /// </summary>
        [Parameter]
        public ProgressIndicatorTextAlignment TextAlignment { get; set; } = ProgressIndicatorTextAlignment.left;

        /// <summary>
        /// The type of progress indicator to use.
        /// </summary>
        [Parameter]
        public ProgressIndicatorType Type { get; set; } = ProgressIndicatorType.linear;

        /// <summary>
        /// The value of the progress indicator.
        /// </summary>
        [Parameter]
        public double Value { get; set; } = 0;
    }
}
