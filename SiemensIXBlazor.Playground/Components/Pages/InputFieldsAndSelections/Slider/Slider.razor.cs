// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.InputFieldsAndSelections.Slider;

public partial class Slider
{
    private int activeTab = 0;

    public string CodeContent { get; private set; } = @"
    <Slider Id=""slider-demo"" Min=""0"" Max=""50"" Step=""5"" Value=""0"" Marker=""[0, 10, 20, 30, 40, 50]"">
        <span slot=""label-start"">0</span>
        <span slot=""label-end"">50</span>
    </Slider>";
}
