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
using SiemensIXBlazor.Components;

namespace SiemensIXBlazor.Tests
{
    public class ApplicationHeaderTests : TestContextBase
    {
        [Fact]
        public void ApplicationHeaderRendersWithoutCrashing()
        {
            // Arrange
            var cut = Render<ApplicationHeader>(parameters => {
                parameters.Add(p => p.Name, "testName");
                parameters.Add(p => p.NameSuffix, "testSuffix");
                parameters.Add(p => p.CompanyLogo, "logo.png");
                parameters.Add(p => p.CompanyLogoAlt, "Company Logo");
                parameters.Add(p => p.AppIcon, "app-icon.svg");
                parameters.Add(p => p.AppIconAlt, "App Icon");
                parameters.Add(p => p.AppIconOutline, true);
                parameters.Add(p => p.HideBottomBorder, true);
                parameters.Add(p => p.ShowMenu, true);
                parameters.Add(p => p.AriaLabelAppSwitchIconButton, "Open applications");
                parameters.Add(p => p.AriaLabelMoreMenuIconButton, "More actions");
            });

            // Assert
            cut.MarkupMatches("<ix-application-header id='' name='testName' name-suffix='testSuffix' company-logo='logo.png' company-logo-alt='Company Logo' app-icon='app-icon.svg' app-icon-alt='App Icon' app-icon-outline='true' hide-bottom-border='true' show-menu='true' aria-label-app-switch-icon-button='Open applications' aria-label-more-menu-icon-button='More actions' slot='application-header'></ix-application-header>");
        }

        [Fact]
        public void ApplicationHeaderRendersChildContent()
        {
            // Arrange
            var expectedContent = "Expected content";

            // Act
            var cut = Render<ApplicationHeader>(parameters => parameters
                .Add(p => p.ChildContent, builder => 
                {
                    builder.AddContent(0, expectedContent);
                }));

            // Assert
            Assert.Contains(expectedContent, cut.Markup);
        }

        [Fact]
        public void ApplicationHeaderRendersSecondarySlot()
        {
            // Arrange
            var expectedSecondaryContent = "Secondary content";

            // Act
            var cut = Render<ApplicationHeader>(parameters => parameters
                .Add(p => p.Secondary, builder => 
                {
                    builder.AddContent(0, expectedSecondaryContent);
                }));

            // Assert
            var markup = cut.Markup;
            Assert.Contains("slot=\"secondary\"", markup);
            Assert.Contains(expectedSecondaryContent, markup);
        }

        [Fact]
        public void ApplicationHeaderRendersNamedSlots()
        {
            var cut = Render<ApplicationHeader>(parameters => parameters
                .Add(p => p.Overflow, builder => builder.AddContent(0, "Overflow"))
                .Add(p => p.Logo, builder => builder.AddContent(1, "Logo"))
                .Add(p => p.Avatar, builder => builder.AddContent(2, "Avatar")));

            Assert.Contains("slot=\"overflow\"", cut.Markup);
            Assert.Contains("slot=\"logo\"", cut.Markup);
            Assert.Contains("slot=\"ix-application-header-avatar\"", cut.Markup);
        }

        [Fact]
        public void ApplicationHeaderDoesNotRenderSecondarySlotWhenNull()
        {
            // Arrange & Act
            var cut = Render<ApplicationHeader>(parameters => {
                parameters.Add(p => p.Name, "testName");
            });

            // Assert
            Assert.DoesNotContain("slot=\"secondary\"", cut.Markup);
        }
        [Fact]
        public async Task OpenAppSwitchEventWorks()
        {
            // Arrange
            var eventTriggered = false;
            var cut = Render<ApplicationHeader>(parameters => parameters
                .Add(p => p.Id, "headerId")
                .Add(p => p.OpenAppSwitchEvent, EventCallback.Factory.Create(this, () => { eventTriggered = true; }))
            );

            // Act
            await cut.InvokeAsync(() => cut.Instance.OpenAppSwitch());

            // Assert
            Assert.True(eventTriggered);
        }

        [Fact]
        public async Task MenuToggleEventWorks()
        {
            var menuExpanded = false;
            var cut = Render<ApplicationHeader>(parameters => parameters
                .Add(p => p.Id, "headerId")
                .Add(p => p.MenuToggleEvent, EventCallback.Factory.Create<bool>(this, value => menuExpanded = value))
            );

            await cut.InvokeAsync(() => cut.Instance.MenuToggle(true));

            Assert.True(menuExpanded);
        }

        [Fact]
        public void EnableTopLayerDefaultsToFalse()
        {
            // Arrange
            var cut = Render<ApplicationHeader>(parameters => parameters
                .Add(p => p.Id, "test-id"));

            // Assert
            Assert.False(cut.Instance.EnableTopLayer);
            Assert.DoesNotContain("enable-top-layer", cut.Markup);
        }

        [Fact]
        public void EnableTopLayerTrueRendersAttribute()
        {
            // Arrange
            var cut = Render<ApplicationHeader>(parameters => parameters
                .Add(p => p.Id, "test-id")
                .Add(p => p.EnableTopLayer, true));

            // Assert
            Assert.True(cut.Instance.EnableTopLayer);
            Assert.Contains("enable-top-layer", cut.Markup);
        }
    }
}
