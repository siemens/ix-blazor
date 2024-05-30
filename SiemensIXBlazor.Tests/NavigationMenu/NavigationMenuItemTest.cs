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
using Xunit;
using SiemensIXBlazor.Components.NavigationMenu;

namespace SiemensIXBlazor.Tests.NavigationMenu
{
    public class NavigationMenuItemTests : TestContextBase
    {
        [Fact]
        public void NavigationMenuItemRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<NavigationMenuItem>(
                ("Active", true),
                ("Disabled", false),
                ("Home", true),
                ("Icon", "testIcon"),
                ("Notifications", 5),
                ("TabIcon", "document"),
                ("ChildContent", (RenderFragment)(builder =>
                {
                    builder.OpenElement(0, "div");
                    builder.AddContent(1, "Test child content");
                    builder.CloseElement();
                }))
            );

            // Assert
            // Adjust the expected markup to match your component's output
            cut.MarkupMatches("<ix-menu-item active=\"\" home=\"\" icon=\"testIcon\" notifications=\"5\" tab-icon=\"document\"><div>Test child content</div></ix-menu-item>");
        }
    }
}