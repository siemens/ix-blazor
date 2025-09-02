// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.NavigationAndHierarchy.Pagination;

public partial class Pagination
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
         <SiemensIXBlazor.Components.Pagination.Pagination Id=""basic-pagination"" Count=""100""></SiemensIXBlazor.Components.Pagination.Pagination>";

    public string ContentForAdvanced { get; private set; } = @"
          <SiemensIXBlazor.Components.Pagination.Pagination Id=""advanced-pagination"" Advanced=""true"" ShowItemCount=""true"" Count=""100""></SiemensIXBlazor.Components.Pagination.Pagination>";
}
