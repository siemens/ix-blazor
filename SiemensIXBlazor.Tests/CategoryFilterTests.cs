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
using SiemensIXBlazor.Enums.CategoryFilter;
using SiemensIXBlazor.Objects;
using System.Text.Json;

namespace SiemensIXBlazor.Tests
{
    public class CategoryFilterTests : TestContextBase
    {
        [Fact]
        public void CategoryFilterRendersWithoutCrashing()
        {
            // Arrange
            var cut = Render<CategoryFilter>(parameters =>
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
            cut.MarkupMatches("<ix-category-filter id=\"testId\" placeholder=\"TestPlaceholder\" hide-icon=\"true\" i18n-plain-text=\"testI18PlainText\" icon=\"testIcon\" label-categories=\"testLabelCategories\" unique-categories='true' readonly='true' disabled='true'></ix-category-filter>");
        }

        [Fact]
        public void CategoriesSetsCorrectly()
        {
            // Arrange
            var mockCategories = new Dictionary<string, Category> { { "category1", new() { Label = "Test Label", Options = ["options1"] } } };

            var cut = Render<CategoryFilter>(parameters => parameters
                .Add(p => p.Categories, mockCategories));

            // Assert
            Assert.Equal(cut.Instance.Categories, mockCategories);
        }

        [Fact]
        public void FilterStateSetsCorrectly()
        {
            // Arrange
            var mockFilterState = new FilterState { Categories = [new FilterStateCategory { Id = "testId", Operator = LogicalFilterOperator.NotEqual, Value = "testValue" }] };

            var cut = Render<CategoryFilter>(parameters => parameters
                .Add(p => p.FilterState, mockFilterState));

            // Assert
            Assert.Equal(cut.Instance.FilterState, mockFilterState);
        }

        [Fact]
        public void NonSelectableCategoriesSetsCorrectly()
        {
            // Arrange
            var mockNonSelectableCategories = new Dictionary<string, string> { { "test", "test" } };

            var cut = Render<CategoryFilter>(parameters => parameters
                .Add(p => p.NonSelectableCategories, mockNonSelectableCategories));

            // Assert
            Assert.Equal(cut.Instance.NonSelectableCategories, mockNonSelectableCategories);
        }

        [Fact]
        public void SuggestionsSetsCorrectly()
        {
            // Arrange
            var mockSuggestions = new string[] { "test", "test2" };

            var cut = Render<CategoryFilter>(parameters => parameters
                .Add(p => p.Suggestions, mockSuggestions));

            // Assert
            Assert.Equal(cut.Instance.Suggestions, mockSuggestions);
        }

        [Fact]
        public async Task FilterChangedEventReceivesTypedState()
        {
            FilterState? received = null;
            var cut = Render<CategoryFilter>(parameters => parameters
                .Add(p => p.FilterChangedEvent, EventCallback.Factory.Create<FilterState>(this, state => received = state)));

            var payload = JsonDocument.Parse("""
                {"tokens":["test"],"categories":[{"id":"vendor","value":"Siemens","operator":"Equal"}]}
                """).RootElement;
            await cut.Instance.FilterChanged(payload);

            Assert.Equal("test", received!.Tokens[0]);
            Assert.Equal(LogicalFilterOperator.Equal, received.Categories[0].Operator);
        }

        [Fact]
        public async Task InputChangedEventReceivesTypedState()
        {
            InputState? received = null;
            var cut = Render<CategoryFilter>(parameters => parameters
                .Add(p => p.InputChangedEvent, EventCallback.Factory.Create<InputState>(this, state => received = state)));

            var payload = JsonDocument.Parse("""{"token":"Sie","category":"vendor"}""").RootElement;
            await cut.Instance.InputChanged(payload);

            Assert.Equal("Sie", received!.Token);
            Assert.Equal("vendor", received.Category);
            Assert.True(received.HasCategory());
        }

        [Fact]
        public async Task CategoryChangedEventPassesCategoryAndSupportsClear()
        {
            string? category = "not-set";
            var cleared = false;
            var cut = Render<CategoryFilter>(parameters => parameters
                .Add(p => p.CategoryChangedEvent, EventCallback.Factory.Create<string?>(this, value => category = value))
                .Add(p => p.FilterClearedEvent, EventCallback.Factory.Create<FilterClearedEventArgs>(this, _ => cleared = true)));

            await cut.Instance.CategoryChanged("category");
            await cut.Instance.CategoryChanged(null);
            var canceled = await cut.Instance.FilterCleared();

            Assert.Null(category);
            Assert.True(cleared);
            Assert.False(canceled);
        }

        [Fact]
        public async Task FilterClearedCanBeCanceled()
        {
            var cut = Render<CategoryFilter>(parameters => parameters
                .Add(p => p.FilterClearedEvent, EventCallback.Factory.Create<FilterClearedEventArgs>(this, eventArgs => eventArgs.Cancel = true)));

            Assert.True(await cut.Instance.FilterCleared());
        }

        [Fact]
        public void FilterStateUsesOfficialOperatorStrings()
        {
            var state = new FilterState
            {
                Categories = [new() { Id = "vendor", Value = "Siemens", Operator = LogicalFilterOperator.NotEqual }]
            };

            var json = JsonSerializer.Serialize(state);
            var deserialized = JsonSerializer.Deserialize<FilterState>(json);

            Assert.Contains("\"operator\":\"Not equal\"", json);
            Assert.Equal(LogicalFilterOperator.NotEqual, deserialized!.Categories[0].Operator);
        }

        [Fact]
        public void RepeatedEquivalentStateDoesNotCauseRecursiveRendering()
        {
            var cut = Render<CategoryFilter>(parameters => parameters
                .Add(p => p.Id, "category-filter"));

            for (var index = 0; index < 100; index++)
            {
                cut.Render(parameters => parameters
                    .Add(p => p.Categories, new Dictionary<string, Category>
                    {
                        ["vendor"] = new() { Label = "Vendor", Options = ["Siemens"] }
                    })
                    .Add(p => p.FilterState, new FilterState
                    {
                        Tokens = ["test"],
                        Categories = []
                    })
                    .Add(p => p.NonSelectableCategories, new Dictionary<string, string>
                    {
                        ["archived"] = "Archived"
                    })
                    .Add(p => p.Suggestions, ["test"]));
            }

            Assert.Equal("category-filter", cut.Instance.Id);
        }

        [Fact]
        public void EnableTopLayerDefaultsToFalse()
        {
            // Arrange
            var cut = Render<CategoryFilter>(parameters => parameters
                .Add(p => p.Id, "test-id"));

            // Assert
            Assert.False(cut.Instance.EnableTopLayer);
            Assert.DoesNotContain("enable-top-layer", cut.Markup);
        }

        [Fact]
        public void EnableTopLayerTrueRendersAttribute()
        {
            // Arrange
            var cut = Render<CategoryFilter>(parameters => parameters
                .Add(p => p.Id, "test-id")
                .Add(p => p.EnableTopLayer, true));

            // Assert
            Assert.True(cut.Instance.EnableTopLayer);
            Assert.Contains("enable-top-layer", cut.Markup);
        }
    }
}
