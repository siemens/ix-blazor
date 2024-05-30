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

namespace SiemensIXBlazor.Tests;

public class EmptyStateTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithCorrectProperties()
    {
        // Arrange
        var cut = RenderComponent<EmptyState>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.Action, "testAction")
            .Add(p => p.Header, "testHeader")
            .Add(p => p.Icon, "testIcon")
            .Add(p => p.Layout, "compact")
            .Add(p => p.SubHeader, "testSubHeader"));

        // Assert
        cut.MarkupMatches(
            "<ix-empty-state header=\"testHeader\" sub-header=\"testSubHeader\" icon=\"testIcon\" action=\"testAction\" id=\"testId\"></ix-empty-state>");
    }

    [Fact]
    public void ActionClickedEventInvoked()
    {
        // Arrange
        var eventCalled = false;
        var cut = RenderComponent<EmptyState>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.ActionClickedEvent, EventCallback.Factory.Create(this, () => eventCalled = true)));

        // Act
        cut.Instance.ActionClicked();

        // Assert
        Assert.True(eventCalled);
    }
}