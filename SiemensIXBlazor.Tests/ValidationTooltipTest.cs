// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.ValidationTooltip;

namespace SiemensIXBlazor.Tests;
public class ValidationTooltipTests : TestContextBase
{
	[Fact]
	public void ValidationTooltipRendersCorrectly()
	{
		// Arrange
		string id = "test-id";
		string style = "color: red;";
		string cssClass = "test-class";
		string message = "Test message";
		string childContent = "Test content";
		bool suppressAutomaticPlacement = true;
		ValidationTooltipPlacement placement = ValidationTooltipPlacement.Top;

		// Act
		var cut = RenderComponent<ValidationTooltip>(parameters => parameters
				.Add(p => p.Id, id)
				.Add(p => p.Style, style)
				.Add(p => p.Class, cssClass)
				.Add(p => p.Message, message)
				.Add(p => p.SuppressAutomaticPlacement, suppressAutomaticPlacement)
				.Add(p => p.Placement, placement)
				.Add(p => p.ChildContent, childContent)
			);

		// Assert
		cut.MarkupMatches($@"
					<ix-validation-tooltip id=""{id}"" style=""{style}"" class=""{cssClass}"" message=""{message}"" suppress-automatic-placement placement=""top"">
						{childContent}
					</ix-validation-tooltip>
				");
	}

	[Fact]
	public void ValidationTooltipComplexChildContentRendersCorrectly()
	{
		// Arrange
		string childContent = $@"<label for=""validationCustom01"">Example input</label><input id=""validationCustom01"" value="""" required minlength=""4"" />";

		// Act
		var cut = RenderComponent<ValidationTooltip>(parameters => parameters
			.AddChildContent(childContent)
		);

		// Assert
		cut.MarkupMatches($@"
					<ix-validation-tooltip placement=""top"">
						{childContent}
					</ix-validation-tooltip>
				");
	}
}
