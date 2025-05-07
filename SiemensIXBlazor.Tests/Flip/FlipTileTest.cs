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
using SiemensIXBlazor.Objects.Pane;

namespace SiemensIXBlazor.Tests.Flip;

public class FlipTileTest : TestContextBase
{
    [Fact]
    public void ComponentRendersAndSetsPropertiesCorrectly()
    {
        // Arrange
        var cut = RenderComponent<FlipTile>(parameters => parameters
            .Add(p=>p.Id,"ix-flip")
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content")))
            .Add(p => p.State, "testState")
            .Add(p => p.Height, 20)
            .Add(p => p.Width, 25));

        // Assert
        cut.MarkupMatches("<ix-flip-tile id=\"ix-flip\" state=\"testState\" height=\"20\" width=\"25\" index=\"0\" >Test content</ix-flip-tile>");
    }

    [Fact]
    public void ToggleEventWorksAsExpected()
    {
        var isToggleEventOccured = false;

        var cut = RenderComponent<FlipTile>(parameters => parameters
            .Add(p => p.Id, "ix-flip")
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content")))
            .Add(p => p.State, "testState")
            .Add(p => p.ToggleEvent, EventCallback.Factory.Create<int>(this, () => isToggleEventOccured = true))
            .Add(p => p.Height, 20)
            .Add(p => p.Width, 25));


        // Act
        //cut.Find("ix-flip").Click();

         cut.Instance.ToggleEvent.InvokeAsync();

        // Assert
        Assert.True(isToggleEventOccured);


    }
}