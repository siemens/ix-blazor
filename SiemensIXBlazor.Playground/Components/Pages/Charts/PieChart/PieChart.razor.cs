// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using SiemensIXBlazor.Playground.Components.Pages.Charts.BarChart;
using System.Reflection.Emit;

namespace SiemensIXBlazor.Playground.Components.Pages.Charts.PieChart;

public partial class PieChart
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
        <div class=""pie-chart"">
            <SiemensIXBlazor.Components.ECharts.ECharts Id=""basic-pie-chart"" @ref=""piechart1"">

            </SiemensIXBlazor.Components.ECharts.ECharts>
        </div>";

    public string ContentForDonutCharts { get; private set; } = @"
        <div class=""pie-chart"">
            <SiemensIXBlazor.Components.ECharts.ECharts Id=""donut-pie-chart"" @ref=""donutchart"">

            </SiemensIXBlazor.Components.ECharts.ECharts>
        </div>";

    SiemensIXBlazor.Components.ECharts.ECharts piechart;
    SiemensIXBlazor.Components.ECharts.ECharts donutchart;

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender && piechart is not null && donutchart is not null)
        {
            createBasicPieChart();
            createDonutChart();
        }
    }

    public async Task createBasicPieChart()
    {
        var data = new List<Dictionary<string, object>>
        {
            new Dictionary<string, object> { { "value", 29.4 }, { "name", "China" } },
            new Dictionary<string, object> { { "value", 14.3 }, { "name", "U.S" } },
            new Dictionary<string, object> { { "value", 9.8 },  { "name", "EEA" } },
            new Dictionary<string, object> { { "value", 6.8 },  { "name", "India" } },
            new Dictionary<string, object> { { "value", 4.9 },  { "name", "Russia" } },
            new Dictionary<string, object> { { "value", 3.5 },  { "name", "Japan" } },
            new Dictionary<string, object> { { "value", 31.5 }, { "name", "Other" } }
        };


        var dynamicobject1 = new Dictionary<string, object>();

        var tooltip = new Dictionary<string, object>
            {
                {"trigger", "item" }
            };
        dynamicobject1.Add("tooltip", tooltip);

        var legend = new Dictionary<string, object>
            {
                {"icon", "rect" },
                {"bottom", "0" },
                {"left", "0" }

            };
        dynamicobject1.Add("legend", legend);

        var series = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    {"name", "CO2 emissions from<" },
                    {"type", "pie" },
                    {"radius", "80%" },
                    {"data", data },
                    {"label", new Dictionary<string, object>
                    {
                        {"show", true },
                        {"color", "color-neutral" } 
                    }
                    },
                    {"emphasis", new Dictionary<string, object>
                        {
                            {"itemStyle", new Dictionary<string, object>()
                            {
                                {"shadowBlur", 10 },
                                {"shadowOffsetX", 0 },
                                {"shadowColor", "rgba(0, 0, 0, 0.5)" }

                            }
                            }
                        }
                    }
                }
            };
        dynamicobject1.Add("series", series);
        piechart.InitialChart(dynamicobject1);


    }

    public async Task createDonutChart()
    {
        var data = new List<Dictionary<string, object>>
        {
            new Dictionary<string, object> { { "value", 72.17 }, { "name", "Windows" } },
            new Dictionary<string, object> { { "value", 15.42 }, { "name", "macOS" } },
            new Dictionary<string, object> { { "value", 4.03 },  { "name", "Linux" } },
            new Dictionary<string, object> { { "value", 2.27 },  { "name", "Chrome OS" } },
            new Dictionary<string, object> { { "value", 6.11 },  { "name", "Other" } }
        };


        var dynamicObject2 = new Dictionary<string, object>();

        var tooltip = new Dictionary<string, object>
            {
                {"trigger", "item" }
            };
        dynamicObject2.Add("tooltip", tooltip);

        var legend = new Dictionary<string, object>
            {
                {"icon", "rect"},
                {"bottom", "0" },
                {"left", "0" }
            };
        dynamicObject2.Add("legend", legend);

        var series = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "name", "OS Share" },
                    { "type", "pie" },
                    { "radius", new List<object> { "60%", "90%" } },
                    { "label", new Dictionary<string, object>
                        {
                            { "show", true },
                            { "color", "color-neutral" } 
                        }
                    },

                    { "emphasis", new Dictionary<string, object>
                        {
                            { "label", new Dictionary<string, object>
                                {
                                    { "show", true },
                                    { "fontSize", 25 },
                                    { "fontWeight", "bold" }
                                }
                            }
                        }
                    },

                    { "labelLine", new Dictionary<string, object>
                        {
                            { "show", true }
                        }
                    },

                    { "data", data }

                }
            };

        dynamicObject2.Add("series", series);

        donutchart.InitialChart(dynamicObject2);


    }
}
