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
using SiemensIXBlazor.Enums.Flip;

namespace SiemensIXBlazor.Tests.Flip;

public class FlipTileTest : TestContextBase
{
    [Fact]
    public void ComponentRendersAndSetsPropertiesCorrectly()
    {
        // Arrange
        var cut = RenderComponent<FlipTile>(parameters => parameters
            .Add(p => p.Id, "ix-flip")
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content")))
            .Add(p => p.Variant, FlipTileVariant.primary)
            .Add(p => p.Height, 20)
            .Add(p => p.Width, 25)
            .Add(p => p.Index, 5));

        // Assert
        cut.MarkupMatches("<ix-flip-tile id=\"ix-flip\" variant=\"primary\" height=\"20\" width=\"25\" index=\"5\" >Test content</ix-flip-tile>");
    }

    [Fact]
    public void ComponentRendersWithAllPropertiesSet()
    {
        // Arrange
        var cut = RenderComponent<FlipTile>(parameters => parameters
            .Add(p => p.Id, "ix-flip-complete")
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Full test")))
            .Add(p => p.Variant, FlipTileVariant.warning)
            .Add(p => p.AriaLabelEyeIconButton, "Toggle view")
            .Add(p => p.Height, 30)
            .Add(p => p.Width, 35)
            .Add(p => p.Index, 3));

        // Assert
        cut.MarkupMatches("<ix-flip-tile id=\"ix-flip-complete\" variant=\"warning\" aria-label-eye-icon-button=\"Toggle view\" height=\"30\" width=\"35\" index=\"3\" >Full test</ix-flip-tile>");
    }

    [Fact]
    public void ToggleEventWorksAsExpected()
    {
        var isToggleEventOccured = false;

        var cut = RenderComponent<FlipTile>(parameters => parameters
            .Add(p => p.Id, "ix-flip")
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content")))
            .Add(p => p.Variant, FlipTileVariant.alarm)
            .Add(p => p.ToggleEvent, EventCallback.Factory.Create<int>(this, () => isToggleEventOccured = true))
            .Add(p => p.Height, 20)
            .Add(p => p.Width, 25));


        // Act
        //cut.Find("ix-flip").Click();

        cut.Instance.ToggleEvent.InvokeAsync();

        // Assert
        Assert.True(isToggleEventOccured);
    }

