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
using Microsoft.AspNetCore.Components.Web;
using SiemensIXBlazor.Components.MenuAvatar;

namespace SiemensIXBlazor.Tests.Menu
{
	public class MenuAvatarItemTests : TestContextBase
    {
        [Fact]
        public void MenuAvatarItemRendersCorrectly()
        {
            // Arrange
            var cut = Render<MenuAvatarItem>(parameters => parameters
                .Add(p => p.Id, "testId")
                .Add(p => p.Icon, "testIcon")
                .Add(p => p.Label, "Test Label")
                .Add(p => p.Class, "test-class")
                .Add(p => p.Style, "width: 100%")
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
            var cut = Render<MenuAvatarItem>(parameters => parameters
                .Add(p => p.Id, "navigationMenuAvatarItem")
                .Add(p => p.ItemClickedEvent, EventCallback.Factory.Create(this, (MouseEventArgs args) => clicked = true))
            );

            // Act
            await cut.Instance.ItemClicked(new MouseEventArgs());

            // Assert
            Assert.True(clicked);
        }
    }
}