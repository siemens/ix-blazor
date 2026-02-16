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
                "<ix-menu-settings id=\"testId\" class=\"test-class\" style=\"width: 100%\" active-tab-label=\"Active Tab\" aria-label-close-button=\"Close Settings\" label=\"Test Label\" show=\"\"></ix-menu-settings>");
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

        [Fact]
        public void AriaLabelCloseButtonDefaultsToCloseSettings()
        {
            // Arrange
            var cut = RenderComponent<Components.MenuSettings.MenuSettings>(parameters => parameters
                .Add(p => p.Id, "test-id"));

            // Assert
            Assert.Equal("Close Settings", cut.Instance.AriaLabelCloseButton);
            Assert.Contains("aria-label-close-button=\"Close Settings\"", cut.Markup);
        }

        [Fact]
        public void AriaLabelCloseButtonCanBeCustomized()
        {
            // Arrange
            var cut = RenderComponent<Components.MenuSettings.MenuSettings>(parameters => parameters
                .Add(p => p.Id, "test-id")
                .Add(p => p.AriaLabelCloseButton, "Custom Close Label"));

            // Assert
            Assert.Equal("Custom Close Label", cut.Instance.AriaLabelCloseButton);
            Assert.Contains("aria-label-close-button=\"Custom Close Label\"", cut.Markup);
        }
    }
}