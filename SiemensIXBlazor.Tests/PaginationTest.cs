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
using Xunit;
using SiemensIXBlazor.Components.Pagination;

namespace SiemensIXBlazor.Tests
{
    public class PaginationTests : TestContextBase
    {
        [Fact]
        public void PaginationRendersCorrectly()
        {
            // Arrange
            var cut = Render<Pagination>(parameters =>
                {
                    parameters.Add(p => p.Id, "testId");
                    parameters.Add(p => p.ItemCount, 15);
                    parameters.Add(p => p.SelectedPage, 1);
                    parameters.Add(p => p.HideItemCount, true);
                    parameters.Add(p => p.AriaLabelPageSelection, "testAriaLabelPageSelection");
                }
            );
            // Assert
            cut.MarkupMatches("<ix-pagination id=\"testId\" item-count=\"15\" selected-page=\"1\" hide-item-count=\"\" aria-label-page-selection=\"testAriaLabelPageSelection\" i18n-items=\"Items\" i18n-of=\"of\" i18n-page=\"Page\"></ix-pagination>");
        }

        [Fact]
        public async Task ItemCountChangedEventWorks()
        {
            // Arrange
            var itemCount = 0;
            var cut = Render<Components.Pagination.Pagination>(parameters => parameters
                .Add(p => p.Id, "pagination")
                .Add(p => p.ItemCountChangedEvent, EventCallback.Factory.Create(this, (int count) => itemCount = count))
            );

            // Act
            await cut.Instance.ItemCountChanged(20);

            // Assert
            Assert.Equal(20, itemCount);
        }

        [Fact]
        public async Task PageSelectedEventWorks()
        {
            // Arrange
            var selectedPage = 0;
            var cut = Render<Components.Pagination.Pagination>(parameters => parameters
                .Add(p => p.Id, "pagination")
                .Add(p => p.PageSelectedEvent, EventCallback.Factory.Create(this, (int page) => selectedPage = page))
            );

            // Act
            await cut.Instance.PageSelected(2);

            // Assert
            Assert.Equal(2, selectedPage);
        }

        [Fact]
        public void ItemCountOptionsIsNullByDefault()
        {
            // Arrange & Act
            var cut = Render<Components.Pagination.Pagination>(parameters =>
                parameters.Add(p => p.Id, "testId")
            );

            // Assert
            Assert.Null(cut.Instance.ItemCountOptions);
            Assert.DoesNotContain("item-count-options", cut.Markup);
        }

        [Fact]
        public void ItemCountOptionsRendersAsCommaSeparatedString()
        {
            // Arrange & Act
            var cut = Render<Components.Pagination.Pagination>(parameters =>
            {
                parameters.Add(p => p.Id, "testId");
                parameters.Add(p => p.ItemCountOptions, new[] { 15, 25, 50 });
            });

            // Assert
            var element = cut.Find("ix-pagination");
            Assert.Equal("15,25,50", element.GetAttribute("item-count-options"));
        }
    }
}
