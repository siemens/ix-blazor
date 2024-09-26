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
using SiemensIXBlazor.Components.MenuCategory;

namespace SiemensIXBlazor.Tests.Menu
{
	public class MenuCategoryTests : TestContextBase
    {
        [Fact]
        public void MenuCategoryRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<MenuCategory>(
                ("Icon", "testIcon"),
                ("Label", "Test Label"),
                ("Notifications", 5),
                ("ChildContent", (RenderFragment)(builder =>
                {
                    builder.OpenElement(0, "div");
                    builder.AddContent(1, "Test child content");
                    builder.CloseElement();
                }))
            );

            // Assert
            // Adjust the expected markup to match your component's output
            cut.MarkupMatches("<ix-menu-category icon=\"testIcon\" label=\"Test Label\" notifications=\"5\"><div>Test child content</div></ix-menu-category>");
        }
    }
}