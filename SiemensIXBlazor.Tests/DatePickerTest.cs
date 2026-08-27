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
using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Tests;

public class DatePickerTest : TestContextBase
{
    [Fact]
    public void UsesOfficialNavigationLabelsByDefault()
    {
        var cut = Render<DatePicker>(parameters => parameters.Add(p => p.Id, "date-picker"));

        Assert.Equal("Change calendar view to previous month", cut.Instance.AriaLabelPreviousMonthButton);
        Assert.Equal("Change calendar view to next month", cut.Instance.AriaLabelNextMonthButton);
    }

    [Fact]
    public void OfficialPropertiesRender()
    {
        var cut = Render<DatePicker>(parameters => parameters
            .Add(p => p.Id, "date-picker")
            .Add(p => p.Format, "dd/LL/yyyy")
            .Add(p => p.SingleSelection, true)
            .Add(p => p.ShowWeekNumbers, true)
            .Add(p => p.AriaLabelMonthSelection, "Month")
            .Add(p => p.AriaLabelYearSelection, "Year")
            .Add(p => p.EnableTopLayer, true));

        var element = cut.Find("ix-date-picker");
        Assert.Equal("dd/LL/yyyy", element.GetAttribute("format"));
        Assert.Contains("single-selection", cut.Markup);
        Assert.Contains("show-week-numbers", cut.Markup);
        Assert.Contains("aria-label-month-selection=\"Month\"", cut.Markup);
        Assert.Contains("aria-label-year-selection=\"Year\"", cut.Markup);
        Assert.Contains("enable-top-layer", cut.Markup);
    }

    [Fact]
    public async Task EventsDeserializeDateRangePayload()
    {
        DatePickerResponse? received = null;
        var cut = Render<DatePicker>(parameters => parameters
            .Add(p => p.Id, "date-picker")
            .Add(p => p.DateSelectEvent,
                EventCallback.Factory.Create<DatePickerResponse>(this, value => received = value)));

        await cut.Instance.DateSelect(JsonSerializer.SerializeToElement(new { from = "2026/01/01", to = "2026/01/31" }));

        Assert.NotNull(received);
        Assert.Equal("2026/01/01", received!.From);
        Assert.Equal("2026/01/31", received.To);
    }

    [Fact]
    public async Task GetCurrentDateReturnsAnEmptyResponseWhenTheElementHasNoDate()
    {
        var cut = Render<DatePicker>(parameters => parameters.Add(p => p.Id, "date-picker"));

        var response = await cut.Instance.GetCurrentDate();

        Assert.NotNull(response);
        Assert.Null(response.From);
        Assert.Null(response.To);
    }

    [Fact]
    public async Task DateAndRangeEventsForwardTypedPayloads()
    {
        var dateChanges = new List<DatePickerResponse>();
        var rangeChanges = new List<DatePickerResponse>();
        var cut = Render<DatePicker>(parameters => parameters
            .Add(p => p.Id, "date-picker")
            .Add(p => p.DateChangeEvent, EventCallback.Factory.Create<DatePickerResponse>(this, value => dateChanges.Add(value)))
            .Add(p => p.DateRangeChangeEvent, EventCallback.Factory.Create<DatePickerResponse>(this, value => rangeChanges.Add(value))));

        await cut.Instance.DateChange(JsonSerializer.SerializeToElement(new { from = "2026/08/01" }));
        await cut.Instance.DateRangeChange(JsonSerializer.SerializeToElement(new { from = "2026/08/01", to = "2026/08/07" }));
        await cut.Instance.DateSelect(JsonSerializer.SerializeToElement((string?)null));

        Assert.Single(dateChanges);
        Assert.Equal("2026/08/01", dateChanges[0].From);
        Assert.Single(rangeChanges);
        Assert.Equal("2026/08/07", rangeChanges[0].To);
    }

    [Fact]
    public async Task GetCurrentDateBeforeRenderingIsRejected()
    {
        await Assert.ThrowsAsync<InvalidOperationException>(() => new DatePicker().GetCurrentDate());
    }
}