    [Fact]
    public void DefaultVariantIsFilled()
    {
        // Arrange & Act
        var cut = RenderComponent<FlipTile>(parameters => parameters
            .Add(p => p.Id, "ix-flip")
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content"))));

        // Assert
        Assert.Equal(FlipTileVariant.filled, cut.Instance.Variant);
        cut.MarkupMatches("<ix-flip-tile id=\"ix-flip\" variant=\"filled\" height=\"15.125\" width=\"16\" index=\"0\" >Test content</ix-flip-tile>");
    }

    [Fact]
    public void DefaultHeightAndWidthAreSet()
    {
        // Arrange & Act
        var cut = RenderComponent<FlipTile>(parameters => parameters
            .Add(p => p.Id, "ix-flip"));

        // Assert
        Assert.Equal(15.125, cut.Instance.Height);
        Assert.Equal(16, cut.Instance.Width);
    }

    [Fact]
    public void DefaultIndexIsZero()
    {
        // Arrange & Act
        var cut = RenderComponent<FlipTile>(parameters => parameters
            .Add(p => p.Id, "ix-flip"));

        // Assert
        Assert.Equal(0, cut.Instance.Index);
    }

    [Fact]
    public void AriaLabelEyeIconButtonRendersWhenSet()
    {
        // Arrange & Act
        var cut = RenderComponent<FlipTile>(parameters => parameters
            .Add(p => p.Id, "ix-flip")
            .Add(p => p.AriaLabelEyeIconButton, "Show details"));

        // Assert
        var element = cut.Find("ix-flip-tile");
        Assert.Equal("Show details", element.GetAttribute("aria-label-eye-icon-button"));
    }

    [Fact]
    public void AriaLabelEyeIconButtonIsOptional()
    {
        // Arrange & Act
        var cut = RenderComponent<FlipTile>(parameters => parameters
            .Add(p => p.Id, "ix-flip"));

        // Assert
        Assert.Null(cut.Instance.AriaLabelEyeIconButton);
    }

    [Fact]
    public void CustomHeightAndWidthRenderCorrectly()
    {
        // Arrange & Act
        var cut = RenderComponent<FlipTile>(parameters => parameters
            .Add(p => p.Id, "ix-flip")
            .Add(p => p.Height, 50)
            .Add(p => p.Width, 60));

        // Assert
        var element = cut.Find("ix-flip-tile");
        Assert.Equal("50", element.GetAttribute("height"));
        Assert.Equal("60", element.GetAttribute("width"));
    }

    [Fact]
    public void IndexPropertyRenders()
    {
        // Arrange & Act
        var cut = RenderComponent<FlipTile>(parameters => parameters
            .Add(p => p.Id, "ix-flip")
            .Add(p => p.Index, 7));

        // Assert
        Assert.Equal(7, cut.Instance.Index);
        var element = cut.Find("ix-flip-tile");
        Assert.Equal("7", element.GetAttribute("index"));
    }

    [Theory]
    [InlineData(FlipTileVariant.alarm, "alarm")]
    [InlineData(FlipTileVariant.filled, "filled")]
    [InlineData(FlipTileVariant.info, "info")]
    [InlineData(FlipTileVariant.outline, "outline")]
    [InlineData(FlipTileVariant.primary, "primary")]
    [InlineData(FlipTileVariant.warning, "warning")]
    public void AllVariantsRenderCorrectly(FlipTileVariant variant, string expectedVariant)
    {
        // Arrange & Act
        var cut = RenderComponent<FlipTile>(parameters => parameters
            .Add(p => p.Id, "ix-flip")
            .Add(p => p.Variant, variant)
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content"))));

        // Assert
        Assert.Equal(variant, cut.Instance.Variant);
        cut.MarkupMatches($"<ix-flip-tile id=\"ix-flip\" variant=\"{expectedVariant}\" height=\"15.125\" width=\"16\" index=\"0\" >Test content</ix-flip-tile>");
    }

    [Fact]
    public void ChildContentRendersCorrectly()
    {
        // Arrange
        var complexContent = "<div class=\"test\"><span>Complex</span> content</div>";

        // Act
        var cut = RenderComponent<FlipTile>(parameters => parameters
            .Add(p => p.Id, "ix-flip")
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, complexContent))));

        // Assert
        var element = cut.Find("ix-flip-tile");
        Assert.Contains("Complex", element.InnerHtml);
        Assert.Contains("content", element.InnerHtml);
    }

    [Fact]
    public void ToggleEventReceivesCorrectIndex()
    {
        // Arrange
        int receivedIndex = -1;

        var cut = RenderComponent<FlipTile>(parameters => parameters
            .Add(p => p.Id, "ix-flip")
            .Add(p => p.Index, 42)
            .Add(p => p.ToggleEvent, EventCallback.Factory.Create<int>(this, (index) => receivedIndex = index)));

        // Act
        cut.Instance.ToggleClicked(42);

        // Assert
        Assert.Equal(42, receivedIndex);
    }

    [Fact]
    public void IdIsRequiredAndRendersCorrectly()
    {
        // Arrange & Act
        var cut = RenderComponent<FlipTile>(parameters => parameters
            .Add(p => p.Id, "custom-flip-id"));

        // Assert
        var element = cut.Find("ix-flip-tile");
        Assert.Equal("custom-flip-id", element.GetAttribute("id"));
    }

    [Fact]
    public void ComponentSupportsUserAttributes()
    {
        // Arrange & Act
        var cut = RenderComponent<FlipTile>(parameters => parameters
            .Add(p => p.Id, "ix-flip")
            .AddUnmatched("data-testid", "flip-test")
            .AddUnmatched("custom-attr", "custom-value"));

        // Assert
        var element = cut.Find("ix-flip-tile");
        Assert.Equal("flip-test", element.GetAttribute("data-testid"));
        Assert.Equal("custom-value", element.GetAttribute("custom-attr"));
    }
}
