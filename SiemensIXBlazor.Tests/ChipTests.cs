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

namespace SiemensIXBlazor.Tests
{
    public class ChipTests: TestContextBase
    {
        [Fact]
        public void ChipRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<Chip>(parameters => {
                parameters.Add(p => p.Id, "testId");
                parameters.Add(p => p.Active, true);
                parameters.Add(p => p.Background, "testBackground");
                parameters.Add(p => p.Closable, true);
                parameters.Add(p => p.ChipColor, "testColor");
                parameters.Add(p => p.Icon, "testIcon");
                parameters.Add(p => p.TooltipText, "tooltipText");
                parameters.Add(p => p.Outline, true);
                parameters.Add(p => p.Variant, Enums.Chip.ChipVariant.neutral);
            });

            // Assert
            cut.MarkupMatches("<ix-chip id=\"testId\" closable=\"\" outline=\"\" active=\"\" background=\"testBackground\" chip-color=\"testColor\" icon=\"testIcon\" variant=\"neutral\" tooltip-text='tooltipText'></ix-chip>");
        }

        [Fact]
        public void ChipRendersChildContent()
        {
            // Arrange
            var expectedContent = "Expected content";

            // Act
            var cut = RenderComponent<Chip>(parameters => parameters
                .Add(p => p.ChildContent, builder =>
                {
                    builder.AddContent(0, expectedContent);
                }));

            // Assert
            Assert.Contains(expectedContent, cut.Markup);
        }
    }
}
