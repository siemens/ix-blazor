// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Components;

namespace SiemensIXBlazor.Tests
{
    public class CardListTests: TestContextBase
    {
        [Fact]
        public void CardListRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<CardList>(parameters => {
                parameters.Add(p => p.Id, "testId");
                parameters.Add(p => p.Collapse, true);
                parameters.Add(p => p.I18NMoreCards, "testMoreCards");
                parameters.Add(p => p.I18NShowAll, "testShowAll");
                parameters.Add(p => p.Label, "testLabel");
                parameters.Add(p => p.ListStyle, Enums.CardList.CardListStyle.Stack);
                parameters.Add(p => p.ShowAllCount, 1);
                parameters.Add(p => p.SuppressOverflowHandling, true);
            });

            // Assert
            cut.MarkupMatches("<ix-card-list id=\"testId\" label=\"testLabel\" show-all-count=\"1\" list-style=\"stack\" collapse=\"\" i-1-8n-more-cards=\"testMoreCards\" i-1-8n-show-all=\"testShowAll\" suppress-overflow-handling=\"\"></ix-card-list>");
        }

        [Fact]
        public void CollapsedChangedEventTriggeredCorrectly()
        {
            // Arrange
            var eventTriggered = false;
            var cut = RenderComponent<CardList>(parameters => parameters.Add(p => p.CollapseChangedEvent, EventCallback.Factory.Create<bool>(this, () => eventTriggered = true)));

            // Act
            cut.Instance.CollapseChangedEvent.InvokeAsync(true);

            // Assert
            Assert.True(eventTriggered);
        }

        [Fact]
        public void ShowAllClickedEventTriggeredCorrectly()
        {
            // Arrange
            var eventTriggered = false;
            var cut = RenderComponent<CardList>(parameters => parameters.Add(p => p.ShowAllClickEvent, EventCallback.Factory.Create(this, () => eventTriggered = true)));

            // Act
            cut.Instance.ShowAllClickEvent.InvokeAsync();

            // Assert
            Assert.True(eventTriggered);
        }

        [Fact]
        public void ShowMoreCardClickedEventTriggeredCorrectly()
        {
            // Arrange
            var eventTriggered = false;
            var cut = RenderComponent<CardList>(parameters => parameters.Add(p => p.ShowMoreCardClickEvent, EventCallback.Factory.Create(this, () => eventTriggered = true)));

            // Act
            cut.Instance.ShowMoreCardClickEvent.InvokeAsync();

            // Assert
            Assert.True(eventTriggered);
        }
    }
}
