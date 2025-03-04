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
using SiemensIXBlazor.Enums.ExpandingSearch;

namespace SiemensIXBlazor.Tests;

public class ExpandingSearchTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithCorrectProperties()
    {
        // Arrange
        var cut = RenderComponent<ExpandingSearch>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.Icon, "testIcon")
            .Add(p => p.Placeholder, "testPlaceholder")
            .Add(p => p.Value, "testValue")
            .Add(p => p.FullWidth, true)
            .Add(p => p.Outline, true)
            .Add(p => p.Ghost, false)
            .Add(p => p.Variant, ExpandingSearchVariant.secondary)
            );

        // Assert
        cut.MarkupMatches(
            "<ix-expanding-search placeholder=\"testPlaceholder\" icon=\"testIcon\" id=\"testId\" value=\"testValue\" full-width outline variant='secondary'></ix-expanding-search>");
    }

    [Fact]
    public void ValueChangedEventInvokedOnValueChange()
    {
        // Arrange
        var valueChangedEventInvoked = false;
        var cut = RenderComponent<ExpandingSearch>(parameters => parameters
            .Add(p => p.ValueChangedEvent,
                EventCallback.Factory.Create<string>(this, _ => valueChangedEventInvoked = true)));

        // Act
        cut.Instance.ValueChanged(string.Empty);

        // Assert
        Assert.True(valueChangedEventInvoked);
    }
}