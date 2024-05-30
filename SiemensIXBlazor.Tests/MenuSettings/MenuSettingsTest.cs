// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using SiemensIXBlazor.Components;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Components.MenuSettings;
using Microsoft.AspNetCore.Components.Web;

namespace SiemensIXBlazor.Tests.MenuSettings
{
    public class MenuSettingsTest : TestContextBase
    {
        [Fact]
        public void MenuSettingsRendersCorrectly()
        {
            // Arrange

            var cut = RenderComponent<Components.MenuSettings.MenuSettings>(

                ("Class", "test-class"),
                ("Style", "width: 100%"),
                ("ActiveTabLabel", "Active Tab"),
                ("Label", "Test Label"),
                ("Id", "testId"),
                ("Show", true));

            // Assert
            cut.MarkupMatches(
                "<ix-menu-settings id=\"testId\" class=\"test-class\" style=\"width: 100%\" active-tab-label=\"Active Tab\" label=\"Test Label\" show=\"\"></ix-menu-settings>");
        }

        [Fact]
        public async Task ClosedEventWorks()
        {
            // Arrange
            var closed = false;
            var cut = RenderComponent<Components.MenuSettings.MenuSettings>(
                ("Id", "menuSettings"),
                ("ClosedEvent", EventCallback.Factory.Create(this, (MouseEventArgs args) => closed = true))
            );

            // Act
            await cut.Instance.Closed(new MouseEventArgs());

            // Assert
            Assert.True(closed);
        }
    }
}