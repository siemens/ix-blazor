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
		var cut = RenderComponent<Typography>(
			("Id", "testId"),
			("Format", TypographyFormat.Body_Xs),
			("Bold", true),
			("TextColor", TypographyColor.Alarm),
			("TextDecoration", TextDecoration.Line_Through),
			("ChildContent", (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content")))
		);

		// Assert
		cut.MarkupMatches("<ix-typography id=\"testId\" bold format=\"body-xs\" text-color=\"alarm\" text-decoration=\"line-through\">Test content</ix-typography>");
	}
}
