using Bunit;
using SiemensIXBlazor.Components.BasicNavigation;
using SiemensIXBlazor.Enums.BasicNavigation;

namespace SiemensIXBlazor.Tests
{
    public class BasicNavigationTests : TestContextBase
    {
        [Fact]
        public void BasicNavigationRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<BasicNavigation>(parameters => {
                parameters.Add(p => p.ApplicationName, "testApplicationName");
                parameters.Add(p => p.HideHeader, true);
                parameters.Add(p => p.ForceBreakpoint, Breakpoint.Md);
            });
        
            // Assert
            cut.MarkupMatches("<ix-basic-navigation application-name='testApplicationName' hide-header='' force-breakpoint='md'></ix-basic-navigation>");
        }

        [Fact]
        public void BasicNavigationRendersChildContent()
        {
            // Arrange
            var expectedContent = "Expected content";

            // Act
            var cut = RenderComponent<BasicNavigation>(parameters => parameters
                .Add(p => p.ChildContent, builder => 
                {
                    builder.AddContent(0, expectedContent);
                }));

            // Assert
            Assert.Contains(expectedContent, cut.Markup);
        }
    }
}