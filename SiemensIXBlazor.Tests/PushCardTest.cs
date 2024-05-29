using Bunit;
using Xunit;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.PushCard;

namespace SiemensIXBlazor.Tests
{
    public class PushCardTests : TestContextBase
    {
        [Fact]
        public void PushCardRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<PushCard>(
                ("Heading", "Test Heading"),
                ("Icon", "testIcon"),
                ("Notification", "5"),
                ("SubHeading", "Test SubHeading"),
                ("Collapsed", true),
                ("Variant", PushCardVariant.insight)
            );

            // Assert
        
            cut.MarkupMatches("<ix-push-card heading=\"Test Heading\" icon=\"testIcon\" notification=\"5\" subheading=\"Test SubHeading\" collapsed=\"\" variant=\"insight\"></ix-push-card>");
        }
    }
}