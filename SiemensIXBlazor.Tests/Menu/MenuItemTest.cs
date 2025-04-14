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
using SiemensIXBlazor.Components.Menu;

namespace SiemensIXBlazor.Tests.Menu
{
	public class MenuItemTests : TestContextBase
	{
		[Fact]
		public void MenuItemRendersCorrectly()
		{
			// Arrange
			var cut = RenderComponent<MenuItem>(
				("Active", true),
				("Disabled", false),
				("Home", true),
				("Icon", "testIcon"),
				("Notifications", 5),
				("Label", "label"),
				("ChildContent", (RenderFragment)(builder =>
				{
					builder.OpenElement(0, "div");
					builder.AddContent(1, "Test child content");
					builder.CloseElement();
				}))
			);

			// Assert
			// Adjust the expected markup to match your component's output
			cut.MarkupMatches("<ix-menu-item active=\"\" home=\"\" icon=\"testIcon\" notifications=\"5\" label=\"label\"><div>Test child content</div></ix-menu-item>");
		}
	}
}