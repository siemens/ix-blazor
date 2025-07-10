// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.InputFieldsAndSelections.ExpandingSearch;

public partial class ExpandingSearch
{
    private int activeTab = 0;

    public string CodeContent { get; private set; } = @"
        <ExpandingSearch Id=""exp-search""
            ValueChangedEvent=""(value) => SearchValueChanged(value)"">
        </ExpandingSearch>";
}
