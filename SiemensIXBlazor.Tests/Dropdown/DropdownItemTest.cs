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

namespace SiemensIXBlazor.Tests.Dropdown;

public class DropdownItemTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithCorrectProperties()
    {
        // Arrange
        var cut = RenderComponent<DropdownItem>(parameters => parameters
            .Add(p => p.Label, "testLabel")
            .Add(p => p.Value, "testValue"));

        // Assert
        cut.MarkupMatches("<ix-dropdown-item label=\"testLabel\" value=\"testValue\"></ix-dropdown-item>");
    }

    [Fact]
    public async Task EventCallbacksAreTriggeredCorrectly()
    {
        // Arrange
        var isOnClickEventTriggered = false;

        var cut = RenderComponent<DropdownItem>(parameters => parameters
            .Add(p => p.OnClickEvent,
                EventCallback.Factory.Create<string>(this, label => isOnClickEventTriggered = true)));

        // Act
        await cut.Instance.OnClickEvent.InvokeAsync("testLabel");

        // Assert
        Assert.True(isOnClickEventTriggered);
    }
}