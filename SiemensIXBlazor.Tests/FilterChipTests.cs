// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Bunit;
using SiemensIXBlazor.Components;

namespace SiemensIXBlazor.Tests;

public class FilterChipTests : TestContextBase
{
    [Fact]
    public void RendersPublicPropertiesAndChildContent()
    {
        var cut = Render<FilterChip>(parameters => parameters
            .Add(component => component.Id, "status-filter")
            .Add(component => component.AriaLabelCloseIconButton, "Remove status filter")
            .Add(component => component.Disabled, true)
            .Add(component => component.HideCloseButton, true)
            .Add(component => component.Readonly, true)
            .Add(component => component.ChildContent, builder => builder.AddContent(0, "Status: Active")));

        cut.MarkupMatches("""
            <ix-filter-chip id="status-filter"
                            aria-label-close-icon-button="Remove status filter"
                            disabled="true"
                            hide-close-button="true"
                            readonly="true">
                Status: Active
            </ix-filter-chip>
            """);
    }

    [Fact]
    public void OmitsFalseBooleanProperties()
    {
        var cut = Render<FilterChip>(parameters => parameters.Add(component => component.Id, "default-filter"));

        var element = cut.Find("ix-filter-chip");
        Assert.False(element.HasAttribute("disabled"));
        Assert.False(element.HasAttribute("hide-close-button"));
        Assert.False(element.HasAttribute("readonly"));
        Assert.False(element.HasAttribute("aria-label-close-icon-button"));
    }

    [Fact]
    public async Task CloseClickInvokesCallback()
    {
        var closeClicked = false;
        var cut = Render<FilterChip>(parameters => parameters
            .Add(component => component.Id, "clickable-filter")
            .Add(component => component.CloseClickEvent,
                EventCallback.Factory.Create(this, () => closeClicked = true)));

        await cut.Instance.CloseClick();

        Assert.True(closeClicked);
    }
}
