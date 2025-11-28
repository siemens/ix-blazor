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

    public class BreadcrumbTests : TestContextBase
    {
        [Fact]
        public void BreadcrumbRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<Breadcrumb>(parameters =>
            {
                parameters.Add(p => p.Id, "testId");
                parameters.Add(p => p.Ghost, true);
                parameters.Add(p => p.AriaLabelPreviousButton, "previous");
                parameters.Add(p => p.NextItems, ["Data"]);
                parameters.Add(p => p.VisibleItemCount, 9);
            });

            // Assert
            cut.MarkupMatches("<ix-breadcrumb ghost='true' visible-item-count='9' id='testId' aria-label-previous-button='previous'></ix-breadcrumb>");
        }

        [Fact]
        public void BreadcrumbRendersChildContent()
        {
            // Arrange
            var expectedContent = "Expected content";

            // Act
            var cut = RenderComponent<Breadcrumb>(parameters => parameters
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
            var eventTriggered = false;
            var cut = RenderComponent<Breadcrumb>(parameters => parameters.Add(p => p.ItemClicked, EventCallback.Factory.Create<string>(this, () => eventTriggered = true)));

            // Act
            cut.Instance.ItemClicked.InvokeAsync("test");

            // Assert
            Assert.True(eventTriggered);
        }

        [Fact]
        public void NextItemClickedEventTriggeredCorrectly()
        {
            // Arrange
            var eventTriggered = false;
            var cut = RenderComponent<Breadcrumb>(parameters => parameters.Add(p => p.NextItemClicked, EventCallback.Factory.Create<string>(this, () => eventTriggered = true)));

            // Act
            cut.Instance.NextItemClicked.InvokeAsync("test");

            // Assert
            Assert.True(eventTriggered);
        }
    }
}
