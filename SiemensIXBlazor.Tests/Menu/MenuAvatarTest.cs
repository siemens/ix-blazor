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
using SiemensIXBlazor.Components.MenuAvatar;

namespace SiemensIXBlazor.Tests.Menu
{
	public class MenuAvatarTests : TestContextBase
    {
        [Fact]
        public void MenuAvatarRendersCorrectly()
        {
            // Arrange
            var cut = Render<MenuAvatar>(parameters => parameters
                .Add(p => p.Id, "testId")
                .Add(p => p.Class, "test-class")
                .Add(p => p.Style, "width: 100%")
                .Add(p => p.Bottom, "Bottom Text")
                .Add(p => p.I18nLogout, "Logout")
                .Add(p => p.Image, "testImage")
                .Add(p => p.Initials, "TI")
                .Add(p => p.Top, "Top Text")
                .Add(p => p.HideLogoutButton, true)
                .Add(p => p.ChildContent, (RenderFragment)(builder =>
                {
                    builder.OpenElement(0, "div");
                    builder.AddContent(1, "Test child content");
                    builder.CloseElement();
                }))
            );

            // Assert
            // Adjust the expected markup to match your component's output
            cut.MarkupMatches("<ix-menu-avatar id=\"testId\" class=\"test-class\" style=\"width: 100%\" slot=\"ix-menu-avatar\" bottom=\"Bottom Text\" i18n-logout=\"Logout\" image=\"testImage\" initials=\"TI\" top=\"Top Text\" hide-logout-button=\"true\"><div>Test child content</div></ix-menu-avatar>");
        }
        [Fact]
        public async Task LogoutClickedEventWorks()
        {
            // Arrange
            var clicked = false;
            var cut = Render<MenuAvatar>(parameters => parameters
                .Add(p => p.Id, "navigationMenuAvatar")
                .Add(p => p.LogoutClickedEvent, EventCallback.Factory.Create(this, () => clicked = true))
            );

            // Act
            await cut.Instance.LogoutClicked();

            // Assert
            Assert.True(clicked);
        }

        [Fact]
        public void EnableTopLayerDefaultsToFalse()
        {
            // Arrange
            var cut = Render<MenuAvatar>(parameters => parameters
                .Add(p => p.Id, "test-id"));

            // Assert
            Assert.False(cut.Instance.EnableTopLayer);
            Assert.DoesNotContain("enable-top-layer", cut.Markup);
        }

        [Fact]
        public void EnableTopLayerTrueRendersAttribute()
        {
            // Arrange
            var cut = Render<MenuAvatar>(parameters => parameters
                .Add(p => p.Id, "test-id")
                .Add(p => p.EnableTopLayer, true));

            // Assert
            Assert.True(cut.Instance.EnableTopLayer);
            Assert.Contains("enable-top-layer", cut.Markup);
        }

        [Fact]
        public void TooltipTextPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = Render<MenuAvatar>(parameters => parameters
                .Add(p => p.Id, "test-id")
                .Add(p => p.TooltipText, "Test tooltip"));

            // Assert
            Assert.Equal("Test tooltip", cut.Instance.TooltipText);
            Assert.Contains("tooltip-text=\"Test tooltip\"", cut.Markup);
        }

        [Fact]
        public void AriaLabelTooltipPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = Render<MenuAvatar>(parameters => parameters
                .Add(p => p.Id, "test-id")
                .Add(p => p.AriaLabelTooltip, "Tooltip label"));

            // Assert
            Assert.Equal("Tooltip label", cut.Instance.AriaLabelTooltip);
            Assert.Contains("aria-label-tooltip=\"Tooltip label\"", cut.Markup);
        }

        [Fact]
        public void TooltipTextAndAriaLabelTooltipCanBeCombined()
        {
            // Arrange
            var cut = Render<MenuAvatar>(parameters => parameters
                .Add(p => p.Id, "test-id")
                .Add(p => p.TooltipText, "Hover text")
                .Add(p => p.AriaLabelTooltip, "Screen reader text"));

            // Assert
            Assert.Equal("Hover text", cut.Instance.TooltipText);
            Assert.Equal("Screen reader text", cut.Instance.AriaLabelTooltip);
            Assert.Contains("tooltip-text=\"Hover text\"", cut.Markup);
            Assert.Contains("aria-label-tooltip=\"Screen reader text\"", cut.Markup);
        }
    }
}
