// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.DataDisplay.KPI;

public partial class KPI
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
        <div class=""example"">
            <div class=""kpi"">
                <SiemensIXBlazor.Components.KPI id=""basic-kpi-1"" Label=""Motor Speed"" Value=""Nominal""></SiemensIXBlazor.Components.KPI>
                <SiemensIXBlazor.Components.KPI id=""basic-kpi-2"" Label=""Motor Speed"" Value=""122.6"" Unit=""rpm""></SiemensIXBlazor.Components.KPI>
                <SiemensIXBlazor.Components.KPI id=""basic-kpi-3"" Label=""Motor Speed"" Value=""122.6"" State=""Enums.KPI.KpiState.Alarm""></SiemensIXBlazor.Components.KPI>
                <SiemensIXBlazor.Components.KPI id=""basic-kpi-4"" Label=""Motor Speed"" Value=""122.6"" State=""Enums.KPI.KpiState.Warning""></SiemensIXBlazor.Components.KPI>

                <SiemensIXBlazor.Components.KPI id=""basic-kpi-5"" Label=""Motor Speed"" Value=""Nominal"" Orientation=""Enums.KPI.KpiOrientation.Vertical""></SiemensIXBlazor.Components.KPI>
                <SiemensIXBlazor.Components.KPI id=""basic-kpi-6"" Label=""Motor Speed"" Value=""122.6"" Unit=""rpm"" State=""Enums.KPI.KpiState.Alarm"" Orientation=""Enums.KPI.KpiOrientation.Vertical""></SiemensIXBlazor.Components.KPI>
            </div>
        </div>";
}
