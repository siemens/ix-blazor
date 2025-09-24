// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.PushCard;

namespace SiemensIXBlazor.Components
{
	public partial class ActionCard
	{
        [Parameter]
        public string? AriaLabelCard { get; set; }

        [Parameter]
        public string? AriaLabelIcon { get; set; }  
        /// <summary>
        /// Card heading
        /// </summary>
        [Parameter]
        public string? Heading { get; set; }
        /// <summary>
        /// Card icon
        /// </summary>
        [Parameter]
        public string? Icon { get; set; }
        /// <summary>
        /// Card selection
        /// </summary>
        [Parameter]
        public bool Selected { get; set; } = false;
        /// <summary>
        /// Card subheading
        /// </summary>
        [Parameter]
        public string? SubHeading { get; set; }
        /// <summary>
        /// Card variant
        /// </summary>
        [Parameter]
        public PushCardVariant Variant { get; set; } = PushCardVariant.outline;
    }
}

