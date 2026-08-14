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

namespace SiemensIXBlazor.Tests.Menu;

public class MenuTest : TestContextBase
{
    [Fact]
    public void RendersCurrentPublicProperties()
    {
        var cut = RenderComponent<Components.Menu.Menu>(parameters => parameters
            .Add(p => p.Id, "menu")
            .Add(p => p.ApplicationName, "Application")
            .Add(p => p.ApplicationDescription, "Description")
            .Add(p => p.EnableToggleTheme, true)
            .Add(p => p.Expand, true)
            .Add(p => p.I18nAriaLabelMenu, "Application menu")
            .Add(p => p.I18nNavigationHint, "Use arrows")
            .Add(p => p.I18nLegal, "Legal")
            .Add(p => p.I18nSettings, "Settings")
            .Add(p => p.I18nToggleTheme, "Theme")
            .Add(p => p.I18nExpand, "Expand menu")
            .Add(p => p.I18nCollapse, "Collapse menu")
            .Add(p => p.ShowAbout, true)
            .Add(p => p.ShowSettings, true)
            .Add(p => p.StartExpanded, true)
            .Add(p => p.Pinned, true)
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddContent(0, "Items"))));

        Assert.Contains("i18n-aria-label-menu=\"Application menu\"", cut.Markup);
        Assert.Contains("i18n-navigation-hint=\"Use arrows\"", cut.Markup);
        Assert.Contains("show-about", cut.Markup);
        Assert.Contains("show-settings", cut.Markup);
        Assert.DoesNotContain("i18n-more", cut.Markup);
        Assert.DoesNotContain("enable-map-expand", cut.Markup);
    }

    [Fact]
    public async Task MenuEventsForwardTheirDetails()
    {
        var expand = false;
        var mapExpand = false;
        var appSwitch = false;
        var settings = false;
        var about = false;
        var cut = RenderComponent<Components.Menu.Menu>(parameters => parameters
            .Add(p => p.Id, "menu")
            .Add(p => p.ExpandChangedEvent, EventCallback.Factory.Create<bool>(this, value => expand = value))
            .Add(p => p.MapExpandChangedEvent, EventCallback.Factory.Create<bool>(this, value => mapExpand = value))
            .Add(p => p.OpenAppSwitchEvent, EventCallback.Factory.Create(this, () => appSwitch = true))
            .Add(p => p.OpenSettingsEvent, EventCallback.Factory.Create(this, () => settings = true))
            .Add(p => p.OpenAboutEvent, EventCallback.Factory.Create(this, () => about = true)));

        await cut.Instance.ExpandChanged(true);
        await cut.Instance.MapExpandChanged(true);
        await cut.Instance.OpenAppSwitch();
        await cut.Instance.OpenSettings();
        await cut.Instance.OpenAbout();

        Assert.True(expand);
        Assert.True(mapExpand);
        Assert.True(appSwitch);
        Assert.True(settings);
        Assert.True(about);
    }
}
