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
using Microsoft.AspNetCore.Components.Web;

namespace SiemensIXBlazor.Tests.MenuAbout;

public class MenuAboutNewsTest : TestContextBase
{
    [Fact]
    public void RendersCurrentPublicProperties()
    {
        var cut = Render<Components.MenuAbout.MenuAboutNews>(parameters => parameters
            .Add(p => p.Id, "news")
            .Add(p => p.Label, "Release notes")
            .Add(p => p.AboutItemLabel, "News")
            .Add(p => p.ActiveAboutTabKey, "news")
            .Add(p => p.I18nShowMore, "Read more")
            .Add(p => p.Show, true));

        Assert.Contains("active-about-tab-key=\"news\"", cut.Markup);
        Assert.Contains("show", cut.Markup);
        Assert.DoesNotContain("expanded", cut.Markup);
    }

    [Fact]
    public async Task NewsEventsForwardDetails()
    {
        var closed = false;
        MouseEventArgs? mouseEvent = null;
        var cut = Render<Components.MenuAbout.MenuAboutNews>(parameters => parameters
            .Add(p => p.ClosePopoverEvent, EventCallback.Factory.Create(this, () => closed = true))
            .Add(p => p.ShowMoreEvent, EventCallback.Factory.Create<MouseEventArgs>(this, value => mouseEvent = value)));
        var expected = new MouseEventArgs();

        await cut.Instance.ClosePopover();
        await cut.Instance.ShowMore(expected);

        Assert.True(closed);
        Assert.Same(expected, mouseEvent);
    }
}
