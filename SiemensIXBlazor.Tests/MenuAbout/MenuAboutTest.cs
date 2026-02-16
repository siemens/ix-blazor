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
namespace SiemensIXBlazor.Tests.MenuAbout
{
	public class MenuAboutTest: TestContextBase
    {
        [Fact]
        public void ComponentRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<Components.MenuAbout.MenuAbout>();

            // Assert
            cut.MarkupMatches("<ix-menu-about aria-label-close-button=\"Close About\" label=\"About &amp; legal information\" id=\"\"></ix-menu-about>");
        }

        [Fact]
        public void ChildContentPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Components.MenuAbout.MenuAbout>(parameters => parameters.Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content"))));

            // Assert
            Assert.NotNull(cut.Instance.ChildContent);
        }

        [Fact]
        public void IdPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Components.MenuAbout.MenuAbout>(parameters => parameters.Add(p => p.Id, "testId"));

            // Assert
            Assert.Equal("testId", cut.Instance.Id);
        }

        [Fact]
        public void ClosedEventTriggeredCorrectly()
        {
            // Arrange
            var eventTriggered = false;
            var cut = RenderComponent<Components.MenuAbout.MenuAbout>(parameters => parameters.Add(p => p.ClosedEvent, EventCallback.Factory.Create<MouseEventArgs>(this, () => eventTriggered = true)));

            // Act
            cut.Instance.ClosedEvent.InvokeAsync(new MouseEventArgs());

            // Assert
            Assert.True(eventTriggered);
        }

        [Fact]
        public void AriaLabelCloseButtonDefaultsToCloseAbout()
        {
            // Arrange
            var cut = RenderComponent<Components.MenuAbout.MenuAbout>(parameters => parameters
                .Add(p => p.Id, "test-id"));

            // Assert
            Assert.Equal("Close About", cut.Instance.AriaLabelCloseButton);
            Assert.Contains("aria-label-close-button=\"Close About\"", cut.Markup);
        }

        [Fact]
        public void AriaLabelCloseButtonCanBeCustomized()
        {
            // Arrange
            var cut = RenderComponent<Components.MenuAbout.MenuAbout>(parameters => parameters
                .Add(p => p.Id, "test-id")
                .Add(p => p.AriaLabelCloseButton, "Custom Close Label"));

            // Assert
            Assert.Equal("Custom Close Label", cut.Instance.AriaLabelCloseButton);
            Assert.Contains("aria-label-close-button=\"Custom Close Label\"", cut.Markup);
        }
    }
}
