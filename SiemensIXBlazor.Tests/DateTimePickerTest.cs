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

public class DateTimePickerTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithCorrectProperties()
    {
        // Arrange
        var cut = RenderComponent<DateTimePicker>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.DateFormat, "yyyy/MM/dd")
            .Add(p => p.From, DateTime.Now.ToString("yyyy/MM/dd"))
            .Add(p => p.MaxDate, "2022/12/31")
            .Add(p => p.MinDate, "2022/01/01")
            .Add(p => p.Range, true)
            .Add(p => p.ShowHour, false)
            .Add(p => p.ShowMinutes, false)
            .Add(p => p.ShowSeconds, false)
            .Add(p => p.ShowTimeReference, "")
            .Add(p => p.Time, "12:00:00")
            .Add(p => p.TimeFormat, "HH:mm:ss")
            .Add(p => p.TimeReference, "12:00:00")
            .Add(p => p.To, "2022/12/31")
            .Add(p => p.I18nDone, "Done"));

        // Assert
        cut.MarkupMatches($"<ix-datetime-picker id=\"testId\" date-format=\"yyyy/MM/dd\" from=\"{DateTime.Now:yyyy/MM/dd}\" max-date=\"2022/12/31\" min-date=\"2022/01/01\" range=\"\" show-time-reference=\"\" time=\"12:00:00\" i18n-done=\"Done\" time-format=\"HH:mm:ss\" time-reference=\"12:00:00\" to=\"2022/12/31\"></ix-datetime-picker>");
    }

    [Fact]
    public void EventCallbacksAreTriggeredCorrectly()
    {
        // Arrange
        bool isDateChangeEventTriggered = false;
        bool isDateSelectEventTriggered = false;
        bool isTimeChangeEventTriggered = false;
        bool isDoneEventTriggered = false;

        var cut = RenderComponent<DateTimePicker>(parameters => parameters
            .Add(p => p.DateChangeEvent, EventCallback.Factory.Create<string>(this, (date) => isDateChangeEventTriggered = true))
            .Add(p => p.DateSelectEvent, EventCallback.Factory.Create<DateTimePickerResponse>(this, (response) => isDateSelectEventTriggered = true))
            .Add(p => p.TimeChangeEvent, EventCallback.Factory.Create<string>(this, (time) => isTimeChangeEventTriggered = true)));

        // Act
        cut.Instance.DateChange("2022/12/31");
        cut.Instance.TimeChange("12:00:00");

        var json = JsonSerializer.Serialize(new DateTimePickerResponse { Time = "2024/01/01" });
        var parsedJson = JsonDocument.Parse(json).RootElement;
        cut.Instance.DateSelect(parsedJson);


        // Assert
        Assert.True(isDateChangeEventTriggered);
        Assert.True(isTimeChangeEventTriggered);
        Assert.True(isDoneEventTriggered);
        Assert.True(isDateSelectEventTriggered);
    }
}