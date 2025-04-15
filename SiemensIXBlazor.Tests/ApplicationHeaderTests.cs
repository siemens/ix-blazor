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
            });

            // Assert
            cut.MarkupMatches("<ix-application-header name='testName'></ix-application-header>");
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