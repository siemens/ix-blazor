// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.Typography;
namespace SiemensIXBlazor.Components;

public partial class Typography
{
	[Parameter]
	public string? Id { get; set; } = string.Empty;
	[Parameter]
	public bool Bold { get; set; } = false;
	[Parameter]
	public TypographyFormat? Format { get; set; }
	[Parameter]
	public TypographyColor? TextColor { get; set; }
	[Parameter]
	public TextDecoration? TextDecoration { get; set; } = Enums.Typography.TextDecoration.None;
	[Parameter]
	public RenderFragment? ChildContent { get; set; }
}
