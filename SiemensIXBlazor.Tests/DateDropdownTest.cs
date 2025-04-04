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
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.DateDropdown;
using SiemensIXBlazor.Objects.DateDropdown;
using System.Text.Json;

namespace SiemensIXBlazor.Tests;

public class DateDropdownTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithoutCrashing()
    {
        // Arrange
        var cut = RenderComponent<DateDropdown>();

        // Assert
        cut.MarkupMatches(@"
                <ix-date-dropdown id='' variant='primary' custom-range-allowed='' week-start-index='0' date-range-id='custom' format='yyyy/LL/dd' i18n-custom-item='Custom...' i18n-done='Done' i18n-no-range='No range set' range=''></ix-date-dropdown>
            ");
    }

    [Fact]
    public void AllPropertiesAreSetCorrectly()
    {
        // Arrange
        var dateDropdownOptions = new DateDropdownOption[] { new() { Id = "test", Label = "Test" } };
        var dateRangeChangeEvent = new EventCallbackFactory().Create<DateDropdownResponse>(this, response =>
        {
            /* your action here */
        });

        var cut = RenderComponent<DateDropdown>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.CustomRangeAllowed, true)
            .Add(p => p.DateRangeId, "custom")
            .Add(p => p.DateRangeOptions, dateDropdownOptions)
            .Add(p => p.Format, "yyyy/LL/dd")
            .Add(p => p.From, "2022/01/01")
            .Add(p => p.I18NCustomItem, "Custom...")
            .Add(p => p.I18NDone, "Done")
            .Add(p => p.I18NNoRange, "No range set")
            .Add(p => p.MaxDate, "2022/12/31")
            .Add(p => p.MinDate, "2022/01/01")
            .Add(p => p.Range, true)
            .Add(p => p.To, "2022/12/31")
            .Add(p => p.Locale, "en")
            .Add(p => p.WeekStartIndex, 2)
            .Add(p => p.DateRangeChangeEvent, dateRangeChangeEvent)
            .Add(p => p.Disabled, true)
            .Add(p => p.Ghost, true)
            .Add(p => p.Loading, true)
            .Add(p => p.Outline, true)
            .Add(p => p.Variant, DateDropdownVariant.secondary)
            );

        // Assert
        cut.MarkupMatches(@"
                <ix-date-dropdown id='testId' outline ghost loading variant='secondary' locale='en' week-start-index='2' custom-range-allowed='' date-range-id='custom' format='yyyy/LL/dd' from='2022/01/01' i18n-custom-item='Custom...' i18n-done='Done' i18n-no-range='No range set' max-date='2022/12/31' min-date='2022/01/01' range='' to='2022/12/31' disabled></ix-date-dropdown>
            ");
    }

    [Fact]
    public void DateRangeChangeEventIsTriggeredCorrectly()
    {
        // Arrange
        var dateDropdownOptions = new DateDropdownOption[]
        {
            new() { Id = "test", Label = "Test", From = "2022/01/01", To = "2022/12/31" },
            new() { Id = "test2", Label = "Test2", From = "2023/01/01", To = "2023/12/31" }
        };
        var dateRangeChangeEventTriggered = false;
        var dateRangeChangeEvent =
            new EventCallbackFactory().Create<DateDropdownResponse>(this,
                response => { dateRangeChangeEventTriggered = true; });

        var cut = RenderComponent<DateDropdown>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.CustomRangeAllowed, true)
            .Add(p => p.DateRangeId, "test1")
            .Add(p => p.DateRangeOptions, dateDropdownOptions)
            .Add(p => p.Format, "yyyy/LL/dd")
            .Add(p => p.From, "2022/01/01")
            .Add(p => p.I18NCustomItem, "Custom...")
            .Add(p => p.I18NDone, "Done")
            .Add(p => p.I18NNoRange, "No range set")
            .Add(p => p.MaxDate, "2022/12/31")
            .Add(p => p.MinDate, "2022/01/01")
            .Add(p => p.Range, true)
            .Add(p => p.To, "2022/12/31")
            .Add(p => p.WeekStartIndex, 0)
            .Add(p => p.DateRangeChangeEvent, dateRangeChangeEvent));

        // Act
        var json = JsonSerializer.Serialize(new DateDropdownResponse { Id = "test2" });
        var parsedJson = JsonDocument.Parse(json).RootElement;
        cut.Instance.DateRangeChange(parsedJson);

        // Assert
        Assert.True(dateRangeChangeEventTriggered);
    }
}