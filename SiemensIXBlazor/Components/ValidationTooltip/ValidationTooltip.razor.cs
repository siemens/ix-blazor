// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.ValidationTooltip;
namespace SiemensIXBlazor.Components;

public partial class ValidationTooltip
{
	[Parameter]
	public string? Id { get; set; }
	[Parameter]
	public string? Message { get; set; }
	[Parameter]
	public ValidationTooltipPlacement Placement { get; set; } = ValidationTooltipPlacement.Top;
	[Parameter]
	public bool SuppressAutomaticPlacement { get; set; } = false;
	[Parameter]
	public RenderFragment? ChildContent { get; set; }
}
