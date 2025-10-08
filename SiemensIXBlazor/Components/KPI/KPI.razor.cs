// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.KPI;

namespace SiemensIXBlazor.Components
{
    public partial class KPI
    {
        [Parameter]
        public string? AriaLabelAlarmIcon { get; set; }
        [Parameter]
        public string? AriaLabelWarningIcon { get; set; }   
        [Parameter]
        public string? Label { get; set; }
        [Parameter]
        public KpiOrientation Orientation { get; set; } = KpiOrientation.Horizontal;
        [Parameter]
        public KpiState State { get; set; } = KpiState.Neutral;
        [Parameter]
        public string? Unit { get; set; }
        [Parameter]
        public string? Value { get; set; }
    }
}
