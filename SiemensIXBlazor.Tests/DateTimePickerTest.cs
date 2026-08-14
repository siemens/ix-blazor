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
using SiemensIXBlazor.Enums.TimePicker;
using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Tests;

public class DateTimePickerTest : TestContextBase
{
    [Fact]
    public void OfficialPropertiesRender()
    {
        var cut = Render<DateTimePicker>(parameters => parameters
            .Add(p => p.Id, "datetime-picker")
            .Add(p => p.MinTime, "08:00")
            .Add(p => p.MaxTime, "18:00")
            .Add(p => p.ShowTimeReference, true)
            .Add(p => p.TimeReference, TimeReference.PM)
            .Add(p => p.ShowWeekNumbers, true));

        Assert.Contains("min-time=\"08:00\"", cut.Markup);
        Assert.Contains("max-time=\"18:00\"", cut.Markup);
        Assert.Contains("show-time-reference", cut.Markup);
        Assert.Contains("time-reference=\"PM\"", cut.Markup);
        Assert.Contains("show-week-numbers", cut.Markup);
    }

    [Fact]
    public async Task DateChangePreservesOfficialStringAndRangeForms()
    {
        var received = new List<DateTimeDateChangeEvent>();
        var cut = Render<DateTimePicker>(parameters => parameters
            .Add(p => p.Id, "datetime-picker")
            .Add(p => p.DateChangeEvent,
                EventCallback.Factory.Create<DateTimeDateChangeEvent>(this, value => received.Add(value))));

        await cut.Instance.DateChange(JsonSerializer.SerializeToElement("2026/01/01 12:00:00"));
        await cut.Instance.DateChange(JsonSerializer.SerializeToElement(new { from = "2026/01/01", to = "2026/01/31" }));

        Assert.Equal("2026/01/01 12:00:00", received[0].Value);
        Assert.Equal("2026/01/01", received[1].From);
        Assert.Equal("2026/01/31", received[1].To);
    }
}
