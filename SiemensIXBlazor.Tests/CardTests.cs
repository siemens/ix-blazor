using Bunit;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Components.BasicNavigation;

namespace SiemensIXBlazor.Tests
{
    public class CardTests: TestContextBase
    {
        [Fact]
        public void CardRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<Card>(parameters => {
                parameters.Add(p => p.Selected, true);
                parameters.Add(p => p.Variant, Enums.CardVariant.neutral);
            });

            // Assert
            cut.MarkupMatches("<ix-card variant=\"neutral\" selected=\"\">\r\n      <ix-card-content></ix-card-content>\r\n    </ix-card>");
        }

        [Fact]
        public void CardRendersChildContent()
        {
            // Arrange
            var expectedContent = "Expected content";

            // Act
            var cut = RenderComponent<Card>(parameters => parameters
                .Add(p => p.ChildContent, builder =>
                {
                    builder.AddContent(0, expectedContent);
                }));

            // Assert
            cut.MarkupMatches("<ix-card variant=\"insight\">\r\n      <ix-card-content>Expected content</ix-card-content>\r\n    </ix-card>");
        }
    }
}
