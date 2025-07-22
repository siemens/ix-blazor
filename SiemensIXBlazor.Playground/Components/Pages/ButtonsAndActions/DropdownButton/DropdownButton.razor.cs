// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.ButtonsAndActions.DropdownButton;

public partial class DropdownButton
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
        <DropdownButton Label=""Dropdown"" Variant=""Primary"" Icon=""Checkboxes"">
            <DropdownItem Label=""Item 1"" Value=""1""></DropdownItem>
            <DropdownItem Label=""Item 2"" Value=""2""></DropdownItem>
        </DropdownButton>

        <DropdownButton Label=""Dropdown"" Outline Variant=""Primary"" Icon=""Checkboxes"">
            <DropdownItem Label=""Item 1"" Value=""1""></DropdownItem>
            <DropdownItem Label=""Item 2"" Value=""2""></DropdownItem>
        </DropdownButton>

        <DropdownButton Label=""Dropdown"" Ghost Variant=""Primary"" Icon=""Checkboxes"">
            <DropdownItem Label=""Item 1"" Value=""1""></DropdownItem>
            <DropdownItem Label=""Item 2"" Value=""2""></DropdownItem>
        </DropdownButton>

        <DropdownButton Label=""Dropdown"" Disabled Variant=""Primary"" Icon=""Checkboxes"">
            <DropdownItem Label=""Item 1"" Value=""1""></DropdownItem>
            <DropdownItem Label=""Item 2"" Value=""2""></DropdownItem>
        </DropdownButton>";
    
    public string ContentForIcon { get; private set; } = @"
        <DropdownButton Label="""" Variant=""Primary"" Icon=""Checkboxes"">
            <DropdownItem Label=""Item 1"" Value=""1""></DropdownItem>
            <DropdownItem Label=""Item 2"" Value=""2""></DropdownItem>
        </DropdownButton>

        <DropdownButton Label="""" Outline Variant=""Primary"" Icon=""Checkboxes"">
            <DropdownItem Label=""Item 1"" Value=""1""></DropdownItem>
            <DropdownItem Label=""Item 2"" Value=""2""></DropdownItem>
        </DropdownButton>

        <DropdownButton Label="""" Ghost Variant=""Primary"" Icon=""Checkboxes"">
            <DropdownItem Label=""Item 1"" Value=""1""></DropdownItem>
            <DropdownItem Label=""Item 2"" Value=""2""></DropdownItem>
        </DropdownButton>

        <DropdownButton Label="""" Disabled Variant=""Primary"" Icon=""Checkboxes"">
            <DropdownItem Label=""Item 1"" Value=""1""></DropdownItem>
            <DropdownItem Label=""Item 2"" Value=""2""></DropdownItem>
        </DropdownButton>";
}
