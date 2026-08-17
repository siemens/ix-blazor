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
    public class CardTests : TestContextBase
    {
        [Fact]
        public void CardRendersWithoutCrashing()
        {
            // Arrange
            var cut = Render<Card>(parameters =>
            {
                parameters.Add(p => p.Selected, true);
                parameters.Add(p => p.Variant, Enums.CardVariant.neutral);
            });

            // Assert
            cut.MarkupMatches("<ix-card variant=\"neutral\" selected=\"\"></ix-card>");
        }

        [Fact]
        public void CardRendersChildContent()
        {
            // Arrange
            var expectedContent = "Expected content";

            // Act
            var cut = Render<Card>(parameters => parameters
                .Add(p => p.ChildContent, builder =>
                {
                    builder.AddContent(0, expectedContent);
                }));

            // Assert
            cut.MarkupMatches("<ix-card variant=\"outline\">Expected content</ix-card>");
        }

        [Fact]
        public void PassiveDefaultsToFalse()
        {
            // Arrange
            var cut = Render<Card>((Action<Bunit.ComponentParameterCollectionBuilder<Card>>)(_ => { }));

            // Assert
            Assert.False(cut.Instance.Passive);
            Assert.DoesNotContain("passive", cut.Markup);
        }

        [Fact]
        public void PassiveTrueRendersAttribute()
        {
            // Arrange
            var cut = Render<Card>(parameters => parameters
                .Add(p => p.Passive, true));

            // Assert
            Assert.True(cut.Instance.Passive);
            Assert.Contains("passive", cut.Markup);
        }

        [Fact]
        public void SelectedDefaultsToFalse()
        {
            var cut = Render<Card>((Action<Bunit.ComponentParameterCollectionBuilder<Card>>)(_ => { }));

            Assert.False(cut.Instance.Selected);
            Assert.DoesNotContain("selected", cut.Markup);
        }
    }
}
