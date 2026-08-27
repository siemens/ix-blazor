// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using Bunit;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.Badge;

namespace SiemensIXBlazor.Tests;

public class BadgeTests : TestContextBase
{
    [Fact]
    public void BadgeRendersOfficialDefaultsAndTypedAttributes()
    {
        var cut = Render<Badge>(parameters =>
        {
            parameters.Add(p => p.Type, BadgeType.StatusIcon);
            parameters.Add(p => p.Variant, BadgeVariant.Error);
            parameters.Add(p => p.Position, BadgePosition.BottomAfter);
            parameters.Add(p => p.Label, "3");
            parameters.Add(p => p.Icon, "warning-filled");
            parameters.Add(p => p.AriaLabelIcon, "Warning");
            parameters.Add(p => p.Border, true);
            parameters.Add(p => p.EnableAnimation, true);
        });

        var element = cut.Find("ix-badge");
        Assert.Equal("status-icon", element.GetAttribute("type"));
        Assert.Equal("error", element.GetAttribute("variant"));
        Assert.Equal("bottom-after", element.GetAttribute("position"));
        Assert.Equal("Warning", element.GetAttribute("aria-label-icon"));
        Assert.Equal("true", element.GetAttribute("border"));
        Assert.Equal("true", element.GetAttribute("enable-animation"));
    }

    [Fact]
    public void BadgeOmitFalseTooltipAndBooleanAttributes()
    {
        var cut = Render<Badge>(parameters => parameters.Add(p => p.TooltipText, false));

        var element = cut.Find("ix-badge");
        Assert.Null(element.GetAttribute("tooltip-text"));
        Assert.Null(element.GetAttribute("align-left"));
        Assert.Null(element.GetAttribute("outline"));
    }

    [Fact]
    public void BadgeSupportsStringAndPresenceOnlyTooltip()
    {
        var stringCut = Render<Badge>(parameters => parameters.Add(p => p.TooltipText, "Custom tooltip"));
        Assert.Equal("Custom tooltip", stringCut.Find("ix-badge").GetAttribute("tooltip-text"));

        var presenceCut = Render<Badge>(parameters => parameters.Add(p => p.TooltipText, true));
        Assert.Equal(string.Empty, presenceCut.Find("ix-badge").GetAttribute("tooltip-text"));
    }

    [Theory]
    [InlineData(BadgeType.Counter, "counter")]
    [InlineData(BadgeType.Dot, "dot")]
    [InlineData(BadgeType.Label, "label")]
    [InlineData(BadgeType.StatusIcon, "status-icon")]
    public void SupportsEveryOfficialBadgeType(BadgeType type, string expected)
    {
        var cut = Render<Badge>(parameters => parameters.Add(p => p.Type, type));

        Assert.Equal(expected, cut.Find("ix-badge").GetAttribute("type"));
    }

    [Theory]
    [InlineData(BadgePosition.TopAfter, "top-after")]
    [InlineData(BadgePosition.BottomAfter, "bottom-after")]
    public void SupportsEveryOfficialBadgePosition(BadgePosition position, string expected)
    {
        var cut = Render<Badge>(parameters => parameters.Add(p => p.Position, position));

        Assert.Equal(expected, cut.Find("ix-badge").GetAttribute("position"));
    }

    [Theory]
    [InlineData(BadgeVariant.Alarm, "alarm")]
    [InlineData(BadgeVariant.Critical, "critical")]
    [InlineData(BadgeVariant.Custom, "custom")]
    [InlineData(BadgeVariant.Error, "error")]
    [InlineData(BadgeVariant.Info, "info")]
    [InlineData(BadgeVariant.Neutral, "neutral")]
    [InlineData(BadgeVariant.Primary, "primary")]
    [InlineData(BadgeVariant.Success, "success")]
    [InlineData(BadgeVariant.Warning, "warning")]
    public void SupportsEveryOfficialBadgeVariant(BadgeVariant variant, string expected)
    {
        var cut = Render<Badge>(parameters => parameters.Add(p => p.Variant, variant));

        Assert.Equal(expected, cut.Find("ix-badge").GetAttribute("variant"));
    }
}
