// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.ButtonsAndActions.SplitButton;

public partial class SplitButton
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
        <SplitButton Id=""split-button-1""
                 Label=""Split Button""
                 SplitIcon=""chevron-down-small""
                ButtonClickedEvent=""SplitButtonClicked"">
            <DropdownItem Label=""Item 1""></DropdownItem>
            <DropdownItem Label=""Item 2""></DropdownItem>
        </SplitButton>";
    public string ContentForIconOnly { get; private set; } = @"
        <SplitButton Id=""split-button-1""
                 SplitIcon=""chevron-down-small""
                ButtonClickedEvent=""SplitButtonClicked"">
            <DropdownItem Icon=""Cut""></DropdownItem>
            <DropdownItem Icon=""Bulb""></DropdownItem>
        </SplitButton>";
}
