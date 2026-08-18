// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using Microsoft.AspNetCore.Components;
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
            var cut = Render<ProgressIndicator>(parameters => parameters
                .Add(p => p.Value, 50.0)
                .Add(p => p.Max, 100.0)
                .Add(p => p.Min, 0.0)
                .Add(p => p.Label, "Loading")
                .Add(p => p.HelperText, "Please wait")
                .Add(p => p.Size, ProgressIndicatorSize.md)
                .Add(p => p.Status, ProgressIndicatorStatus.@default)
                .Add(p => p.Type, ProgressIndicatorType.linear)
                .Add(p => p.TextAlignment, ProgressIndicatorTextAlignment.left)
                .Add(p => p.ShowTextAsTooltip, true)
                .Add(p => p.Style, "margin: 10px;")
                .Add(p => p.Class, "test-class")
            );

            // Assert
            cut.MarkupMatches(@"<ix-progress-indicator helper-text=""Please wait"" label=""Loading"" max=""100"" min=""0"" show-text-as-tooltip='true' size=""md"" status=""default"" text-alignment=""left"" type=""linear"" value=""50"" style=""margin: 10px;"" class=""test-class""></ix-progress-indicator>");
        }

        [Fact]
        public void ProgressIndicatorWithCircularType()
        {
            // Arrange
            var cut = Render<ProgressIndicator>(parameters => parameters
                .Add(p => p.Type, ProgressIndicatorType.circular)
                .Add(p => p.Value, 75.0)
                .Add(p => p.Status, ProgressIndicatorStatus.success)
            );

            // Assert
            cut.MarkupMatches(@"<ix-progress-indicator max=""100"" min=""0"" size=""md"" status=""success"" text-alignment=""left"" type=""circular"" value=""75""></ix-progress-indicator>");
        }

        [Fact]
        public void ProgressIndicatorWithTooltip()
        {
            // Arrange
            var cut = Render<ProgressIndicator>(parameters => parameters
                .Add(p => p.ShowTextAsTooltip, true)
                .Add(p => p.HelperText, "Tooltip text")
                .Add(p => p.Size, ProgressIndicatorSize.lg)
            );

            // Assert
            cut.MarkupMatches(@"<ix-progress-indicator helper-text=""Tooltip text"" max=""100"" min=""0"" show-text-as-tooltip='true' size=""lg"" status=""default"" text-alignment=""left"" type=""linear"" value=""0""></ix-progress-indicator>");
        }

        [Fact]
        public void ProgressIndicatorWithErrorStatus()
        {
            // Arrange
            var cut = Render<ProgressIndicator>(parameters => parameters
                .Add(p => p.Status, ProgressIndicatorStatus.error)
                .Add(p => p.Value, 25.0)
                .Add(p => p.TextAlignment, ProgressIndicatorTextAlignment.center)
            );

            // Assert
            cut.MarkupMatches(@"<ix-progress-indicator max=""100"" min=""0"" size=""md"" status=""error"" text-alignment=""center"" type=""linear"" value=""25""></ix-progress-indicator>");
        }

        [Fact]
        public void ProgressIndicatorRendersHelperTextSlotAlongsideDefaultContent()
        {
            var helperText = (RenderFragment)(builder => builder.AddContent(0, "Custom helper text"));
            var content = (RenderFragment)(builder => builder.AddContent(0, "50%"));

            var cut = Render<ProgressIndicator>(parameters => parameters
                .Add(p => p.HelperTextContent, helperText)
                .Add(p => p.ChildContent, content));

            var indicatorMarkup = cut.Find("ix-progress-indicator").InnerHtml;

            Assert.Contains("slot=\"helper-text\"", indicatorMarkup);
            Assert.Contains("Custom helper text", indicatorMarkup);
            Assert.Contains("50%", indicatorMarkup);
        }
    }
}
