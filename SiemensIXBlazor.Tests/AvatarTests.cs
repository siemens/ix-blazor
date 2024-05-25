using Bunit;
using SiemensIXBlazor.Components.Avatar;

namespace SiemensIXBlazor.Tests
{
    public class AvatarTests : TestContextBase
    {
        [Fact]
        public void AvatarRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<Avatar>(parameters => {
                parameters.Add(p => p.Image, "testImage");
                parameters.Add(p => p.Initials, "testInitials");
            });
        
            // Assert
            cut.MarkupMatches("<ix-avatar image='testImage' initials='testInitials'></ix-avatar>");
        }
    }
}