// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.SystemFeedbackAndStatus.Tooltip;

public partial class Tooltip
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
        <div class=""ix-row"">
            <div class=""ix-button"">
                <SiemensIXBlazor.Components.Button Id=""first-basic"" Class=""any-class"">
                    Hover me
                </SiemensIXBlazor.Components.Button>
            </div>
            <SiemensIXBlazor.Components.Tooltip Id=""tooltip-1"" For="".any-class"">
                Simple Selector
            </SiemensIXBlazor.Components.Tooltip>

            <div class=""ix-button"">
                <SiemensIXBlazor.Components.Button Id=""second-basic"" my-custom-special-selector=""any-value"">
                    Also hover me
                </SiemensIXBlazor.Components.Button>
            </div>
            <SiemensIXBlazor.Components.Tooltip Id=""tooltip-2"" For=""[my-custom-special-selector='any-value']"">
                Custom Selector
            </SiemensIXBlazor.Components.Tooltip>
        </div>";

}
