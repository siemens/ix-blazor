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

namespace SiemensIXBlazor.Tests.EventList;

public class EventListTest : TestContextBase
{
    [Fact]
    public void EventListPropertiesAreSetCorrectly()
    {
        // Arrange
        var cut = Render<Components.EventList>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content")))
            .Add(p => p.Animated, true)
            .Add(p => p.Chevron, true)
            .Add(p => p.Compact, false)
            .Add(p => p.ItemHeight, "L"));

        // Assert
        cut.MarkupMatches(
            "<ix-event-list animated=\"true\" chevron=\"\" item-height=\"L\">Test content</ix-event-list>");
    }

    [Fact]
    public void NumericItemHeightRendersCorrectly()
    {
        var cut = Render<Components.EventList>(parameters => parameters
            .Add(p => p.ItemHeight, 48));

        Assert.Equal("48", cut.Find("ix-event-list").GetAttribute("item-height"));
    }

    [Fact]
    public void DefaultPropertiesMatchOfficialDefaults()
    {
        var cut = Render<Components.EventList>((Action<Bunit.ComponentParameterCollectionBuilder<Components.EventList>>)(_ => { }));

        Assert.Equal("S", cut.Instance.ItemHeight);
        Assert.False(cut.Instance.Animated);
        Assert.False(cut.Instance.Compact);
        Assert.False(cut.Instance.Chevron);
    }
}
