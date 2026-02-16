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
using SiemensIXBlazor.Enums.DropdownButton;

namespace SiemensIXBlazor.Tests;

public class DropdownButtonTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithCorrectProperties()
    {
        // Arrange
        var cut = RenderComponent<DropdownButton>(parameters => parameters
            .Add(p => p.Disabled, true)
            .Add(p => p.Icon, "testIcon")
            .Add(p => p.CloseBehavior, DropdownButtonCloseBehavior.both)
            .Add(p => p.Label, "testLabel")
            .Add(p => p.Placement, DropdownButtonPlacement.bottom_start)
            .Add(p => p.Variant, ButtonVariant.primary)
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content"))));

        // Assert
        cut.MarkupMatches(
            "<ix-dropdown-button label=\"testLabel\" variant=\"primary\" placement=\"bottom-start\" icon=\"testIcon\" closeBehavior=\"both\" disabled=\"\">Test content</ix-dropdown-button>");
    }

    [Fact]
    public void EnableTopLayerDefaultsToFalse()
    {
        // Arrange
        var cut = RenderComponent<DropdownButton>(parameters => parameters
            .Add(p => p.Label, "test"));

        // Assert
        Assert.False(cut.Instance.EnableTopLayer);
        Assert.DoesNotContain("enable-top-layer", cut.Markup);
    }

    [Fact]
    public void EnableTopLayerTrueRendersAttribute()
    {
        // Arrange
        var cut = RenderComponent<DropdownButton>(parameters => parameters
            .Add(p => p.Label, "test")
            .Add(p => p.EnableTopLayer, true));

        // Assert
        Assert.True(cut.Instance.EnableTopLayer);
        Assert.Contains("enable-top-layer", cut.Markup);
    }
}
