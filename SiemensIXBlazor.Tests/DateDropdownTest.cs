// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Text.Json;
using Bunit;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.Button;
using SiemensIXBlazor.Objects.DateDropdown;

namespace SiemensIXBlazor.Tests;

public class DateDropdownTest : TestContextBase
{
    [Fact]
    public void OfficialPropertiesRender()
    {
        var cut = RenderComponent<DateDropdown>(parameters => parameters
            .Add(p => p.Id, "date-dropdown")
            .Add(p => p.DateRangeId, "last-week")
            .Add(p => p.Format, "yyyy/LL/dd")
            .Add(p => p.From, "2026/01/01")
            .Add(p => p.To, "2026/01/31")
            .Add(p => p.SingleSelection, true)
            .Add(p => p.ShowWeekNumbers, true)
            .Add(p => p.Variant, ButtonVariant.secondary)
            .Add(p => p.I18nDone, "Apply"));

        var element = cut.Find("ix-date-dropdown");
        Assert.Equal("last-week", element.GetAttribute("date-range-id"));
        Assert.Equal("yyyy/LL/dd", element.GetAttribute("format"));
        Assert.Equal("2026/01/01", element.GetAttribute("from"));
        Assert.Equal("2026/01/31", element.GetAttribute("to"));
        Assert.Equal("secondary", element.GetAttribute("variant"));
        Assert.Contains("single-selection", cut.Markup);
        Assert.Contains("show-week-numbers", cut.Markup);
        Assert.DoesNotContain(" range=", cut.Markup);
    }

    [Fact]
    public async Task DateRangeChangeEventDeserializesOfficialPayload()
    {
        DateDropdownResponse? received = null;
        var cut = RenderComponent<DateDropdown>(parameters => parameters
            .Add(p => p.Id, "date-dropdown")
            .Add(p => p.DateRangeChangeEvent,
                EventCallback.Factory.Create<DateDropdownResponse>(this, value => received = value)));

        await cut.Instance.DateRangeChange(JsonSerializer.SerializeToElement(new
        {
            id = "custom",
            from = "2026/01/01",
            to = "2026/01/31"
        }));

        Assert.NotNull(received);
        Assert.Equal("custom", received!.Id);
        Assert.Equal("2026/01/31", received.To);
    }
}
