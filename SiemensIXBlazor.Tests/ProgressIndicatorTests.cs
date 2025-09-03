// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.ProgressIndicator;

namespace SiemensIXBlazor.Tests
{
    public class ProgressIndicatorTests : TestContextBase
    {
        [Fact]
        public void ProgressIndicatorRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<ProgressIndicator>(
                ("Value", 50.0),
                ("Max", 100.0),
                ("Min", 0.0),
                ("Label", "Loading"),
                ("HelperText", "Please wait"),
                ("Size", ProgressIndicatorSize.md),
                ("Status", ProgressIndicatorStatus._default),
                ("Type", ProgressIndicatorType.linear),
                ("TextAlignment", ProgressIndicatorTextAlignment.left),
                ("ShowTextAsTooltip", true),
                ("Style", "margin: 10px;"),
                ("Class", "test-class")
            );

            // Assert
            cut.MarkupMatches(@"<ix-progress-indicator helper-text=""Please wait"" label=""Loading"" max=""100"" min=""0"" show-text-as-tooltip size=""md"" status=""default"" text-alignment=""left"" type=""linear"" value=""50"" style=""margin: 10px;"" class=""test-class""></ix-progress-indicator>");
        }

        [Fact]
        public void ProgressIndicatorWithCircularType()
        {
            // Arrange
            var cut = RenderComponent<ProgressIndicator>(
                ("Type", ProgressIndicatorType.circular),
                ("Value", 75.0),
                ("Status", ProgressIndicatorStatus.success)
            );

            // Assert
            cut.MarkupMatches(@"<ix-progress-indicator max=""100"" min=""0"" size=""md"" status=""success"" text-alignment=""left"" type=""circular"" value=""75""></ix-progress-indicator>");
        }

        [Fact]
        public void ProgressIndicatorWithTooltip()
        {
            // Arrange
            var cut = RenderComponent<ProgressIndicator>(
                ("ShowTextAsTooltip", true),
                ("HelperText", "Tooltip text"),
                ("Size", ProgressIndicatorSize.lg)
            );

            // Assert
            cut.MarkupMatches(@"<ix-progress-indicator helper-text=""Tooltip text"" max=""100"" min=""0"" show-text-as-tooltip size=""lg"" status=""default"" text-alignment=""left"" type=""linear"" value=""0""></ix-progress-indicator>");
        }

        [Fact]
        public void ProgressIndicatorWithErrorStatus()
        {
            // Arrange
            var cut = RenderComponent<ProgressIndicator>(
                ("Status", ProgressIndicatorStatus.error),
                ("Value", 25.0),
                ("TextAlignment", ProgressIndicatorTextAlignment.center)
            );

            // Assert
            cut.MarkupMatches(@"<ix-progress-indicator max=""100"" min=""0"" size=""md"" status=""error"" text-alignment=""center"" type=""linear"" value=""25""></ix-progress-indicator>");
        }
    }
}
