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
        var cut = Render<Components.Group>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.Expanded, true)
            .Add(p => p.ExpandOnHeaderClick, false)
            .Add(p => p.Header, "testHeader")
            .Add(p => p.Index, 1)
            .Add(p => p.Selected, true)
            .Add(p => p.SubHeader, "testSubHeader")
            .Add(p => p.SuppressHeaderSelection, false)
            .Add(p => p.ExpandedChangedEvent, EventCallback.Factory.Create(this, (bool value) => { }))
            .Add(p => p.SelectGroupEvent, EventCallback.Factory.Create(this, (bool value) => { }))
            .Add(p => p.SelectItemEvent, EventCallback.Factory.Create(this, (int value) => { })));

        // Assert
        cut.MarkupMatches(
            "<ix-group header=\"testHeader\" sub-header=\"testSubHeader\" expanded=\"true\" index=\"1\" selected=\"true\" id=\"testId\"></ix-group>");
    }

    [Fact]
    public async Task EventCallbacksTriggered()
    {
        // Arrange
        var expandedChangedEventWasCalled = false;
        var selectGroupEventWasCalled = false;
        var selectItemEventValue = 0;

        var cut = Render<Components.Group>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.ExpandedChangedEvent,
                EventCallback.Factory.Create(this, (bool value) => { expandedChangedEventWasCalled = true; }))
            .Add(p => p.SelectGroupEvent,
                EventCallback.Factory.Create(this, (bool value) => { selectGroupEventWasCalled = true; }))
            .Add(p => p.SelectItemEvent,
                EventCallback.Factory.Create(this, (int value) => { selectItemEventValue = value; })));

        // Act
        await cut.Instance.ExpandedChanged(true);
        await cut.Instance.GroupSelected(true);
        await cut.Instance.ItemSelected(1);

        // Assert
        Assert.True(expandedChangedEventWasCalled);
        Assert.True(selectGroupEventWasCalled);
        Assert.Equal(1, selectItemEventValue);
    }

    [Fact]
    public void HeaderAndFooterSlotsRenderCorrectly()
    {
        var cut = Render<Components.Group>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.HeaderContent, (RenderFragment)(builder => builder.AddContent(0, "Header")))
            .Add(p => p.FooterContent, (RenderFragment)(builder => builder.AddContent(0, "Footer"))));

        Assert.Equal("Header", cut.Find("[slot='header']").TextContent);
        Assert.Equal("Footer", cut.Find("[slot='footer']").TextContent);
    }
}
