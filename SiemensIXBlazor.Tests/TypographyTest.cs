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
		cut.MarkupMatches("<ix-typography id=\"testId\" bold format=\"body-xs\" text-color=\"alarm\" text-decoration=\"line-through\">Test content</ix-typography>");
	}
}
