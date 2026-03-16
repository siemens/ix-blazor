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

namespace SiemensIXBlazor.Components
{
	public partial class KeyValue
	{
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        /// <summary>
        /// Optional key value icon
        /// </summary>
        [Parameter]
		public string? Icon { get; set; }

        [Parameter]
        public string? AriaLabelIcon { get; set; }  

        /// <summary>
        /// Key value label
        /// </summary>
        [Parameter]
		public string? Label { get; set; }
        /// <summary>
        /// Optional key value label position - 'top' or 'left'
        /// </summary>
        [Parameter]
		public string LabelPosition { get; set; } = "top";
        /// <summary>
        /// Optional key value text value
        /// </summary>
		[Parameter]
		public string? Value { get; set; }
	}
}

