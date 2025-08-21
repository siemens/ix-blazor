// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using System.Threading.Tasks;

namespace SiemensIXBlazor.Playground.Components.Pages.ApplicationFrame.Settings;

public partial class Settings
{
    private int activeTab = 0;
    private SiemensIXBlazor.Components.MenuSettings.MenuSettings settingsMenuElement;
    public string ContentForBasic { get; private set; } = @"
        <SiemensIXBlazor.Components.Application Id=""application"">
            <SiemensIXBlazor.Components.ApplicationHeader Id=""application-header"">
                <div class=""placeholder-logo"" slot=""logo""></div>
            </SiemensIXBlazor.Components.ApplicationHeader>
            <SiemensIXBlazor.Components.Menu.Menu Id=""settings-menu"" @onclick=""HandleToggleSettings"">
                <SiemensIXBlazor.Components.MenuSettings.MenuSettings Id=""menu-settings"" @ref=""menuSettings"">
                    <SiemensIXBlazor.Components.MenuSettings.MenuSettingsItem  Label=""Example Setting 1""></SiemensIXBlazor.Components.MenuSettings.MenuSettingsItem>
                    <SiemensIXBlazor.Components.MenuSettings.MenuSettingsItem  Label=""Example Setting 2""></SiemensIXBlazor.Components.MenuSettings.MenuSettingsItem>
                </SiemensIXBlazor.Components.MenuSettings.MenuSettings>
            </SiemensIXBlazor.Components.Menu.Menu>
        </SiemensIXBlazor.Components.Application>";

    private SiemensIXBlazor.Components.MenuSettings.MenuSettings menuSettings;
    public async Task HandleToggleSettings()
    {
        if (menuSettings != null)
        {
            await menuSettings.ToggleSettings(true);
        }
    }

}
