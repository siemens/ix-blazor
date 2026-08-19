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
            var cut = Render<PushCard>(parameters => parameters
                .Add(p => p.Heading, "Test Heading")
                .Add(p => p.Icon, "testIcon")
                .Add(p => p.AriaLabelIcon, "Test icon")
                .Add(p => p.Notification, "5")
                .Add(p => p.SubHeading, "Test SubHeading")
                .Add(p => p.Expanded, true)
                .Add(p => p.Variant, PushCardVariant.outline)
            );

            // Assert

            cut.MarkupMatches("<ix-push-card aria-label-icon=\"Test icon\" heading=\"Test Heading\" icon=\"testIcon\" notification=\"5\" subheading=\"Test SubHeading\" expanded=\"true\" variant=\"outline\"></ix-push-card>");
        }

        [Fact]
        public void PassiveDefaultsToFalse()
        {
            // Arrange
            var cut = Render<PushCard>((Action<Bunit.ComponentParameterCollectionBuilder<PushCard>>)(_ => { }));

            // Assert
            Assert.False(cut.Instance.Passive);
            Assert.DoesNotContain("passive", cut.Markup);
        }

        [Fact]
        public void PassiveTrueRendersAttribute()
        {
            // Arrange
            var cut = Render<PushCard>(parameters => parameters
                .Add(p => p.Passive, true));

            // Assert
            Assert.True(cut.Instance.Passive);
            Assert.Contains("passive", cut.Markup);
        }

        [Fact]
        public void ExpandedDefaultsToFalse()
        {
            var cut = Render<PushCard>((Action<Bunit.ComponentParameterCollectionBuilder<PushCard>>)(_ => { }));

            Assert.False(cut.Instance.Expanded);
            Assert.DoesNotContain("expanded", cut.Markup);
        }

        [Fact]
        public void RendersDefaultAndTitleActionContent()
        {
            var cut = Render<PushCard>(parameters => parameters
                .Add(p => p.ChildContent, builder => builder.AddContent(0, "Details"))
                .Add(p => p.TitleAction, builder => builder.AddContent(0, "Action")));

            Assert.Contains("slot=\"title-action\"", cut.Markup);
            Assert.Contains("Action", cut.Markup);
            Assert.Contains("Details", cut.Markup);
        }
    }
}
