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

namespace SiemensIXBlazor.Tests.Group;

public class GroupTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithParametersSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<Components.Group>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.Collapsed, true)
            .Add(p => p.ExpandOnHeaderClick, false)
            .Add(p => p.Header, "testHeader")
            .Add(p => p.Index, 1)
            .Add(p => p.Selected, true)
            .Add(p => p.SubHeader, "testSubHeader")
            .Add(p => p.SuppressHeaderSelection, false)
            .Add(p => p.CollapsedChangedEvent, EventCallback.Factory.Create(this, (bool value) => { }))
            .Add(p => p.SelectGroupEvent, EventCallback.Factory.Create(this, (bool value) => { }))
            .Add(p => p.SelectItemEvent, EventCallback.Factory.Create(this, (int value) => { })));

        // Assert
        cut.MarkupMatches(
            "<ix-group header=\"testHeader\" sub-header=\"testSubHeader\" collapsed=\"\" index=\"1\" selected=\"\" id=\"testId\"></ix-group>");
    }

    [Fact]
    public void EventCallbacksTriggered()
    {
        // Arrange
        var collapsedChangedEventWasCalled = false;
        var selectGroupEventWasCalled = false;
        var selectItemEventValue = 0;

        var cut = RenderComponent<Components.Group>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.CollapsedChangedEvent,
                EventCallback.Factory.Create(this, (bool value) => { collapsedChangedEventWasCalled = true; }))
            .Add(p => p.SelectGroupEvent,
                EventCallback.Factory.Create(this, (bool value) => { selectGroupEventWasCalled = true; }))
            .Add(p => p.SelectItemEvent,
                EventCallback.Factory.Create(this, (int value) => { selectItemEventValue = value; })));

        // Act
        cut.Instance.CollapsedChanged(true);
        cut.Instance.GroupSelected(true);
        cut.Instance.ItemSelected(1);

        // Assert
        Assert.True(collapsedChangedEventWasCalled);
        Assert.True(selectGroupEventWasCalled);
        Assert.Equal(1, selectItemEventValue);
    }
}