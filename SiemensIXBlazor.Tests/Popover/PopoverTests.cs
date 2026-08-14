// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SiemensIXBlazor.Enums.Popover;

namespace SiemensIXBlazor.Tests.Popover;

public class PopoverTests : TestContextBase
{
    [Fact]
    public void Popover_RendersOfficialPropertiesAndChildContent()
    {
        var cut = Render<Components.Popover>(parameters => parameters
            .Add(p => p.Id, "popover")
            .Add(p => p.Trigger, "popover-trigger")
            .Add(p => p.Show, true)
            .Add(p => p.Placement, PopoverPlacement.Top)
            .Add(p => p.HasSpike, true)
            .Add(p => p.TriggerMode, PopoverTriggerMode.Hover)
            .Add(p => p.CloseOnClickOutside, true)
            .AddChildContent("Popover body"));

        var element = cut.Find("ix-popover");

        Assert.Equal("popover", element.GetAttribute("id"));
        Assert.Equal("popover-trigger", element.GetAttribute("trigger"));
        Assert.Equal("top", element.GetAttribute("placement"));
        Assert.Equal("hover", element.GetAttribute("trigger-mode"));
        Assert.NotNull(element.GetAttribute("show"));
        Assert.NotNull(element.GetAttribute("has-spike"));
        Assert.NotNull(element.GetAttribute("close-on-click-outside"));
        Assert.Contains("Popover body", element.InnerHtml);
    }

    [Fact]
    public async Task Popover_RaisesShowChangeAndShowChangedEvents()
    {
        var showChange = false;
        var showChanged = false;

        var cut = Render<Components.Popover>(parameters => parameters
            .Add(p => p.Id, "popover")
            .Add(p => p.ShowChangeEvent, EventCallback.Factory.Create<bool>(this, value => showChange = value))
            .Add(p => p.ShowChangedEvent, EventCallback.Factory.Create<bool>(this, value => showChanged = value)));

        await cut.Instance.ShowChange(true);
        await cut.Instance.ShowChanged(true);

        Assert.True(showChange);
        Assert.True(showChanged);
    }

    [Fact]
    public void PopoverHeader_RendersPropertiesAndNamedSlot()
    {
        var cut = Render<Components.PopoverHeader>(parameters => parameters
            .Add(p => p.Id, "popover-header")
            .Add(p => p.Icon, "info")
            .Add(p => p.IconColor, "#007993")
            .Add(p => p.HideClose, true)
            .Add(p => p.AriaLabelCloseIconButton, "Dismiss")
            .AddChildContent("Popover title")
            .Add(p => p.AdditionalItems, (RenderFragment)(builder => builder.AddContent(0, "Additional"))));

        var element = cut.Find("ix-popover-header");

        Assert.Equal("popover-header", element.GetAttribute("id"));
        Assert.Equal("info", element.GetAttribute("icon"));
        Assert.Equal("#007993", element.GetAttribute("icon-color"));
        Assert.NotNull(element.GetAttribute("hide-close"));
        Assert.Equal("Dismiss", element.GetAttribute("aria-label-close-icon-button"));
        Assert.Contains("Popover title", element.InnerHtml);
        Assert.Contains("slot=\"additional-items\"", element.InnerHtml);
        Assert.Contains("Additional", element.InnerHtml);
    }

    [Fact]
    public async Task PopoverHeader_RaisesCloseClickEvent()
    {
        MouseEventArgs? received = null;
        var cut = Render<Components.PopoverHeader>(parameters => parameters
            .Add(p => p.Id, "popover-header")
            .Add(p => p.CloseClickEvent, EventCallback.Factory.Create<MouseEventArgs>(this, value => received = value)));

        var args = new MouseEventArgs { ClientX = 12 };
        await cut.Instance.CloseClick(args);

        Assert.Same(args, received);
    }

    [Fact]
    public void PopoverImage_RendersImageProperties()
    {
        var cut = Render<Components.PopoverImage>(parameters => parameters
            .Add(p => p.Id, "popover-image")
            .Add(p => p.Image, "/images/example.png")
            .Add(p => p.ImageAlt, "Example image"));

        var element = cut.Find("ix-popover-image");

        Assert.Equal("popover-image", element.GetAttribute("id"));
        Assert.Equal("/images/example.png", element.GetAttribute("image"));
        Assert.Equal("Example image", element.GetAttribute("image-alt"));
    }

    [Fact]
    public void PopoverContent_RendersNoPaddingAndChildContent()
    {
        var cut = Render<Components.PopoverContent>(parameters => parameters
            .Add(p => p.Id, "popover-content")
            .Add(p => p.NoPadding, true)
            .AddChildContent("Popover content"));

        var element = cut.Find("ix-popover-content");

        Assert.Equal("popover-content", element.GetAttribute("id"));
        Assert.NotNull(element.GetAttribute("no-padding"));
        Assert.Contains("Popover content", element.InnerHtml);
    }

    [Fact]
    public void PopoverFooter_RendersAlignmentAndStartSlot()
    {
        var cut = Render<Components.PopoverFooter>(parameters => parameters
            .Add(p => p.Id, "popover-footer")
            .Add(p => p.Alignment, PopoverFooterAlignment.Vertical)
            .Add(p => p.StartContent, (RenderFragment)(builder => builder.AddContent(0, "Metadata")))
            .AddChildContent("Actions"));

        var element = cut.Find("ix-popover-footer");

        Assert.Equal("popover-footer", element.GetAttribute("id"));
        Assert.Equal("vertical", element.GetAttribute("alignment"));
        Assert.Contains("slot=\"start\"", element.InnerHtml);
        Assert.Contains("Metadata", element.InnerHtml);
        Assert.Contains("Actions", element.InnerHtml);
    }
}
