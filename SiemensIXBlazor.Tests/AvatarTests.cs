// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using SiemensIXBlazor.Components.Avatar;

namespace SiemensIXBlazor.Tests
{
    public class AvatarTests : TestContextBase
    {
        [Fact]
        public void AvatarRendersWithoutCrashing()
        {
            // Arrange
            var cut = Render<Avatar>(parameters => {
                parameters.Add(p => p.Image, "testImage");
                parameters.Add(p => p.TooltipText, "testTooltipText");
                parameters.Add(p => p.Initials, "testInitials");
                parameters.Add(p => p.AriaLabel, "testAriaLabel");
                parameters.Add(p => p.AriaLabelTooltip, "testAriaLabelTooltip");

            });
        
            // Assert
            cut.MarkupMatches("<ix-avatar image='testImage' tooltip-text='testTooltipText' initials='testInitials' aria-label='testAriaLabel' aria-label-tooltip='testAriaLabelTooltip'></ix-avatar>");
        }

        [Fact]
        public void TooltipTextIsRenderedWithoutChangingOtherAvatarOptions()
        {
            var cut = Render<Avatar>(parameters => parameters
                .Add(p => p.Initials, "JD")
                .Add(p => p.TooltipText, "John Doe"));

            Assert.Equal("JD", cut.Instance.Initials);
            Assert.Equal("John Doe", cut.Instance.TooltipText);
            Assert.Contains("initials=\"JD\"", cut.Markup);
            Assert.Contains("tooltip-text=\"John Doe\"", cut.Markup);
        }
    }
}
