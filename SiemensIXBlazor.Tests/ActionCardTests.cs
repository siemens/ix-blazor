using Bunit;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.PushCard;

namespace SiemensIXBlazor.Tests 
{
    public class ActionCardTests: TestContextBase
    {
        [Fact]
        public void ComponentRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<ActionCard>();

            // Assert
            cut.MarkupMatches("<ix-action-card variant='insight'></ix-action-card>");
        }

        [Fact]
        public void IconPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<ActionCard>(parameters => parameters.Add(p => p.Icon, "testIcon"));

            // Assert
            Assert.Equal("testIcon", cut.Instance.Icon);
        }

        [Fact]
        public void HeadingPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<ActionCard>(parameters => parameters.Add(p => p.Heading, "testHeading"));

            // Assert
            Assert.Equal("testHeading", cut.Instance.Heading);
        }

        [Fact]
        public void SubHeadingPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<ActionCard>(parameters => parameters.Add(p => p.SubHeading, "testSubHeading"));

            // Assert
            Assert.Equal("testSubHeading", cut.Instance.SubHeading);
        }

        [Fact]
        public void SelectedPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<ActionCard>(parameters => parameters.Add(p => p.Selected, true));

            // Assert
            Assert.True(cut.Instance.Selected);
        }

        [Fact]
        public void VariantPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<ActionCard>(parameters => parameters.Add(p => p.Variant, PushCardVariant.insight));

            // Assert
            Assert.Equal(PushCardVariant.insight, cut.Instance.Variant);
        }
    }
}