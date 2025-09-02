// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using SiemensIXBlazor.Enums.BasicNavigation;
using SiemensIXBlazor.Objects.Application;

namespace SiemensIXBlazor.Playground.Components.Pages.ApplicationFrame.Application;

public partial class Application
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
        <SiemensIXBlazor.Components.Application Id=""basic-app"">
            <SiemensIXBlazor.Components.ApplicationHeader Id=""basic-app-header"" Name=""My Application"">
                <div className=""placeholder-logo"" slot=""logo""></div>
            </SiemensIXBlazor.Components.ApplicationHeader>

            <SiemensIXBlazor.Components.Menu.Menu Id=""basic-app-menu"">
                <SiemensIXBlazor.Components.Menu.MenuItem id=""basic-app-menu-item-1"">
                    Item 1
                </SiemensIXBlazor.Components.Menu.MenuItem>
                <SiemensIXBlazor.Components.Menu.MenuItem id=""basic-app-menu-item-2"">
                    Item 2
                </SiemensIXBlazor.Components.Menu.MenuItem>
            </SiemensIXBlazor.Components.Menu.Menu>

            <SiemensIXBlazor.Components.Content Id=""basic-app-content"">
                <SiemensIXBlazor.Components.ContentHeader id=""basic-app-content-header"" slot=""header"" HeaderTitle=""My Content Page"">
                </SiemensIXBlazor.Components.ContentHeader>
            </SiemensIXBlazor.Components.Content>
        </SiemensIXBlazor.Components.Application>";

    private string[] Breakpoints = new string[] {"md"};

    private bool IsChecked(string bp) => Breakpoints[0] == bp;
    private void SetBreakpoint(string bp)
    {
        Breakpoints = new[] { bp };
        StateHasChanged();
    }

    SiemensIXBlazor.Components.Application _app;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
           
            AppSwitchConfig config = new()
            {
                I18nAppSwitch = "Switch to Application",
                CurrentAppId = "demo-app-2",
                Apps = new List<SiemensIXBlazor.Objects.Application.App> { 
                    new SiemensIXBlazor.Objects.Application.App()
                    {
                        Id = "demo-app-1",
                        Name = "Floor App",
                        Description = "Example description for Floor app",
                        Url = "https://ix.siemens.io/",
                        Target = "_self",
                        IconSrc = "https://www.svgrepo.com/show/530661/genetic-data.svg"
                    },
                    new SiemensIXBlazor.Objects.Application.App()
                    { 
                        Id = "demo-app-2",
                        Name = "Calculator App",
                        Description = "Example description for Calculator app",
                        Url = "https://ix.siemens.io/",
                        Target = "_self",
                        IconSrc = "https://www.svgrepo.com/show/530661/genetic-data.svg"
                   }
                }
            };
    
         _app.AppSwitchConfig=config;
        }
    }


}
