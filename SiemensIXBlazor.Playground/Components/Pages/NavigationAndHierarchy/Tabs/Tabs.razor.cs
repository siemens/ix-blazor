// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.NavigationAndHierarchy.Tabs;

public partial class Tabs
{
    private int activeTab = 0;
    private int selectedTab = 0;

    public string ContentForBasic { get; private set; } = @"
         <div class=""tabs"">
            <SiemensIXBlazor.Components.Tabs Id=""basic-tab"" SelectedChangeEvent=""HandleSelectedChangeEvent"">
                <SiemensIXBlazor.Components.Tabs.Item Id=""tab-1"" data-tab-id=""0""> Tab 1 </SiemensIXBlazor.Components.Tabs.Item>
                <SiemensIXBlazor.Components.Tabs.Item Id=""tab-1"" data-tab-id=""1""> Tab 2 </SiemensIXBlazor.Components.Tabs.Item>
                <SiemensIXBlazor.Components.Tabs.Item Id=""tab-1"" data-tab-id=""2""> Tab 3 </SiemensIXBlazor.Components.Tabs.Item>
        </SiemensIXBlazor.Components.Tabs>
        </div>";

    public string ContentForRounded { get; private set; } = @"
         <SiemensIXBlazor.Components.Tabs Id=""rounded-tabs"" Rounded=true>
            <SiemensIXBlazor.Components.Tabs.Item Id=""rounded-tabs-item-1"">
                 <SiemensIXBlazor.Components.IconButton Id=""icon-button-item-1"" Icon=""success""></SiemensIXBlazor.Components.IconButton>
            </SiemensIXBlazor.Components.Tabs.Item>
            <SiemensIXBlazor.Components.Tabs.Item Id=""rounded-tabs-item-2"" Counter=""200"">
                 <SiemensIXBlazor.Components.IconButton Id=""icon-button-item-2"" Icon=""tree""></SiemensIXBlazor.Components.IconButton>
            </SiemensIXBlazor.Components.Tabs.Item>
            <SiemensIXBlazor.Components.Tabs.Item Id=""rounded-tabs-item-3"">
                 <SiemensIXBlazor.Components.IconButton Id=""icon-button-item-3"" Icon=""maintenance""></SiemensIXBlazor.Components.IconButton>
            </SiemensIXBlazor.Components.Tabs.Item>
            <SiemensIXBlazor.Components.Tabs.Item Id=""rounded-tabs-item-4"" Diasabled=""true"" Counter=""24"">
                 <SiemensIXBlazor.Components.IconButton Id=""icon-button-item-4"" Icon=""sound-loud""></SiemensIXBlazor.Components.IconButton>
            </SiemensIXBlazor.Components.Tabs.Item>
            <SiemensIXBlazor.Components.Tabs.Item Id=""rounded-tabs-item-5"">
                 <SiemensIXBlazor.Components.IconButton Id=""icon-button-item-5"" Icon=""hierarchy""></SiemensIXBlazor.Components.IconButton>
            </SiemensIXBlazor.Components.Tabs.Item>
            <SiemensIXBlazor.Components.Tabs.Item Id=""rounded-tabs-item-6"" Disabled=""true"">
                 <SiemensIXBlazor.Components.IconButton Id=""icon-button-item-6"" Icon=""calendar-settings""></SiemensIXBlazor.Components.IconButton>
            </SiemensIXBlazor.Components.Tabs.Item>
        </SiemensIXBlazor.Components.Tabs>";
    private void HandleSelectedChangeEvent(int tabId)
    {
        selectedTab = tabId;
    }
}
