using Bunit;
using SiemensIXBlazor.Components;

namespace SiemensIXBlazor.Tests
{
    public class ApplicationHeaderTests : TestContextBase
    {
        [Fact]
        public void ApplicationHeaderRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<ApplicationHeader>(parameters => {
                parameters.Add(p => p.Name, "testName");
            });

            // Assert
            cut.MarkupMatches("<ix-application-header name='testName'></ix-application-header>");
        }

        [Fact]
        public void ApplicationHeaderRendersChildContent()
        {
            // Arrange
            var expectedContent = "Expected content";

            // Act
            var cut = RenderComponent<ApplicationHeader>(parameters => parameters
                .Add(p => p.ChildContent, builder => 
                {
                    builder.AddContent(0, expectedContent);
                }));

            // Assert
            Assert.Contains(expectedContent, cut.Markup);
        }
    }
}