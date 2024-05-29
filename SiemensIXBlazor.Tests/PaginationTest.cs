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
            var cut = RenderComponent<Components.Pagination.Pagination>(
                ("Id", "testId"),
                ("ItemCount", 15),
                ("SelectedPage", 1),
                ("ShowItemCount", true)
            );

            // Assert

            cut.MarkupMatches("<ix-pagination id=\"testId\" item-count=\"15\" selected-page=\"1\" show-item-count  i-1-8n-items=\"Items\" i-1-8n-of=\"of\" i-1-8n-page=\"Page\"></ix-pagination>");
        }

        [Fact]
        public async Task ItemCountChangedEventWorks()
        {
            // Arrange
            var itemCount = 0;
            var cut = RenderComponent<Components.Pagination.Pagination>(
                ("Id", "pagination"),
                ("ItemCountChangedEvent", EventCallback.Factory.Create(this, (int count) => itemCount = count))
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
            var cut = RenderComponent<Components.Pagination.Pagination>(
                ("Id", "pagination"),
                ("PageSelectedEvent", EventCallback.Factory.Create(this, (int page) => selectedPage = page))
            );

            // Act
            await cut.Instance.PageSelected(2);

            // Assert
            Assert.Equal(2, selectedPage);
        }
    }
}