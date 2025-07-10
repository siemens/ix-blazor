// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.InputFieldsAndSelections.Select;

public partial class Select
{
    private int activeTab = 0;

    public string CodeContent { get; private set; } = @"
    <Select ItemSelectionChangeEvent=SelectItemSelectedChanged
        AddItemEvent=""SelectItemAdded"" Mode=""SelectMode.Single"" SelectedIndices=""2"" Id=""select1"">
            <SelectItem Id=""selectItem1"" Label=""Item 1"" Value=""1""></SelectItem>
            <SelectItem Id=""selectItem2"" Label=""Item 2"" Value=""2""></SelectItem>
    </Select>";
}
