// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.Blind;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Tests
{
    public class BlindTests : TestContextBase
    {
        [Fact]
        public void ComponentRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<Blind>(parameters => parameters.Add(p => p.Id, "testId"));

            // Assert
            cut.MarkupMatches("<ix-blind id='testId' variant='insight'></ix-blind>");
        }

        [Fact]
        public void IdPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Blind>(parameters => parameters.Add(p => p.Id, "testId"));

            // Assert
            Assert.Equal("testId", cut.Instance.Id);
        }

        [Fact]
        public void CollapsedPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Blind>(parameters => parameters.Add(p => p.Collapsed, true).Add(p => p.Id, "testId"));

            // Assert
            Assert.True(cut.Instance.Collapsed);
        }

        [Fact]
        public void IconPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Blind>(parameters => parameters.Add(p => p.Icon, "testIcon").Add(p => p.Id, "testId"));

            // Assert
            Assert.Equal("testIcon", cut.Instance.Icon);
        }

        [Fact]
        public void VariantPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Blind>(parameters => parameters.Add(p => p.Variant, BlindVariant.insight).Add(p => p.Id, "testId"));

            // Assert
            Assert.Equal(BlindVariant.insight, cut.Instance.Variant);
        }

        [Fact]
        public void CollapsedChangedEventTriggeredCorrectly()
        {
            // Arrange
            var eventTriggered = false;
            var cut = RenderComponent<Blind>(parameters => parameters.Add(p => p.CollapsedChangedEvent, EventCallback.Factory.Create<bool>(this, () => eventTriggered = true))
            .Add(p => p.Id, "testId"));

            // Act
            cut.Instance.CollapsedChangedEvent.InvokeAsync(true);

            // Assert
            Assert.True(eventTriggered);
        }
    }
}