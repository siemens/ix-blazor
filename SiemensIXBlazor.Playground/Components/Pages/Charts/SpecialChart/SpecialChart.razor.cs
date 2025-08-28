// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using SiemensIXBlazor.Playground.Components.Pages.Charts.PieChart;

namespace SiemensIXBlazor.Playground.Components.Pages.Charts.SpecialChart;

public partial class SpecialChart
{
    private int activeTab = 0;

    public string ContentForInteractiveToolbox { get; private set; } = @"
        <div class=""special-chart"">
            <SiemensIXBlazor.Components.ECharts.ECharts Id=""interactive-toolbox-chart"" @ref=""interactiveToolboxChart"">

            </SiemensIXBlazor.Components.ECharts.ECharts>
        </div>";

    public string ContentForAdvancedZoomAndPan { get; private set; } = @"
        <div class=""special-chart"">
            <SiemensIXBlazor.Components.ECharts.ECharts Id=""advanced-zoom-and-pan-chart"" @ref=""advancedZoomAndPanChart"">

            </SiemensIXBlazor.Components.ECharts.ECharts>
        </div>";

    SiemensIXBlazor.Components.ECharts.ECharts interactiveToolboxChart;
    SiemensIXBlazor.Components.ECharts.ECharts advancedZoomAndPanChart;
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender && interactiveToolboxChart is not null && advancedZoomAndPanChart is not null)
        {
            createInteractiveToolbox();
            createAdvancedZoomAndPan();
        }
    }

    private List<string> months = new() { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
    private List<int> values = new() { 150, 230, 224, 218, 135, 147, 260 };
    public async Task createInteractiveToolbox()
    {
        var dynamicObject1 = new Dictionary<string, object>();

        var toolbox = new Dictionary<string, object>
            {
                    {"feature", new Dictionary<string, object>
                        {
                            {"dataZoom", new Dictionary<string,object>
                                {
                                    {"yAxisIndex", "none" }
                                }
                            },
                            {"restore", new Dictionary<string, object>() },
                            {"saveAsImage", new Dictionary<string, object>() },
                            {"magicType", new Dictionary<string,object>
                                {
                                    {"type", new List<string> {"line", "bar"} }
                                }
                            },
                            {"dataView", new Dictionary<string, object>
                                {
                                    {"show", true }
                                }
                            }
                        }
                    }
            };
        dynamicObject1.Add("toolbox", toolbox);

        var xAxis = new Dictionary<string, object>
            {
                    {"type", "category" },
                    {"data", months }
            };
        dynamicObject1.Add("xAxis", xAxis);

        var yAxis = new Dictionary<string, object>
            {
                    {"type", "value" }
            };
        dynamicObject1.Add("yAxis", yAxis);

        var series = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    {"data", values },
                    {"type", "line" },
                    {"step", "end" }
                }
            };
        dynamicObject1.Add("series", series);

        interactiveToolboxChart.InitialChart(dynamicObject1);

    }


    public async Task createAdvancedZoomAndPan()
    {
        var baseDate = new DateTime(1968, 10, 3);
        const int oneDay = 1;

        List<string> date = new();
        List<double> data = new() { 0 };

        void generateData()
        {
            var rnd = new Random();

            for (int i = 1; i < 20000; i++)
            {
                baseDate = baseDate.AddDays(oneDay);
                date.Add($"{baseDate.Year}/{baseDate.Month}/{baseDate.Day}");
                data.Add(Math.Round((rnd.NextDouble() - 0.5) * 20 + data[i - 1], 2));
            }
        }

        generateData();

        var dynamicObject2 = new Dictionary<string, object>();

        var toolbox = new Dictionary<string, object>
            {
                    {"feature", new Dictionary<string, object>
                        {
                            {"dataZoom", new Dictionary<string, object>
                                {
                                    {"yAxisIndex", "none" }
                                }
                            }
                        }
                    }
            };
        dynamicObject2.Add("toolbox", toolbox);

        var xAxis = new Dictionary<string, object>
            {
                    {"type", "category" },
                    {"boundaryGap", false },
                    {"data", date }
            };
        dynamicObject2.Add("xAxis", xAxis);

        var yAxis = new Dictionary<string, object>
            {
                    {"type", "value" },
                    {"boundaryGap", new List<object> {0,"100%"}}
            };
        dynamicObject2.Add("yAxis", yAxis);

        var dataZoom = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    {"type", "inside" },
                    {"start", 0 },
                    {"end", 10 }
                },

                new Dictionary <string, object>
                {
                    {"start", 0 },
                    {"end", 10 }
                }
            };
        dynamicObject2.Add("dataZoom", dataZoom);

        var areaStyle = new Dictionary<string, object>
            {
                {"color", new Dictionary<string, object>
                {
                    {"type", "linear" },
                    {"x", 0 },
                    {"y", 0},
                    {"x2", 0 },
                    {"y2", 1 },
                    {"colorStops", new List<Dictionary<string,object>>
                        {
                            new Dictionary<string, object>
                            {
                                {"offset",0 },
                                {"color", "color-primary" } 
                            },
                            new Dictionary<string, object>
                            {
                                {"offset", 1 },
                                {"color", "transparent" }
                            }
                        }
                    }
                }
                }
            };

        var series = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    {"name", "Synthetic data" },
                    {"type", "line" },
                    {"symbol", "none" },
                    {"sampling", "lttb" },
                    {"areaStyle", areaStyle },
                    {"data", data }

                }
            };
        dynamicObject2.Add("series", series);


        advancedZoomAndPanChart.InitialChart(dynamicObject2);


    }
}
