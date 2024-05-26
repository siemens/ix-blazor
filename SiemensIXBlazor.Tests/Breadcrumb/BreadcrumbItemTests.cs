using Bunit;
using SiemensIXBlazor.Components;

namespace SiemensIXBlazor.Tests
{
    public class BreadcrumbItemTests: TestContextBase
    {
        [Fact]
        public void BreadcrumbItemRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<BreadcrumbItem>();
        
            // Assert
            cut.MarkupMatches("<ix-breadcrumb-item ></ix-breadcrumb-item>");
        }
    }
}