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

namespace SiemensIXBlazor.Tests.Dropdown;

public class DropdownTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithCorrectProperties()
    {
        // Arrange
        var cut = RenderComponent<Components.Dropdown>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.Anchor, "testAnchor")
            //.Add(p => p.CloseBehavior, "both")
            .Add(p => p.Header, "testHeader")
            .Add(p => p.Placement, "bottom-start")
            .Add(p => p.PositioningStrategy, "fixed")
            .Add(p => p.Show, false)
            .Add(p => p.SuppressAutomaticPlacement, false)
            .Add(p => p.Trigger, "testTrigger")
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content"))));

        // Assert
        cut.MarkupMatches(
            "<ix-dropdown id=\"testId\" trigger=\"testTrigger\" anchor=\"testAnchor\" close-behavior=\"both\" header=\"testHeader\" placement=\"bottom-start\" positioning-strategy=\"fixed\">Test content</ix-dropdown>");
    }

    [Fact]
    public void EventCallbacksAreTriggeredCorrectly()
    {
        // Arrange
        var isShowChangedEventTriggered = false;

        var cut = RenderComponent<Components.Dropdown>(parameters => parameters
            .Add(p => p.ShowChangedEvent,
                EventCallback.Factory.Create<bool>(this, value => isShowChangedEventTriggered = true))
            .Add(p => p.Id, "testId"));

        // Act
        cut.Instance.ShowChanged(true);

        // Assert
        Assert.True(isShowChangedEventTriggered);
    }
}