// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.Typography;

namespace SiemensIXBlazor.Tests;
public class TypographyTest : TestContextBase
{
	[Fact]
	public void TypographyRendersCorrectly()
	{
		// Arrange
		var cut = Render<Typography>(parameters => parameters
		    .Add(p => p.Id, "testId")
		    .Add(p => p.Format, TypographyFormat.Body_Xs)
		    .Add(p => p.Bold, true)
		    .Add(p => p.TextColor, TypographyColor.Alarm)
		    .Add(p => p.TextDecoration, TextDecoration.Line_Through)
		    .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content")))
		);

		// Assert
		cut.MarkupMatches("<ix-typography id=\"testId\" bold='true' format=\"body-xs\" text-color=\"alarm\" text-decoration=\"line-through\">Test content</ix-typography>");
	}

	[Fact]
	public void TypographyOmitsUnsetOptionalAttributesAndUsesDefaultDecoration()
	{
		var cut = Render<Typography>();
		var element = cut.Find("ix-typography");

		Assert.False(element.HasAttribute("format"));
		Assert.False(element.HasAttribute("text-color"));
		Assert.Equal("none", element.GetAttribute("text-decoration"));
	}

	[Theory]
	[InlineData(TypographyColor.Alarm_Contrast, "alarm-contrast")]
	[InlineData(TypographyColor.Critical_Contrast, "critical-contrast")]
	[InlineData(TypographyColor.Info_Contrast, "info-contrast")]
	[InlineData(TypographyColor.Neutral_Contrast, "neutral-contrast")]
	[InlineData(TypographyColor.Primary_Contrast, "primary-contrast")]
	[InlineData(TypographyColor.Success_Contrast, "success-contrast")]
	[InlineData(TypographyColor.Warning_Contrast, "warning-contrast")]
	public void TypographyRendersAllContrastColors(TypographyColor color, string expected)
	{
		var cut = Render<Typography>(parameters => parameters.Add(p => p.TextColor, color));

		Assert.Equal(expected, cut.Find("ix-typography").GetAttribute("text-color"));
	}
}
