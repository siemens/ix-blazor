// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Playground.Components.Pages.InputFieldsAndSelections.DatePicker;

public partial class DatePicker
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
    <DatePicker From=""2023/02/01""
        To=""2023/02/15""
        Id=""timepicker1""
        DateChangeEvent=""(date) => DateChangeEventTest(date)"">
    </DatePicker>";

    public string ContentForSingleSelection { get; private set; } = @"Single Selection Code";

    private readonly DatePickerResponse _singleSelection = new()
    {
        From = DateTime.Today.ToString("MM/dd/yyyy"),
        To = DateTime.Today.ToString("MM/dd/yyyy"),
    };
}
