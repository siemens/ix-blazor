// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.ApplicationFrame.ApplicationHeader;

public partial class ApplicationHeader
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
        <SiemensIXBlazor.Components.ApplicationHeader Id=""basic-app-header"" Name=""My Application"">
            <div class=""placeholder-logo"" slot=""logo""></div>

            <SiemensIXBlazor.Components.IconButton Id=""icon-button-1"" Ghost=""true"" icon=""checkboxes""></SiemensIXBlazor.Components.IconButton>
            <SiemensIXBlazor.Components.IconButton Id=""icon-button-2"" Ghost=""true"" icon=""checkboxes""></SiemensIXBlazor.Components.IconButton>
            <SiemensIXBlazor.Components.IconButton Id=""icon-button-3"" Ghost=""true"" icon=""checkboxes""></SiemensIXBlazor.Components.IconButton>

            <SiemensIXBlazor.Components.DropdownButton Id=""dropdown-button"" Variant=""Enums.Button.ButtonVariant.secondary"" Label=""Select Config"" Ghost=""true"">
                <SiemensIXBlazor.Components.DropdownItem Id=""dropdown-item-1"" Label=""Config 1""></SiemensIXBlazor.Components.DropdownItem>
                <SiemensIXBlazor.Components.DropdownItem Id=""dropdown-item-2"" Label=""Config 2""></SiemensIXBlazor.Components.DropdownItem>
                <SiemensIXBlazor.Components.DropdownItem Id=""dropdown-item-3"" Label=""Config 3""></SiemensIXBlazor.Components.DropdownItem>
            </SiemensIXBlazor.Components.DropdownButton>

            <SiemensIXBlazor.Components.Avatar.Avatar Id=""avatar"" username=""John Doe"" extra=""Administrator"">
                <SiemensIXBlazor.Components.DropdownItem Id=""avatar-dropdown-item-1"" Label=""Action 1""></SiemensIXBlazor.Components.DropdownItem>
                <SiemensIXBlazor.Components.DropdownItem Id=""avatar-dropdown-item-2"" Label=""Action 2""></SiemensIXBlazor.Components.DropdownItem>
            </SiemensIXBlazor.Components.Avatar.Avatar>
        </SiemensIXBlazor.Components.ApplicationHeader>";

}
