// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using SiemensIXBlazor.Objects.DateDropdown;

namespace SiemensIXBlazor.Playground.Components.Pages.InputFieldsAndSelections.DateDropdown
{
    public partial class DateDropdown
    {
        private int activeTab = 0;
        private int activeTabForDateDefined = 0;

        public string ContentForBasic { get; private set; } = @"
        <SiemensIXBlazor.Components.DateDropdown Id=""datedropdown-basic"" 
            DateRangeChangeEvent=""Callback"">
        </SiemensIXBlazor.Components.DateDropdown>";

        public string ContentForDefinedDate { get; private set; } = @"
    private readonly DateDropdownOption[] _dateRangeOptions =
    {
        new()
        {
            Id = ""last-7"",
            Label = ""Last 7 days"",
            From = DateTime.Today.AddDays(-7).ToString(""MM/dd/yyyy""),
            To = DateTime.Today.ToString(""MM/dd/yyyy"")
        },
        new()
        {
            Id = ""today"",
            Label = ""Today"",
            From = DateTime.Today.ToString(""MM/dd/yyyy""),
            To = DateTime.Today.ToString(""MM/dd/yyyy"")
        }
    };

    private void Callback(DateDropdownResponse selectedDateDropdown)
    {
        Console.WriteLine(selectedDateDropdown.Id);
    }

    <SiemensIXBlazor.Components.DateDropdown Id=""datedropdown-defined-date"" DateRangeId=""last-7"" Format=""MM/dd/yyyy""
            DateRangeOptions=""_dateRangeOptions"" DateRangeChangeEvent=""Callback"">
     </SiemensIXBlazor.Components.DateDropdown>
    ";

        private readonly DateDropdownOption[] _dateRangeOptions =
        {
            new()
            {
                Id = "last-7",
                Label = "Last 7 days",
                From = DateTime.Today.AddDays(-7).ToString("MM/dd/yyyy"),
                To = DateTime.Today.ToString("MM/dd/yyyy")
            },
            new() 
            {
                Id = "today",
                Label = "Today",
                From = DateTime.Today.ToString("MM/dd/yyyy"),
                To = DateTime.Today.ToString("MM/dd/yyyy")
            }
        };
        private void Callback(DateDropdownResponse selectedDateDropdown)
        {
            Console.WriteLine(selectedDateDropdown.Id);
        }


    }
}
