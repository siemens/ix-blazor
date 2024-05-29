using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiemensIXBlazor.Components;
using Microsoft.AspNetCore.Components;


namespace SiemensIXBlazor.Tests.MenuSettings


public class MenuSettingsItemTests : TestContext
{

    public void MenuSettingsItemRendersCorrectly()
    {
        // Arrange
        using var ctx = new TestContext();

        RenderFragment childContent = builder =>
        {
            builder.AddContent(0, "Test Content");
        };

        // Act
        var cut = ctx.RenderComponent<MenuSettingsItem>(parameters => parameters
            .Add(p => p.Label, "Test Label")
            .Add(p => p.ChildContent, childContent));

        // Assert
        cut.MarkupMatches("<ix-menu-settings-item label=\"Test Label\">Test Content</ix-menu-settings-item>");
    }
}