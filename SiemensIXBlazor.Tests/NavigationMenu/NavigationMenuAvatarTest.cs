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
    public class NavigationMenuAvatarTests : TestContextBase
    {
        [Fact]
        public void NavigationMenuAvatarRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<NavigationMenuAvatar>(
                ("Id", "testId"),
                ("Class", "test-class"),
                ("Style", "width: 100%"),
                ("Bottom", "Bottom Text"),
                ("I18NLogout", "Logout"),
                ("Image", "testImage"),
                ("Initials", "TI"),
                ("Top", "Top Text"),
                ("ShowLogoutButton", true),
                ("ChildContent", (RenderFragment)(builder =>
                {
                    builder.OpenElement(0, "div");
                    builder.AddContent(1, "Test child content");
                    builder.CloseElement();
                }))
            );

            // Assert
            // Adjust the expected markup to match your component's output
            cut.MarkupMatches("<ix-menu-avatar id=\"testId\" class=\"test-class\" style=\"width: 100%\" bottom=\"Bottom Text\" i-1-8n-logout=\"Logout\" image=\"testImage\" initials=\"TI\" top=\"Top Text\" show-logout-button=\"\"><div>Test child content</div></ix-menu-avatar>");
        }
        [Fact]
        public async Task LogoutClickedEventWorks()
        {
            // Arrange
            var clicked = false;
            var cut = RenderComponent<NavigationMenuAvatar>(
                ("Id", "navigationMenuAvatar"),
                ("LogoutClickedEvent", EventCallback.Factory.Create(this, () => clicked = true))
            );

            // Act
            await cut.Instance.LogoutClicked();

            // Assert
            Assert.True(clicked);
        }
    }
}