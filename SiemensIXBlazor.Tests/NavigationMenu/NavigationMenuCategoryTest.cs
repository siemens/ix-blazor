using Bunit;
using Microsoft.AspNetCore.Components;
using Xunit;
using SiemensIXBlazor.Components.NavigationMenu;

namespace SiemensIXBlazor.Tests.NavigationMenu
{
    public class NavigationMenuCategoryTests : TestContextBase
    {
        [Fact]
        public void NavigationMenuCategoryRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<NavigationMenuCategory>(
                ("Icon", "testIcon"),
                ("Label", "Test Label"),
                ("Notifications", 5),
                ("ChildContent", (RenderFragment)(builder =>
                {
                    builder.OpenElement(0, "div");
                    builder.AddContent(1, "Test child content");
                    builder.CloseElement();
                }))
            );

            // Assert
            // Adjust the expected markup to match your component's output
            cut.MarkupMatches("<ix-menu-category icon=\"testIcon\" label=\"Test Label\" notifications=\"5\"><div>Test child content</div></ix-menu-category>");
        }
    }
}