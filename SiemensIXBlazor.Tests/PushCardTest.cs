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
    public class PushCardTests : TestContextBase
    {
        [Fact]
        public void PushCardRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<PushCard>(
                ("Heading", "Test Heading"),
                ("Icon", "testIcon"),
                ("Notification", "5"),
                ("SubHeading", "Test SubHeading"),
                ("Expanded", true),
                ("Variant", PushCardVariant.outline)
            );

            // Assert

            cut.MarkupMatches("<ix-push-card heading=\"Test Heading\" icon=\"testIcon\" notification=\"5\" subheading=\"Test SubHeading\" expanded=\"\" variant=\"outline\"></ix-push-card>");
        }

        [Fact]
        public void PassiveDefaultsToFalse()
        {
            // Arrange
            var cut = RenderComponent<PushCard>();

            // Assert
            Assert.False(cut.Instance.Passive);
            Assert.DoesNotContain("passive", cut.Markup);
        }

        [Fact]
        public void PassiveTrueRendersAttribute()
        {
            // Arrange
            var cut = RenderComponent<PushCard>(parameters => parameters
                .Add(p => p.Passive, true));

            // Assert
            Assert.True(cut.Instance.Passive);
            Assert.Contains("passive", cut.Markup);
        }
    }
}