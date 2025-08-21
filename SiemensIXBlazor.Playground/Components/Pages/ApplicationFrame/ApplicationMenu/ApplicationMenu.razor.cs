// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.ApplicationFrame.ApplicationMenu;

public partial class ApplicationMenu
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
        <div class=""ix-menu"">
            <SiemensIXBlazor.Components.Menu.Menu Id=""app-menu"">
                <SiemensIXBlazor.Components.Menu.MenuItem Id=""menu-item-1"" Home=""true"" Icon=""home"">
                    Home
                </SiemensIXBlazor.Components.Menu.MenuItem>
                <SiemensIXBlazor.Components.Menu.MenuItem Id=""menu-item-2"" Icon=""globe"">
                    Normal Tab
                </SiemensIXBlazor.Components.Menu.MenuItem>
                <SiemensIXBlazor.Components.Menu.MenuItem Id=""menu-item-3"" Icon=""star"" Disabled=""true"">
                    Disabled tab
                </SiemensIXBlazor.Components.Menu.MenuItem>
                <SiemensIXBlazor.Components.Menu.MenuItem Id=""menu-item-4"" Icon=""star"">
                    With other icon
                </SiemensIXBlazor.Components.Menu.MenuItem>
                <SiemensIXBlazor.Components.Menu.MenuItem Id=""menu-item-5"" Icon=""globe"" Style=""display: none"">
                    Should not be visible
                </SiemensIXBlazor.Components.Menu.MenuItem>
            </SiemensIXBlazor.Components.Menu.Menu>
        </div>     ";

    public string ContentForSecondNavigationLevel { get; private set; } = @"
        <SiemensIXBlazor.Components.Application Id=""nav-application"" ForceBreakpoint=""Enums.ForceBreakpoint.lg"">
            <SiemensIXBlazor.Components.Menu.Menu Id=""nav-menu"">
                <SiemensIXBlazor.Components.Menu.MenuItem Id=""nav-menu-item-1"" Home=""true"" Icon=""home"">
                    Home
                </SiemensIXBlazor.Components.Menu.MenuItem>
                <SiemensIXBlazor.Components.Menu.MenuItem Id=""nav-menu-item-2"" Icon=""globe"">
                    Normal Tab
                </SiemensIXBlazor.Components.Menu.MenuItem>
                <SiemensIXBlazor.Components.MenuCategory.MenuCategory Id=""menu-category"" Label=""Top level Category"" Icon=""rocket"">
                    <SiemensIXBlazor.Components.Menu.MenuItem Id=""cat-menu-item-1"" Icon=""globe"">Nested Tab</SiemensIXBlazor.Components.Menu.MenuItem>
                    <SiemensIXBlazor.Components.Menu.MenuItem Id=""cat-menu-item-2"" Icon=""globe"">Nested Tab</SiemensIXBlazor.Components.Menu.MenuItem>
                </SiemensIXBlazor.Components.MenuCategory.MenuCategory>
            </SiemensIXBlazor.Components.Menu.Menu>
        </SiemensIXBlazor.Components.Application>";

    public string ContentForAvatar { get; private set; } = @"
        <SiemensIXBlazor.Components.Menu.Menu Id=""avatar-menu"">
            <SiemensIXBlazor.Components.MenuAvatar.MenuAvatar Id=""menu-avatar"" Top=""john.doe@company.com"" Bottom=""Administrator"" Image=""https://ui-avatars.com/api/?name=John+Doe"">
                <SiemensIXBlazor.Components.MenuAvatar.MenuAvatarItem Id=""menu-avatar-item"" Icon=""user-profile"" Label=""User profile...""></SiemensIXBlazor.Components.MenuAvatar.MenuAvatarItem>
            </SiemensIXBlazor.Components.MenuAvatar.MenuAvatar>

            <SiemensIXBlazor.Components.Menu.MenuItem Id=""avatar-menu-item-1"" Home=""true"" Icon=""home"">Home</SiemensIXBlazor.Components.Menu.MenuItem>
            <SiemensIXBlazor.Components.Menu.MenuItem Id=""avatar-menu-item-2"" Icon=""globe"">Normal Tab</SiemensIXBlazor.Components.Menu.MenuItem>
            <SiemensIXBlazor.Components.Menu.MenuItem Id=""avatar-menu-item-3"" Icon=""star"" Disabled=""true"">Disabled tab</SiemensIXBlazor.Components.Menu.MenuItem>
            <SiemensIXBlazor.Components.Menu.MenuItem Id=""avatar-menu-item-4"" Icon=""star"">With other icon</SiemensIXBlazor.Components.Menu.MenuItem>
            <SiemensIXBlazor.Components.Menu.MenuItem Id=""avatar-menu-item-5"" Icon=""globe"" style=""display: none"">Should not be visible</SiemensIXBlazor.Components.Menu.MenuItem>
        </SiemensIXBlazor.Components.Menu.Menu>";

}
