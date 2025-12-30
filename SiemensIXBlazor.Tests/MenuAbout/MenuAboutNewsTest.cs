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
using SiemensIXBlazor.Components.MenuAbout;

namespace SiemensIXBlazor.Tests.MenuAbout
{
	public class MenuAboutNewsTest: TestContextBase
    {
        [Fact]
        public void ComponentRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<MenuAboutNews>();

            // Assert
            cut.MarkupMatches("<ix-menu-about-news id='' i18n-show-more='Show more'></ix-menu-about-news>");
        }

        [Fact]
        public void ChildContentPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<MenuAboutNews>(parameters => parameters.Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content"))));

            // Assert
            Assert.NotNull(cut.Instance.ChildContent);
        }

        [Fact]
        public void AboutItemLabelPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<MenuAboutNews>(parameters => parameters.Add(p => p.AboutItemLabel, "testAboutItemLabel"));

            // Assert
            Assert.Equal("testAboutItemLabel", cut.Instance.AboutItemLabel);
        }

        [Fact]
        public void ExpandedPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<MenuAboutNews>(parameters => parameters.Add(p => p.Expanded, true));

            // Assert
            Assert.True(cut.Instance.Expanded);
        }

        [Fact]
        public void I18nShowMorePropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<MenuAboutNews>(parameters => parameters.Add(p => p.I18NShowMore, "showMoreTest"));

            // Assert
            Assert.Equal("showMoreTest", cut.Instance.I18NShowMore);
        }

        [Fact]
        public void LablePropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<MenuAboutNews>(parameters => parameters.Add(p => p.Label, "testLabel"));

            // Assert
            Assert.Equal("testLabel", cut.Instance.Label);
        }

        [Fact]
        public void ShowPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<MenuAboutNews>(parameters => parameters.Add(p => p.Show, true));

            // Assert
            Assert.True(cut.Instance.Show);
        }

        [Fact]
        public void ClosePopoverEventTriggeredCorrectly()
        {
            // Arrange
            var eventTriggered = false;
            var cut = RenderComponent<MenuAboutNews>(parameters => parameters.Add(p => p.ClosePopoverEvent, EventCallback.Factory.Create(this, () => eventTriggered = true)));

            // Act
            cut.Instance.ClosePopoverEvent.InvokeAsync();

            // Assert
            Assert.True(eventTriggered);
        }

        [Fact]
        public void ShowMoreEventTriggeredCorrectly()
        {
            // Arrange
            var eventTriggered = false;
            var cut = RenderComponent<MenuAboutNews>(parameters => parameters.Add(p => p.ShowMoreEvent, EventCallback.Factory.Create<MouseEventArgs>(this, () => eventTriggered = true)));

            // Act
            cut.Instance.ShowMoreEvent.InvokeAsync(new MouseEventArgs());

            // Assert
            Assert.True(eventTriggered);
        }
    }
}
