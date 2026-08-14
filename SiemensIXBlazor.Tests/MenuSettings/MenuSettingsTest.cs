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

namespace SiemensIXBlazor.Tests.MenuSettings;

public class MenuSettingsTest : TestContextBase
{
    [Fact]
    public void RendersCurrentPublicProperties()
    {
        var cut = Render<Components.MenuSettings.MenuSettings>(parameters => parameters
            .Add(p => p.Id, "settings")
            .Add(p => p.ActiveTabKey, "general")
            .Add(p => p.SuppressLegacyTabs, true)
            .Add(p => p.AriaLabelCloseButton, "Close")
            .Add(p => p.Label, "Settings"));

        Assert.Contains("active-tab-key=\"general\"", cut.Markup);
        Assert.Contains("slot=\"ix-menu-settings\"", cut.Markup);
        Assert.Contains("suppress-legacy-tabs", cut.Markup);
        Assert.DoesNotContain("active-tab-label", cut.Markup);
        Assert.DoesNotContain("show=", cut.Markup);
    }

    [Fact]
    public async Task CloseAndTabEventsForwardTypedDetails()
    {
        MenuCloseEvent? close = null;
        var tabKey = string.Empty;
        var cut = Render<Components.MenuSettings.MenuSettings>(parameters => parameters
            .Add(p => p.ClosedEvent, EventCallback.Factory.Create<MenuCloseEvent>(this, value => close = value))
            .Add(p => p.TabChangedEvent, EventCallback.Factory.Create<string>(this, value => tabKey = value)));
        var expected = new MenuCloseEvent { Name = "ix-menu-settings" };

        await cut.Instance.Closed(expected);
        await cut.Instance.TabChanged("general");

        Assert.Same(expected, close);
        Assert.Equal("general", tabKey);
    }
}
