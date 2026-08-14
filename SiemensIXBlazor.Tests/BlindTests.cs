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
    public class BlindTests: TestContextBase
    {
        [Fact]
        public void ComponentRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<Blind>();

            // Assert
            cut.MarkupMatches("<ix-blind id='' variant='filled'></ix-blind>");
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
            var cut = RenderComponent<Blind>(parameters => parameters.Add(p => p.Collapsed, true));

            // Assert
            Assert.True(cut.Instance.Collapsed);
        }

        [Fact]
        public void IconPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Blind>(parameters => parameters.Add(p => p.Icon, "testIcon"));

            // Assert
            Assert.Equal("testIcon", cut.Instance.Icon);
        }

        [Fact]
        public void VariantPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Blind>(parameters => parameters.Add(p => p.Variant, BlindVariant.filled));

            // Assert
            Assert.Equal(BlindVariant.filled, cut.Instance.Variant);
        }

        [Fact]
        public void BlindRendersCustomHeaderAndHeaderActionsSlots()
        {
            var cut = RenderComponent<Blind>(parameters => parameters
                .Add(p => p.CustomHeader, builder => builder.AddContent(0, "Custom header"))
                .Add(p => p.HeaderActions, builder => builder.AddContent(0, "Actions"))
                .Add(p => p.ChildContent, builder => builder.AddContent(0, "Blind content")));

            Assert.Contains("slot=\"custom-header\"", cut.Markup);
            Assert.Contains("Custom header", cut.Markup);
            Assert.Contains("slot=\"header-actions\"", cut.Markup);
            Assert.Contains("Actions", cut.Markup);
            Assert.Contains("Blind content", cut.Markup);
        }

        [Fact]
        public async Task CollapsedChangedEventPassesTypedValue()
        {
            var collapsed = false;
            var cut = RenderComponent<Blind>(parameters => parameters
                .Add(p => p.CollapsedChangedEvent, EventCallback.Factory.Create<bool>(this, value => collapsed = value)));

            await cut.Instance.CollapsedChanged(true);

            Assert.True(collapsed);
        }

        [Fact]
        public void CollapsedChangedEventTriggeredCorrectly()
        {
            // Arrange
            var eventTriggered = false;
            var cut = RenderComponent<Blind>(parameters => parameters.Add(p => p.CollapsedChangedEvent, EventCallback.Factory.Create<bool>(this, () => eventTriggered = true)));

            // Act
            cut.Instance.CollapsedChangedEvent.InvokeAsync(true);

            // Assert
            Assert.True(eventTriggered);
        }
    }
}
