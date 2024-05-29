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
using SiemensIXBlazor.Enums.Pill;

namespace SiemensIXBlazor.Components
{
	public partial class Pill
	{
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
		public bool AlignLeft { get; set; } = false;
		[Parameter]
		public string? Background { get; set; }
		[Parameter]
		public string? Color { get; set; }
		[Parameter]
		public string? Icon { get; set; }
		[Parameter]
		public bool Outline { get; set; } = false;
		[Parameter]
		public PillVariant Variant { get; set; } = PillVariant.primary;
	}
}

