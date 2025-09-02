// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.NavigationAndHierarchy.Breadcrumb;

public partial class Breadcrumb
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
        <SiemensIXBlazor.Components.Breadcrumb Id=""basic-breadcrumb"">
            <SiemensIXBlazor.Components.BreadcrumbItem Id=""basic-breadcrumb-item-1"" Label=""Item 1"">
            </SiemensIXBlazor.Components.BreadcrumbItem>
            <SiemensIXBlazor.Components.BreadcrumbItem Id=""basic-breadcrumb-item-2"" Label=""Item 2"">
            </SiemensIXBlazor.Components.BreadcrumbItem>
            <SiemensIXBlazor.Components.BreadcrumbItem Id=""basic-breadcrumb-item-3"" Label=""Item 3"">
            </SiemensIXBlazor.Components.BreadcrumbItem>
        </SiemensIXBlazor.Components.Breadcrumb>";

    public string ContentForTruncate { get; private set; } = @"
         <SiemensIXBlazor.Components.Breadcrumb Id=""truncate-breadcrumb"" VisibleItemCount=3>
            <SiemensIXBlazor.Components.BreadcrumbItem Id=""truncate-breadcrumb-item-1"" Label=""Item 1""></SiemensIXBlazor.Components.BreadcrumbItem>
            <SiemensIXBlazor.Components.BreadcrumbItem Id=""truncate-breadcrumb-item-2"" Label=""Item 2""></SiemensIXBlazor.Components.BreadcrumbItem>
            <SiemensIXBlazor.Components.BreadcrumbItem Id=""truncate-breadcrumb-item-3"" Label=""Item 3""></SiemensIXBlazor.Components.BreadcrumbItem>
            <SiemensIXBlazor.Components.BreadcrumbItem Id=""truncate-breadcrumb-item-4"" Label=""Item 4""></SiemensIXBlazor.Components.BreadcrumbItem>
            <SiemensIXBlazor.Components.BreadcrumbItem Id=""truncate-breadcrumb-item-5"" Label=""Item 5""></SiemensIXBlazor.Components.BreadcrumbItem>
        </SiemensIXBlazor.Components.Breadcrumb>";

    private string[] NextItems = new[] { "Next Item 1" };

    private void SetNextItem()
    {
        NextItems = new[] { "Next Item 1", "Next Item 2" };
        StateHasChanged();
    }
}
