// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.SystemFeedbackAndStatus.Spinner;

public partial class Spinner
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
        <SiemensIXBlazor.Components.Spinner id=""basic"">
            
        </SiemensIXBlazor.Components.Spinner>";
    public string ContentForLarge { get; private set; } = @"
        <SiemensIXBlazor.Components.Spinner id=""large"" Size=""large"">

        </SiemensIXBlazor.Components.Spinner>";
}
