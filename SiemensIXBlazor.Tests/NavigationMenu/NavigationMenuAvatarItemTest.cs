// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using Xunit;
using SiemensIXBlazor.Components.NavigationMenu;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Tests.NavigationMenu
{
    public class NavigationMenuAvatarItemTests : TestContextBase
    {
        [Fact]
        public void NavigationMenuAvatarItemRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<NavigationMenuAvatarItem>(
                ("Id", "testId"),
                ("Icon", "testIcon"),
                ("Label", "Test Label"),
                ("Class", "test-class"),
                ("Style", "width: 100%")
            );

            // Assert
            // Adjust the expected markup to match your component's output
            cut.MarkupMatches("<ix-menu-avatar-item id=\"testId\" class=\"test-class\" style=\"width: 100%\" label=\"Test Label\" icon=\"testIcon\"></ix-menu-avatar-item>");
        }

        [Fact]
        public async Task ItemClickedEventWorks()
        {
            // Arrange
            var clicked = false;
            var cut = RenderComponent<NavigationMenuAvatarItem>(
                ("Id", "navigationMenuAvatarItem"),
                ("ItemClickedEvent", EventCallback.Factory.Create(this, (MouseEventArgs args) => clicked = true))
            );

            // Act
            await cut.Instance.ItemClicked(new MouseEventArgs());

            // Assert
            Assert.True(clicked);
        }
    }
}