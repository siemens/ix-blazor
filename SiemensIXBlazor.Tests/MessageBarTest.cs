// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------


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
        var cut = Render<MessageBar>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.Class, "test-class")
            .Add(p => p.Style, "width: 100%")
            .Add(p => p.Persistent, true)
            .Add(p => p.Type, MessageBarType.Info)
            .Add(p => p.ChildContent, childContent)
        );

        // Assert
        cut.MarkupMatches("<ix-message-bar id=\"testId\" class=\"test-class\" style=\"width: 100%\" persistent=\"true\" type=\"info\">Simple Text</ix-message-bar>");
    }

    [Fact]
    public async Task ClosedChangeEventWorks()
    {
        // Arrange
        var closed = false;
        var cut = Render<MessageBar>(parameters => parameters
            .Add(p => p.Id, "messageBar")
            .Add(p => p.ClosedChangeEvent, EventCallback.Factory.Create(this, () => closed = true))
        );

        // Act
        cut.Instance.ClosedChange();

        // Assert
        Assert.True(closed);
    }

    [Fact]
    public void CloseAnimationCompletedEventWorks()
    {
        // Arrange
        var animationCompleted = false;
        var cut = Render<MessageBar>(parameters => parameters
            .Add(p => p.Id, "messageBar")
            .Add(p => p.CloseAnimationCompletedEvent, EventCallback.Factory.Create(this, () => animationCompleted = true))
        );

        // Act
        cut.Instance.CloseAnimationCompleted();

        // Assert
        Assert.True(animationCompleted);
    }
}
