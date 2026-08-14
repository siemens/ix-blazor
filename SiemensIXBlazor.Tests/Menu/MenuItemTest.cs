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
using SiemensIXBlazor.Enums.Menu;
using SiemensIXBlazor.Components.Menu;

namespace SiemensIXBlazor.Tests.Menu
{
	public class MenuItemTests : TestContextBase
	{
		[Fact]
		public void MenuItemRendersCorrectly()
		{
			// Arrange
			var cut = Render<MenuItem>(parameters => parameters
			    .Add(p => p.Active, true)
			    .Add(p => p.Disabled, false)
			    .Add(p => p.Home, true)
			    .Add(p => p.Bottom, true)
			    .Add(p => p.Icon, "testIcon")
			    .Add(p => p.Notifications, 5)
			    .Add(p => p.Label, "label")
			    .Add(p => p.ChildContent, (RenderFragment)(builder =>
				{
					builder.OpenElement(0, "div");
					builder.AddContent(1, "Test child content");
					builder.CloseElement();
				}))
			    .Add(p => p.TooltipText, "Test tooltip")
			);

			// Assert
			// Adjust the expected markup to match your component's output
			cut.MarkupMatches("<ix-menu-item active=\"\" home=\"\" bottom=\"\" icon=\"testIcon\" notifications=\"5\" label=\"label\" tooltip-text=\"Test tooltip\" target=\"_self\"><div>Test child content</div></ix-menu-item>");
		}

		[Fact]
		public void RendersLinkAttributes()
		{
			var cut = Render<MenuItem>(parameters => parameters
				.Add(p => p.Label, "Documentation")
				.Add(p => p.Href, "/docs")
				.Add(p => p.Target, MenuItemTarget._blank)
				.Add(p => p.Rel, "noopener"));

			Assert.Contains("href=\"/docs\"", cut.Markup);
			Assert.Contains("target=\"_blank\"", cut.Markup);
			Assert.Contains("rel=\"noopener\"", cut.Markup);
		}
	}
}
