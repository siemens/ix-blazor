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
        var cut = Render<DropdownButton>(parameters => parameters
            .Add(p => p.Disabled, true)
            .Add(p => p.Icon, "testIcon")
            .Add(p => p.CloseBehavior, DropdownButtonCloseBehavior.both)
            .Add(p => p.Label, "testLabel")
            .Add(p => p.Placement, DropdownButtonPlacement.bottom_start)
            .Add(p => p.Variant, ButtonVariant.primary)
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content"))));

        // Assert
        var element = cut.Find("ix-dropdown-button");
        Assert.StartsWith("dropdown-button-", element.GetAttribute("id"));
        Assert.Equal("testLabel", element.GetAttribute("label"));
        Assert.Equal("primary", element.GetAttribute("variant"));
        Assert.Equal("bottom-start", element.GetAttribute("placement"));
        Assert.Equal("testIcon", element.GetAttribute("icon"));
        Assert.Equal("both", element.GetAttribute("close-behavior"));
        Assert.Contains("Test content", element.InnerHtml);
    }

    [Fact]
    public void EnableTopLayerDefaultsToFalse()
    {
        // Arrange
        var cut = Render<DropdownButton>(parameters => parameters
            .Add(p => p.Label, "test"));

        // Assert
        Assert.False(cut.Instance.EnableTopLayer);
        Assert.DoesNotContain("enable-top-layer", cut.Markup);
    }

    [Fact]
    public void PlacementIsOmittedWhenUnset()
    {
        var cut = Render<DropdownButton>(parameters => parameters
            .Add(p => p.Label, "test"));

        Assert.False(cut.Find("ix-dropdown-button").HasAttribute("placement"));
    }

    [Fact]
    public void EnableTopLayerTrueRendersAttribute()
    {
        // Arrange
        var cut = Render<DropdownButton>(parameters => parameters
            .Add(p => p.Label, "test")
            .Add(p => p.EnableTopLayer, true));

        // Assert
        Assert.True(cut.Instance.EnableTopLayer);
        Assert.Contains("enable-top-layer", cut.Markup);
    }

    [Fact]
    public async Task ShowEventsAreTypedAndForwarded()
    {
        var showChange = false;
        var showChanged = false;
        var cut = Render<DropdownButton>(parameters => parameters
            .Add(p => p.ShowChangeEvent,
                EventCallback.Factory.Create<bool>(this, value => showChange = value))
            .Add(p => p.ShowChangedEvent,
                EventCallback.Factory.Create<bool>(this, value => showChanged = value)));

        await cut.Instance.ShowChange(true);
        await cut.Instance.ShowChanged(true);

        Assert.True(showChange);
        Assert.True(showChanged);
    }

    [Fact]
    public void RendersButtonLabelSlot()
    {
        var cut = Render<DropdownButton>(parameters => parameters
            .Add(p => p.ButtonLabelContent,
                (RenderFragment)(builder => builder.AddContent(0, "Additional label"))));

        Assert.Contains("slot=\"button-label\"", cut.Markup);
        Assert.Contains("Additional label", cut.Markup);
    }
}
