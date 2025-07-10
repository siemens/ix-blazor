// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.InputFieldsAndSelections.DateTimePicker;

public partial class DateTimePicker
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
    <DateTimePicker
        DateChangeEvent=""(date) => DateChangeEventTest(date)""
        From=""2023/02/01""
        To=""2023/02/15""
        Id=""datetimepicker1""
        TimeChangeEvent=""(date) => DateChangeEventTest(date)"">
    </DateTimePicker>";
}
