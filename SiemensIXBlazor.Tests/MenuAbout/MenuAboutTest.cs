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
using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Tests.MenuAbout;

public class MenuAboutTest : TestContextBase
{
    [Fact]
    public void RendersCurrentPublicProperties()
    {
        var cut = RenderComponent<Components.MenuAbout.MenuAbout>(parameters => parameters
            .Add(p => p.Id, "about")
            .Add(p => p.ActiveTabKey, "legal")
            .Add(p => p.SuppressLegacyTabs, true)
            .Add(p => p.AriaLabelCloseButton, "Close")
            .Add(p => p.Label, "About"));

        Assert.Contains("active-tab-key=\"legal\"", cut.Markup);
        Assert.Contains("slot=\"ix-menu-about\"", cut.Markup);
        Assert.Contains("suppress-legacy-tabs", cut.Markup);
        Assert.DoesNotContain("active-tab-label", cut.Markup);
        Assert.DoesNotContain("show=", cut.Markup);
    }

    [Fact]
    public async Task CloseAndTabEventsForwardTypedDetails()
    {
        MenuCloseEvent? close = null;
        var tabKey = string.Empty;
        var cut = RenderComponent<Components.MenuAbout.MenuAbout>(parameters => parameters
            .Add(p => p.ClosedEvent, EventCallback.Factory.Create<MenuCloseEvent>(this, value => close = value))
            .Add(p => p.TabChangedEvent, EventCallback.Factory.Create<string>(this, value => tabKey = value)));
        var expected = new MenuCloseEvent { Name = "ix-menu-about" };

        await cut.Instance.Closed(expected);
        await cut.Instance.TabChanged("legal");

        Assert.Same(expected, close);
        Assert.Equal("legal", tabKey);
    }
}
