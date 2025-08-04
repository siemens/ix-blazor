// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.InputFieldsAndSelections.Toggle;

public partial class Toggle
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
        <Toggle></Toggle>";
    public string ContentForCustomLabel { get; private set; } = @"
        <Toggle TextOn=""Online"" TextOff=""Offline""></Toggle>";
    public string ContentForDisabled { get; private set; } = @"
        <Toggle Disabled></Toggle>";
    public string ContentForChecked { get; private set; } = @"
        <Toggle Checked></Toggle>";
    public string ContentForIndeterminate { get; private set; } = @"
        <Toggle Indeterminate></Toggle>";
}
