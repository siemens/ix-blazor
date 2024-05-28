using SiemensIXBlazor.Components;

namespace SiemensIXBlazor.Tests
{
    public class ContentTests: TestContextBase
    {
        [Fact]
        public void ContentRendersChildContent()
        {
            // Arrange
            var expectedContent = "Expected content";

            // Act
            var cut = RenderComponent<Content>(parameters => parameters
                .Add(p => p.ChildContent, builder =>
                {
                    builder.AddContent(0, expectedContent);
                }));

            // Assert
            Assert.Contains(expectedContent, cut.Markup);
        }
    }
}
