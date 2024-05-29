
using Bunit;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.MessageBar;

namespace SiemensIXBlazor.Tests;

public class MessageBarTests : TestContextBase
{
    [Fact]
    public void MessageBarRendersCorrectly()
    {
        // Arrange

        RenderFragment childContent = builder =>
        {
            builder.AddContent(0, "Simple Text");
        };

        // Act
        var cut = RenderComponent<MessageBar>(
            ("Id", "testId"),
            ("Class", "test-class"),
            ("Style", "width: 100%"),
            ("Dismissible", true),
            ("Type", MessageBarType.Info),
            ("ChildContent", childContent));

        // Assert
        cut.MarkupMatches("<ix-message-bar id=\"testId\" class=\"test-class\" style=\"width: 100%\" dismissible=\"\" type=\"info\">Simple Text</ix-message-bar>");
    }

    [Fact]
    public async Task ClosedChangeEventWorks()
    {
        // Arrange
        var closed = false;
        var cut = RenderComponent<MessageBar>(
            ("Id", "messageBar"),
            ("ClosedChangeEvent", EventCallback.Factory.Create(this, () => closed = true))
        );

        // Act
        cut.Instance.ClosedChange();

        // Assert
        Assert.True(closed);
    }
}