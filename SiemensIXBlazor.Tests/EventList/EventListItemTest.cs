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

namespace SiemensIXBlazor.Tests.EventList;

public class EventListItemTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithCorrectProperties()
    {
        // Arrange
        var cut = RenderComponent<EventListItem>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content")))
            .Add(p => p.Chevron, true)
            .Add(p => p.ItemColor, "red")
            .Add(p => p.Disabled, false)
            .Add(p => p.Opacity, 1)
            .Add(p => p.Selected, true)
            .Add(p=>p.Variant,Enums.EventList.EventListVariant.outline)
            .Add(p => p.ItemClickEvent, EventCallback.Factory.Create(this, () => { })));


        // Assert
        cut.MarkupMatches(
            "<ix-event-list-item id=\"testId\" item-color=\"red\" chevron=\"\" variant=\"outline\" opacity=\"1\" selected=\"\">Test content</ix-event-list-item>");
    }

    [Fact]
    public void ItemClickEventInvoked()
    {
        // Arrange
        var wasClicked = false;
        var cut = RenderComponent<EventListItem>(parameters => parameters
            .Add(p => p.ItemClickEvent, EventCallback.Factory.Create(this, () => wasClicked = true)));

        // Act
        cut.Instance.ItemClicked();

        // Assert
        Assert.True(wasClicked);
    }
}