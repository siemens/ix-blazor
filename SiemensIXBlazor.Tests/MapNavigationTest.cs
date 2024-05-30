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
using SiemensIXBlazor.Components.MapNavigation;

namespace SiemensIXBlazor.Tests;

public class MapNavigationTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithParametersSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<MapNavigation>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content")))
            .Add(p => p.Id, "testId")
            .Add(p => p.ApplicationName, "testApp")
            .Add(p => p.HideContextMenu, true)
            .Add(p => p.NavigationTitle, "testTitle"));

        // Assert
        // Replace the expected markup with the actual expected markup of your MapNavigation component
        cut.MarkupMatches("<ix-map-navigation id=\"testId\" application-name=\"testApp\" navigation-title=\"testTitle\" hide-context-menu=\"\">Test content</ix-map-navigation>");
    }

    [Fact]
    public async Task EventCallbacksTriggered()
    {
        // Arrange
        var contextMenuClickEventWasCalled = false;
        var navigationToggledEventWasCalled = false;

        var cut = RenderComponent<MapNavigation>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.ContextMenuClickEvent,
                EventCallback.Factory.Create(this, () => { contextMenuClickEventWasCalled = true; }))
            .Add(p => p.NavigationToggledEvent,
                EventCallback.Factory.Create<bool>(this, value => { navigationToggledEventWasCalled = true; })));

        // Act
        await cut.Instance.ContextMenuClicked();
        await cut.Instance.NavigationToggled(true);

        // Assert
        Assert.True(contextMenuClickEventWasCalled);
        Assert.True(navigationToggledEventWasCalled);
    }
}