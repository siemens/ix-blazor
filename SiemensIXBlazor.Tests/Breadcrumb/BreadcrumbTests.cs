// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

namespace SiemensIXBlazor.Tests
{
    using Bunit;
    using Microsoft.AspNetCore.Components;
    using SiemensIXBlazor.Components;
    using SiemensIXBlazor.Objects.Breadcrumb;
    using System.Text.Json;

    public class BreadcrumbTests : TestContextBase
    {
        [Fact]
        public void BreadcrumbRendersWithoutCrashing()
        {
            // Arrange
            var cut = Render<Breadcrumb>(parameters =>
            {
                parameters.Add(p => p.Id, "testId");
                parameters.Add(p => p.Subtle, true);
                parameters.Add(p => p.AriaLabelPreviousButton, "Show previous breadcrumb items");
                parameters.Add(p => p.NextItems, [new BreadcrumbClick { BreadcrumbKey = "data", Label = "Data" }]);
                parameters.Add(p => p.VisibleItemCount, 9);
            });

            // Assert
            cut.MarkupMatches("<ix-breadcrumb subtle='true' visible-item-count='9' id='testId' aria-label-previous-button='Show previous breadcrumb items'></ix-breadcrumb>");
        }

        [Fact]
        public void BreadcrumbRendersChildContent()
        {
            // Arrange
            var expectedContent = "Expected content";

            // Act
            var cut = Render<Breadcrumb>(parameters => parameters
                .Add(p => p.ChildContent, builder =>
                {
                    builder.AddContent(0, expectedContent);
                }));

            // Assert
            Assert.Contains(expectedContent, cut.Markup);
        }

        [Fact]
        public void ItemClickedEventTriggeredCorrectly()
        {
            // Arrange
            BreadcrumbClick? clickedItem = null;
            var cut = Render<Breadcrumb>(parameters => parameters.Add(p => p.ItemClicked, EventCallback.Factory.Create<BreadcrumbClick>(this, item => clickedItem = item)));

            // Act
            cut.Instance.BreadcrumbItemClicked(JsonSerializer.SerializeToElement(new { breadcrumbKey = "test-key", label = "test" }));

            // Assert
            Assert.Equal("test-key", clickedItem?.BreadcrumbKey);
            Assert.Equal("test", clickedItem?.Label);
        }

        [Fact]
        public void NextItemClickedEventTriggeredCorrectly()
        {
            // Arrange
            BreadcrumbNextClick? clickedItem = null;
            var cut = Render<Breadcrumb>(parameters => parameters.Add(p => p.NextItemClicked, EventCallback.Factory.Create<BreadcrumbNextClick>(this, item => clickedItem = item)));

            // Act
            cut.Instance.BreadcrumbNextItemClicked(JsonSerializer.SerializeToElement(new { @event = new { type = "click" }, item = new { breadcrumbKey = "test-key", label = "test" } }));

            // Assert
            Assert.Equal("test-key", clickedItem?.Item.BreadcrumbKey);
            Assert.Equal("click", clickedItem?.Event.GetProperty("type").GetString());
        }

        [Fact]
        public void EnableTopLayerDefaultsToFalse()
        {
            // Arrange
            var cut = Render<Breadcrumb>(parameters => parameters
                .Add(p => p.Id, "test-id"));

            // Assert
            Assert.False(cut.Instance.EnableTopLayer);
            Assert.DoesNotContain("enable-top-layer", cut.Markup);
        }

        [Fact]
        public void EnableTopLayerTrueRendersAttribute()
        {
            // Arrange
            var cut = Render<Breadcrumb>(parameters => parameters
                .Add(p => p.Id, "test-id")
                .Add(p => p.EnableTopLayer, true));

            // Assert
            Assert.True(cut.Instance.EnableTopLayer);
            Assert.Contains("enable-top-layer", cut.Markup);
        }
    }
}
