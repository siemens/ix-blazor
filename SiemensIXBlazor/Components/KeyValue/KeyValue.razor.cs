// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.KeyValue;

namespace SiemensIXBlazor.Components
{
	public partial class KeyValue
	{
        /// <summary>
        /// Optional custom value content rendered through the official custom-value slot.
        /// </summary>
        [Parameter]
        public RenderFragment? CustomValue { get; set; }
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
        [Parameter, EditorRequired]
		public string Label { get; set; } = string.Empty;
        /// <summary>
        /// Optional key value label position - 'top' or 'left'
        /// </summary>
        [Parameter]
		public KeyValueLabelPosition LabelPosition { get; set; } = KeyValueLabelPosition.top;
        /// <summary>
        /// Optional key value text value
        /// </summary>
		[Parameter]
		public string? Value { get; set; }
	}
}
