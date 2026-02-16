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
using SiemensIXBlazor.Enums.PushCard;

namespace SiemensIXBlazor.Tests 
{
    public class ActionCardTests: TestContextBase
    {
        [Fact]
        public void ComponentRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<ActionCard>();

            // Assert
            cut.MarkupMatches("<ix-action-card variant='outline'></ix-action-card>");
        }

        [Fact]
        public void IconPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<ActionCard>(parameters => parameters.Add(p => p.Icon, "testIcon"));

            // Assert
            Assert.Equal("testIcon", cut.Instance.Icon);
        }

        [Fact]
        public void HeadingPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<ActionCard>(parameters => parameters.Add(p => p.Heading, "testHeading"));

            // Assert
            Assert.Equal("testHeading", cut.Instance.Heading);
        }

        [Fact]
        public void SubHeadingPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<ActionCard>(parameters => parameters.Add(p => p.SubHeading, "testSubHeading"));

            // Assert
            Assert.Equal("testSubHeading", cut.Instance.SubHeading);
        }

        [Fact]
        public void SelectedPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<ActionCard>(parameters => parameters.Add(p => p.Selected, true));

            // Assert
            Assert.True(cut.Instance.Selected);
        }

        [Fact]
        public void VariantPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<ActionCard>(parameters => parameters.Add(p => p.Variant, PushCardVariant.outline));

            // Assert
            Assert.Equal(PushCardVariant.outline, cut.Instance.Variant);
        }

        [Fact]
        public void PassiveDefaultsToFalse()
        {
            // Arrange
            var cut = RenderComponent<ActionCard>();

            // Assert
            Assert.False(cut.Instance.Passive);
            Assert.DoesNotContain("passive", cut.Markup);
        }

        [Fact]
        public void PassiveTrueRendersAttribute()
        {
            // Arrange
            var cut = RenderComponent<ActionCard>(parameters => parameters
                .Add(p => p.Passive, true));

            // Assert
            Assert.True(cut.Instance.Passive);
            Assert.Contains("passive", cut.Markup);
        }
    }
}