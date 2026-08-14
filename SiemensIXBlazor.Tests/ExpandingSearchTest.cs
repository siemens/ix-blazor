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
using SiemensIXBlazor.Enums.Button;

namespace SiemensIXBlazor.Tests;

public class ExpandingSearchTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithCorrectProperties()
    {
        // Arrange
        var cut = Render<ExpandingSearch>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.Icon, "testIcon")
            .Add(p => p.Placeholder, "testPlaceholder")
            .Add(p => p.Value, "testValue")
            .Add(p => p.FullWidth, true)
            .Add(p => p.AriaLabelClearIconButton, "Clear")
            .Add(p => p.AriaLabelSearchIconButton, "Search")
            .Add(p => p.AriaLabelSearchInput, "Search field")
            .Add(p => p.Variant, ButtonVariant.subtle_secondary)
            );

        // Assert
        cut.MarkupMatches(
            "<ix-expanding-search placeholder=\"testPlaceholder\" icon=\"testIcon\" id=\"testId\" value=\"testValue\" aria-label-clear-icon-button=\"Clear\" aria-label-search-icon-button=\"Search\" aria-label-search-input=\"Search field\" full-width variant=\"subtle-secondary\"></ix-expanding-search>");
    }

    [Fact]
    public void ValueChangedEventInvokedOnValueChange()
    {
        // Arrange
        var valueChangedEventInvoked = false;
        var cut = Render<ExpandingSearch>(parameters => parameters
            .Add(p => p.ValueChangedEvent,
                EventCallback.Factory.Create<string>(this, _ => valueChangedEventInvoked = true)));

        // Act
        cut.Instance.ValueChanged(string.Empty);

        // Assert
        Assert.True(valueChangedEventInvoked);
    }
}
