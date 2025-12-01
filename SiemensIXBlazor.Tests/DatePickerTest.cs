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
using SiemensIXBlazor.Enums.DatePicker;
using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Tests;

public class DatePickerTest : TestContextBase
{
    [Fact]
    public void ComponentRendersAndPropertiesAreSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<DatePicker>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.Corners, DatePickerCorners.Rounded)
            .Add(p => p.Format, "yyyy/MM/dd")
            .Add(p => p.From, "2022/01/01")
            .Add(p => p.MaxDate, "2022/12/31")
            .Add(p => p.MinDate, "2022/01/01")
            .Add(p => p.SingleSelection, true)
            .Add(p => p.I18nDone, "Done")
            .Add(p => p.Locale, "en-US")
            .Add(p => p.WeekStartIndex, 0)
            .Add(p => p.To, "2022/12/31"));

        // Assert
        cut.MarkupMatches($@"
                <ix-date-picker id=""testId"" from=""2022/01/01"" to=""2022/12/31"" corners=""rounded"" format=""yyyy/MM/dd"" max-date=""2022/12/31"" min-date=""2022/01/01"" single-selection="""" locale=""en-US"" i18n-done=""Done"" week-start-index=""0""></ix-date-picker>");
    }

    [Fact]
    public void EventCallbacksAreTriggered()
    {
        // Arrange
        var dateRangeChangeInvoked = false;
        var dateChangeInvoked = false;
        var dateSelectInvoked = false;

        var dateRangeChangeEvent =
            new EventCallbackFactory().Create<DatePickerResponse>(this,
                response => { dateRangeChangeInvoked = true; });

        var dateChangeEvent =
            new EventCallbackFactory().Create<DatePickerResponse>(this,
                response => { dateChangeInvoked = true; });

        var dateSelectEvent =
            new EventCallbackFactory().Create<DatePickerResponse>(this,
                response => { dateSelectInvoked = true; });

        var cut = RenderComponent<DatePicker>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.DateRangeChangeEvent!, dateRangeChangeEvent)
            .Add(p => p.DateChangeEvent!, dateChangeEvent)
            .Add(p => p.DateSelectEvent, dateSelectEvent));

        // Act
        var json = JsonSerializer.Serialize(new DatePickerResponse { From = "2024/01/01", To = "2024/12/31\"" });
        var parsedJson = JsonDocument.Parse(json).RootElement;
        cut.Instance.DateRangeChange(parsedJson);
        cut.Instance.DateChange(parsedJson);
        cut.Instance.DateSelect(parsedJson);

        // Assert
        Assert.True(dateRangeChangeInvoked);
        Assert.True(dateChangeInvoked);
        Assert.True(dateSelectInvoked);
    }
}