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
                parameters.Add(p => p.ForceBreakpoint, Enums.ForceBreakpoint.lg);
                parameters.Add(p => p.Theme, "testTheme");
                parameters.Add(p => p.ColorSchema, Enums.ColorSchema.Dark);
            });

            // Assert
            cut.MarkupMatches("<ix-application id='testId' force-breakpoint='lg' theme='testTheme' color-schema='dark'></ix-application>");
        }

        [Fact]
        public void ColorSchemaDefaultsToSystem()
        {
            var cut = RenderComponent<Application>();

            Assert.Equal(Enums.ColorSchema.System, cut.Instance.ColorSchema);
            Assert.Contains("color-schema=\"system\"", cut.Markup);
        }

        [Fact]
        public void AppSwitchConfig_SetsValueAndCallsInitialParameter()
        {
            // Arrange
            var config = new AppSwitchConfig
            {
                CurrentAppId = "app-1"
            };
            var cut = RenderComponent<Application>(parameters => parameters
                .Add(p => p.AppSwitchConfig, config));

            // Assert
            Assert.Equal(config, cut.Instance.AppSwitchConfig);
        }
        
    }
}
