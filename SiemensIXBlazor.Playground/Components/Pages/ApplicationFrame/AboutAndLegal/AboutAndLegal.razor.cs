// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.ApplicationFrame.AboutAndLegal;

using SiemensIXBlazor.Components.MenuAbout;
public partial class AboutAndLegal
{
    private int activeTab = 0;
    private SiemensIXBlazor.Components.MenuAbout.MenuAbout basicMenuAbout;

    public string ContentForBasic { get; private set; } = @"
        <SiemensIXBlazor.Components.Application Id=""basic-application"">
            <SiemensIXBlazor.Components.ApplicationHeader Id=""basic-application-header"">
                <div class=""placeholder-logo"" slot=""logo""></div>
            </SiemensIXBlazor.Components.ApplicationHeader>
            <SiemensIXBlazor.Components.Menu.Menu Id=""basic-app-menu"" @onclick=""HandleToggleAbout"">
                <SiemensIXBlazor.Components.MenuAbout.MenuAbout Id=""basic-app-menu-about"" @ref=""basicMenuAbout"">
                    <SiemensIXBlazor.Components.MenuAbout.MenuAboutItem  Label=""Tab 1"">
                        Content 1
                    </SiemensIXBlazor.Components.MenuAbout.MenuAboutItem>
                    <SiemensIXBlazor.Components.MenuAbout.MenuAboutItem  Label=""Tab 2"">
                        Content 2
                    </SiemensIXBlazor.Components.MenuAbout.MenuAboutItem>
                </SiemensIXBlazor.Components.MenuAbout.MenuAbout>
            </SiemensIXBlazor.Components.Menu.Menu>
        </SiemensIXBlazor.Components.Application>";
    public async Task HandleToggleAbout()
    {
        if (basicMenuAbout != null) 
        {
            await basicMenuAbout.ToggleAbout(true);
        }   
    }
}
