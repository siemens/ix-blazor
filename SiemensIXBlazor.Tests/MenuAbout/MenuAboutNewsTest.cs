﻿// -----------------------------------------------------------------------
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
            var cut = RenderComponent<MenuAboutNews>(parameters => parameters
                .Add(p => p.Id, "testId"));

            // Assert
            cut.MarkupMatches("<ix-menu-about-news id='testId' i-1-8n-show-more='Show more' offset-bottom='0'></ix-menu-about-news>");
        }

        [Fact]
        public void ChildContentPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<MenuAboutNews>(parameters => parameters.Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content")))
                .Add(p => p.Id, "testId"));

            // Assert
            Assert.NotNull(cut.Instance.ChildContent);
        }

        [Fact]
        public void AboutItemLabelPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<MenuAboutNews>(parameters => parameters.Add(p => p.AboutItemLabel, "testAboutItemLabel")
            .Add(p => p.Id, "testId"));

            // Assert
            Assert.Equal("testAboutItemLabel", cut.Instance.AboutItemLabel);
        }

        [Fact]
        public void ExpandedPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<MenuAboutNews>(parameters => parameters.Add(p => p.Expanded, true).Add(p => p.Id, "testId"));

            // Assert
            Assert.True(cut.Instance.Expanded);
        }

        [Fact]
        public void I18nShowMorePropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<MenuAboutNews>(parameters => parameters.Add(p => p.I18NShowMore, "showMoreTest")
            .Add(p => p.Id, "testId"));

            // Assert
            Assert.Equal("showMoreTest", cut.Instance.I18NShowMore);
        }

        [Fact]
        public void LablePropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<MenuAboutNews>(parameters => parameters.Add(p => p.Label, "testLabel").Add(p => p.Id, "testId"));

            // Assert
            Assert.Equal("testLabel", cut.Instance.Label);
        }

        [Fact]
        public void OffsetBottomPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<MenuAboutNews>(parameters => parameters.Add(p => p.OffsetBottom, 1).Add(p => p.Id, "testId"));

            // Assert
            Assert.Equal(1, cut.Instance.OffsetBottom);
        }

        [Fact]
        public void ShowPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<MenuAboutNews>(parameters => parameters.Add(p => p.Show, true).Add(p => p.Id, "testId"));

            // Assert
            Assert.True(cut.Instance.Show);
        }

        [Fact]
        public void ClosePopoverEventTriggeredCorrectly()
        {
            // Arrange
            var eventTriggered = false;
            var cut = RenderComponent<MenuAboutNews>(parameters => parameters.Add(p => p.ClosePopoverEvent, EventCallback.Factory.Create(this, () => eventTriggered = true))
            .Add(p => p.Id, "testId"));

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
            var cut = RenderComponent<MenuAboutNews>(parameters => parameters.Add(p => p.ShowMoreEvent, EventCallback.Factory.Create<MouseEventArgs>(this, () => eventTriggered = true))
            .Add(p => p.Id, "testId"));

            // Act
            cut.Instance.ShowMoreEvent.InvokeAsync(new MouseEventArgs());

            // Assert
            Assert.True(eventTriggered);
        }
    }
}
