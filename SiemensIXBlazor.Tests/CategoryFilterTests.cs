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
using SiemensIXBlazor.Components.CategoryFilter;
using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Tests
{
    public class CategoryFilterTests : TestContextBase
    {
        [Fact]
        public void CategoryFilterRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<CategoryFilter>(parameters =>
            {
                parameters.Add(p => p.Id, "testId");
                parameters.Add(p => p.HideIcon, true);
                parameters.Add(p => p.I18nPlainText, "testI18PlainText");
                parameters.Add(p => p.Icon, "testIcon");
                parameters.Add(p => p.LabelCategories, "testLabelCategories");
                parameters.Add(p => p.Placeholder, "TestPlaceholder");
                parameters.Add(p => p.UniqueCategories, true);
                parameters.Add(p => p.Suggestions, ["testSugestion"]);
                parameters.Add(p => p.Disabled, true);
                parameters.Add(p => p.Readonly, true);
            });

            // Assert
            cut.MarkupMatches("<ix-category-filter id=\"testId\" placeholder=\"TestPlaceholder\" hide-icon=\"\" i-1-8n-plain-text=\"testI18PlainText\" icon=\"testIcon\" label-categories=\"testLabelCategories\" unique-categories readonly disabled></ix-category-filter>");
        }

        [Fact]
        public void CategoriesSetsCorrectly()
        {
            // Arrange
            var cut = RenderComponent<CategoryFilter>();
            var mockCategories = new Dictionary<string, Category> { { "category1", new() { Label = "Test Label", Options = ["options1"] } } };

            // Act
            cut.Instance.Categories = mockCategories;

            // Assert
            Assert.Equal(cut.Instance.Categories, mockCategories);
        }

        [Fact]
        public void FilterStateSetsCorrectly()
        {
            // Arrange
            var cut = RenderComponent<CategoryFilter>();
            var mockFilterState = new FilterState { Categories = [new FilterStateCategory { Id = "testId", Operator = "testOperator", Value = "testValue" }] };

            // Act
            cut.Instance.FilterState = mockFilterState;

            // Assert
            Assert.Equal(cut.Instance.FilterState, mockFilterState);
        }

        [Fact]
        public void NonSelectableCategoriesSetsCorrectly()
        {
            // Arrange
            var cut = RenderComponent<CategoryFilter>();
            var mockNonSelectableCategories = new Dictionary<string, string> { { "test", "test" } };

            // Act
            cut.Instance.NonSelectableCategories = mockNonSelectableCategories;

            // Assert
            Assert.Equal(cut.Instance.NonSelectableCategories, mockNonSelectableCategories);
        }

        [Fact]
        public void SuggestionsSetsCorrectly()
        {
            // Arrange
            var cut = RenderComponent<CategoryFilter>();
            var mockSuggestions = new string[] { "test", "test2" };

            // Act
            cut.Instance.Suggestions = mockSuggestions;

            // Assert
            Assert.Equal(cut.Instance.Suggestions, mockSuggestions);
        }

        [Fact]
        public void FilterChangedEventTriggeredCorrectly()
        {
            // Arrange
            var eventTriggered = false;
            var cut = RenderComponent<CategoryFilter>(parameters => parameters.Add(p => p.FilterChangedEvent, EventCallback.Factory.Create<FilterState>(this, () => eventTriggered = true)));

            // Act
            cut.Instance.FilterChangedEvent.InvokeAsync();

            // Assert
            Assert.True(eventTriggered);
        }

        [Fact]
        public void InputChangedEventTriggeredCorrectly()
        {
            // Arrange
            var eventTriggered = false;
            var cut = RenderComponent<CategoryFilter>(parameters => parameters.Add(p => p.InputChangedEvent, EventCallback.Factory.Create<dynamic>(this, () => eventTriggered = true)));

            // Act
            cut.Instance.InputChangedEvent.InvokeAsync();

            // Assert
            Assert.True(eventTriggered);
        }
    }
}
