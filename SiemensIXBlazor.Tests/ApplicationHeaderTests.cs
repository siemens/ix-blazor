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
            var cut = RenderComponent<ApplicationHeader>(parameters => {
                parameters.Add(p => p.Name, "testName");
                parameters.Add(p => p.NameSuffix, "testSuffix");
                parameters.Add(p => p.CompanyLogo, "logo.png");
                parameters.Add(p => p.CompanyLogoAlt, "Company Logo");
                parameters.Add(p => p.AppIcon, "app-icon.svg");
                parameters.Add(p => p.AppIconAlt, "App Icon");
                parameters.Add(p => p.HideBottomBorder, true);
            });

            // Assert
            cut.MarkupMatches("<ix-application-header id='' name='testName' name-suffix='testSuffix' company-logo='logo.png' company-logo-alt='Company Logo' app-icon='app-icon.svg' app-icon-alt='App Icon' hide-bottom-border=\"\" ></ix-application-header>");
        }

        [Fact]
        public void ApplicationHeaderRendersChildContent()
        {
            // Arrange
            var expectedContent = "Expected content";

            // Act
            var cut = RenderComponent<ApplicationHeader>(parameters => parameters
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
            var cut = RenderComponent<ApplicationHeader>(parameters => parameters
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
        public void ApplicationHeaderDoesNotRenderSecondarySlotWhenNull()
        {
            // Arrange & Act
            var cut = RenderComponent<ApplicationHeader>(parameters => {
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
            var cut = RenderComponent<ApplicationHeader>(
                ("Id", "headerId"),
                ("OpenAppSwitchEvent", EventCallback.Factory.Create(this, () => { eventTriggered = true; }))
            );

            // Act
            await cut.InvokeAsync(() => cut.Instance.OpenAppSwitch());

            // Assert
            Assert.True(eventTriggered);
        }
    }
}