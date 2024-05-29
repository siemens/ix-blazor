using Bunit;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Components.About;

namespace SiemensIXBlazor.Tests.About
{
    public class AboutMenuItemTest: TestContextBase
    {
        [Fact]
        public void ComponentRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<AboutMenuItem>();

            // Assert
            cut.MarkupMatches("<ix-menu-about-item></ix-menu-about-item>");
        }

        [Fact]
        public void ChildContentPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<AboutMenuItem>(parameters => parameters.Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content"))));

            // Assert
            Assert.NotNull(cut.Instance.ChildContent);
        }

        [Fact]
        public void LablePropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<AboutMenuItem>(parameters => parameters.Add(p => p.Label, "testLabel"));

            // Assert
            Assert.Equal("testLabel", cut.Instance.Label);
        }
    }
}
