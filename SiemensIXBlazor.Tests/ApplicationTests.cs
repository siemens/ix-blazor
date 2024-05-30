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
using SiemensIXBlazor.Objects.Application;

namespace SiemensIXBlazor.Tests
{
    public class ApplicationTests : TestContextBase
    {
        [Fact]
        public void ApplicationRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<Application>(parameters => {
                parameters.Add(p => p.Id, "testId");
                parameters.Add(p => p.Breakpoints, ["sm", "md", "lg"]);
                parameters.Add(p => p.ForceBreakpoint, Enums.ForceBreakpoint.lg);
                parameters.Add(p => p.Theme, "testTheme");
                parameters.Add(p => p.ThemeSystemAppearance, true);
            });

            // Assert
            cut.MarkupMatches("<ix-application id='testId' breakpoints=\"['sm','md','lg']\" force-breakpoint='lg' theme='testTheme' theme-system-appearance=''></ix-application>");
        }

        [Fact]
        public void AppSwitchConfig_SetsValueAndCallsInitialParameter()
        {
            // Arrange
            var cut = RenderComponent<Application>();
            var config = new AppSwitchConfig();

            // Act
            cut.Instance.AppSwitchConfig = config;

            // Assert
            Assert.Equal(config, cut.Instance.AppSwitchConfig);
        }
        
    }
}