// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.SystemFeedbackAndStatus.EmptyState;

public partial class EmptyState
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
        <SiemensIXBlazor.Components.EmptyState Id=""empty-state-basic"" Header=""No Elements available"" SubHeader=""Create an element first"" Icon=""add"" Action=""Create an element"" >
        </SiemensIXBlazor.Components.EmptyState>";

    public string ContentForCompact { get; private set; } = @"
        <SiemensIXBlazor.Components.EmptyState Id=""empty-state-compact"" Header=""No Elements available"" SubHeader=""Create an element first"" Icon=""add"" Action=""Create an element"" Layout=""compact"">
        </SiemensIXBlazor.Components.EmptyState>";

    public string ContentForCompactBreak { get; private set; } = @"
        <SiemensIXBlazor.Components.EmptyState Id=""empty-state-compact-break"" Header=""No Elements available"" SubHeader=""Create an element first"" Icon=""add"" Action=""Create an element"" Layout=""compactBreak"">
        </SiemensIXBlazor.Components.EmptyState>";
}
