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

namespace SiemensIXBlazor.Tests.Group;

public class GroupItemTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithParametersSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<GroupItem>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.Disabled, true)
            .Add(p => p.Icon, "testIcon")
            .Add(p => p.Index, 1)
            .Add(p => p.SecondaryText, "testSecondaryText")
            .Add(p => p.Selected, true)
            .Add(p => p.SuppressSelection, false)
            .Add(p => p.Text, "testText"));

        // Assert
        cut.MarkupMatches(
            "<ix-group-item text=\"testText\" focusable=\"\" icon=\"testIcon\" index=\"1\" secondary-text=\"testSecondaryText\" selected=\"\" id=\"testId\"></ix-group-item>");
    }

    [Fact]
    public void ItemClickedEventTriggered()
    {
        // Arrange
        var wasCalled = false;
        var cut = RenderComponent<GroupItem>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.SelectedChangeEvent, EventCallback.Factory.Create<string>(this, id => { wasCalled = true; })));

        // Act
        cut.Instance.ItemClicked();

        // Assert
        Assert.True(wasCalled);
    }
}