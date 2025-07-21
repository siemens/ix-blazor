// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.ContainersAndLayout.Blind;

public partial class Blind
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
    <Blind
        Label=""Test Blind""
        Id=""blind1""
        CollapsedChangedEvent=""(value) => BlindEventHandler(value)"">
        Test content
    </Blind>";
}
