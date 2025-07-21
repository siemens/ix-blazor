// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Playground.Components.Pages.ContainersAndLayout.Drawer;



public partial class Drawer
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
    <Button ClickEvent=""DrawerButtonClicked"">Drawer Button</Button>
    <Drawer @ref=""drawer1"" Id=""drawer1"">
        <span>Some content of drawer</span>
    </Drawer>";

    SiemensIXBlazor.Components.Drawer drawer1;

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            drawer1.FullHeight = true;
            drawer1.CloseOnClickOutside = true;
        }
    }

    private void DrawerButtonClicked()
    {
        drawer1.Show = !drawer1.Show;
    }
}
