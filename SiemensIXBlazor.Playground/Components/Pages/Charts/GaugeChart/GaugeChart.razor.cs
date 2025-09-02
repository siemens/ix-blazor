// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using Newtonsoft.Json.Linq;
using SiemensIXBlazor.Playground.Components.Pages.Charts.BarChart;
using System.Security.Claims;

namespace SiemensIXBlazor.Playground.Components.Pages.Charts.GaugeChart;

public partial class GaugeChart
{
    private int activeTab = 0;

    public string ContentForMetricGauge { get; private set; } = @"
        <div class=""gauge-chart"">
            <SiemensIXBlazor.Components.ECharts.ECharts Id=""metric-gauge-chart"" @ref=""metricGaugeChart"">

            </SiemensIXBlazor.Components.ECharts.ECharts>
        </div>";

    public string ContentForCircleGauge { get; private set; } = @"
        <div class=""gauge-chart"">
            <SiemensIXBlazor.Components.ECharts.ECharts Id=""circle-gauge-chart"" @ref=""circleGaugeChart"">

            </SiemensIXBlazor.Components.ECharts.ECharts>
        </div>";

    public string ContentForArcGauge { get; private set; } = @"
         <div class=""gauge-chart"">
            <SiemensIXBlazor.Components.ECharts.ECharts Id=""arc-gauge-chart"" @ref=""arcGaugeChart"">

            </SiemensIXBlazor.Components.ECharts.ECharts>
        </div>";

    SiemensIXBlazor.Components.ECharts.ECharts metricGaugeChart;
    SiemensIXBlazor.Components.ECharts.ECharts circleGaugeChart;
    SiemensIXBlazor.Components.ECharts.ECharts arcGaugeChart;

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender && metricGaugeChart is not null && circleGaugeChart is not null && arcGaugeChart is not null)
        {
            createMetricGaugeChart();
            createCircleGaugeChart();
            createArcGaugeChart();
        }
    }
    private double value = 45.3;

    private readonly Dictionary<string, string> GaugeColors = new() 
    {
        { "success", "#4fbf5a" },
        { "warning", "#ffc738" },
        { "alarm", "#e64646" },
        { "neutral", "#b3bac5" }

    };

    private string GetGaugeColor(double value)
    {
        if (value > 60) return GaugeColors["success"];
        else if (value > 25) return GaugeColors["warning"];
        else return GaugeColors["alarm"];
    }

    public async Task createMetricGaugeChart()
    {
        var value = 45.3;

        var dynamicObject1 = new Dictionary<string, object>();

        var series = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    {"id", "1" },
                    {"type", "gauge" },
                    {"axisLine", new Dictionary<string, object>
                        {
                            {"show", true },
                            {"lineStyle", new Dictionary<string,object>
                                {
                                    {"width",18 },
                                    {"color", new List<object> { new List<object> { 1, GaugeColors["neutral"] } } } 
                                }
                            }
                        }
                    },
                    {"axisTick", new Dictionary<string,object>
                        {
                            {"show", false }
                        }
                    },
                    {"radius", "75%" },
                    {"center", new List<string> {"50%", "60%"} },
                    {"startAngle", 180 },
                    {"endAngle", 0 },
                    {"splitNumber", 1 },
                    {"splitLine", new Dictionary<string, object>
                        {
                            {"show",true }
                        }
                    },
                    {"axisLabel", new Dictionary<string,object>
                        {
                            {"show",true },
                            {"distance", 30 },
                            {"fontSize", 16 }
                        }
                    },
                    {"progress", new Dictionary<string,object>
                        {
                            {"show",true },
                            {"overlap", false },
                            {"width",35 },
                            {"itemStyle", new Dictionary<string,object>
                                {
                                    {"borderMiterLimit", 16 },
                                    {"color", GetGaugeColor(value)}
                                }
                            }

                        }
                    },
                    {"pointer",new Dictionary<string,object>
                        {
                            {"show", false }
                        }
                    },
                    {"data", new List<Dictionary<string,object>>
                        {
                            new Dictionary<string, object>
                            {
                                {"value", value },
                                {"title", new Dictionary<string,object>
                                    {
                                        {"show", false }
                                    }
                                },
                                {"detail", new Dictionary<string,object>
                                    {
                                        {"show",true },
                                        {"offsetCenter", new List<int> {0,-70} },
                                        {"overflow", "break" },
                                        {"fontSize", "1.5rem" },
                                        {"width", 250 },
                                        {"lineHeight", 35 },
                                        {"color", "#888"  },  
                                        {"formatter", "{value}Mbps \nNetwork Speed" }
                                    }
                                },
                                {"pointer", new Dictionary<string,object>
                                    {
                                        {"show", false }
                                    }
                                }
                            }
                        }
                    }
                },

                new Dictionary<string, object>
                {
                    {"id", "2" },
                    {"type", "gauge" },
                    {"splitLine", new Dictionary<string, object>
                        {
                            {"show",false }
                        }
                    },
                    {"axisTick", new Dictionary<string,object>
                        {
                            {"show", false }
                        }
                    },
                    {"axisLabel", new Dictionary<string,object>
                        {
                            {"show", false }
                        }
                    },
                    {"axisLine", new Dictionary<string, object>
                        {
                            {"show", true },
                            {"lineStyle", new Dictionary<string,object>
                                {
                                    {"width",5 },
                                    {"color", new List<object>
                                        {
                                            new List<object> { 0.25, GaugeColors["alarm"] },
                                            new List<object> { 0.6, GaugeColors["warning"] },
                                            new List<object> { 1, GaugeColors["success"] }
                                        }
                                    },
                                }
                            }
                        }
                    },
                    {"radius", "80%" },
                    {"center", new List<string> {"50%", "60%"} },
                    {"startAngle", 180 },
                    {"endAngle", 0 },
                }
            };
        dynamicObject1.Add("series", series);

        metricGaugeChart.InitialChart(dynamicObject1);
    }

    public async Task createCircleGaugeChart()
    {
        var value = 60;

        var dynamicObject2 = new Dictionary<string, object>();

        var series = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "id", "1" },
                    { "type", "gauge" },
                    {"axisLine",new Dictionary<string, object>
                        {
                            { "show", true },
                            {"lineStyle",new Dictionary<string, object>
                                {
                                    { "width", 15 },
                                    { "color", new List<object> { new List<object> { 1, GaugeColors["neutral"] } } }  
                                }
                            }
                        }
                    },
                    {"axisTick",new Dictionary<string, object>
                        {
                            { "show", false }
                        }
                    },
                    { "radius", "100%" },
                    { "startAngle", 90 },
                    { "endAngle", -270 },
                    {"splitLine",new Dictionary<string, object>
                        {
                            { "show", false }
                        }
                    },
                    {"axisLabel",new Dictionary<string, object>
                        {
                            { "show", false }
                        }
                    },
                    {"progress",new Dictionary<string, object>
                        {
                            { "show", true },
                            { "overlap", false },
                            { "width", 35 },
                            {"itemStyle",new Dictionary<string, object>
                                {
                                    { "borderMiterLimit", 16 },
                                    { "color", GaugeColors["success"] }
                                }
                            }
                        }
                    },
                    {"pointer", new Dictionary<string, object>
                        {
                            { "show", false }
                        }
                    },
                    { "data",new List<Dictionary<string, object>>
                        {
                            new Dictionary<string, object>
                            {
                                { "value", value },
                                { "detail",new Dictionary<string, object>
                                    {
                                        { "offsetCenter", new List<int> { 0, 0 } },
                                        { "fontSize", "2rem" },
                                        {"fontWeight", "normal" },
                                        { "color", "#888" }, 
                                        {"rich",new Dictionary<string, object>
                                            {
                                                {"valueStyle",new Dictionary<string, object>
                                                    {
                                                        { "fontSize", "2rem" },
                                                        { "color", "#888" }, 
                                                        { "fontWeight", "bold" }
                                                    }
                                                },
                                                {"textStyle",new Dictionary<string, object>
                                                    {
                                                        { "fontSize", "1.5rem" },
                                                        { "color", "#888" } 
                                                    }
                                                }
                                            }
                                        },
                                        { "formatter", "{valueStyle|{value}}/100\n{textStyle|completed}" }
                                    }
                                },
                                {"pointer",new Dictionary<string, object>
                                    {
                                        { "show", false }
                                    }
                                }
                            }
                        }
                    }
                }
            };
        dynamicObject2.Add("series", series);

        circleGaugeChart.InitialChart(dynamicObject2);
    }

    public async Task createArcGaugeChart()
    {
        var value = 68;

        var dynamicObject3 = new Dictionary<string, object>();

        var series = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    {"id", "1" },
                    {"type", "gauge" },
                    {"axisLine", new Dictionary<string, object>
                        {
                            {"show", true },
                            {"lineStyle", new Dictionary<string,object>
                                {
                                    {"width", 15 },
                                    {"color", new List<object> { new List<object> { 1, GaugeColors["neutral"] } } } 
                                }
                            }
                        }
                    },
                    {"axisTick", new Dictionary<string,object>
                        {
                            {"show", false }
                        }
                    },
                    {"radius", "100%" },
                    {"startAngle", 200 },
                    {"endAngle", -20 },
                    {"splitLine", new Dictionary<string,object>
                        {
                            {"show", false }
                        }
                    },
                    {"axisLabel", new Dictionary<string,object>
                        {
                            {"show", false}
                        }
                    },
                    {"progress", new Dictionary<string,object>
                        {
                            {"show", true},
                            {"overlap", false },
                            {"width", 35 },
                            {"itemStyle", new Dictionary<string,object>
                                {
                                    {"borderMiterLimit", 16 },
                                    {"color", GaugeColors["success"] }
                                }
                            }
                        }
                    },
                    {"pointer", new Dictionary<string,object>
                        {
                            {"show", false }
                        }
                    },
                    {"data", new List<Dictionary<string,object>>
                        {
                            new Dictionary<string, object>
                            {
                                {"value", value },
                                {"detail", new Dictionary<string,object>
                                    {
                                        { "offsetCenter", new List<int> { 0, 0 } },
                                        {"overflow", "break" },
                                        {"fontSize", "2rem" },
                                        {"fontWeight", "normal" },
                                        {"color", "#888" }, 
                                        {"width", 250 },
                                        {"lineHeight", 35 },
                                        {"formatter", "{value} / 100 \n completed" }
                                    }
                                },
                                {"pointer", new Dictionary<string,object>
                                    {
                                        {"show", false }
                                    }
                                }
                            }
                        }
                    }
                }
            };
        dynamicObject3.Add("series", series);

        arcGaugeChart.InitialChart(dynamicObject3);

    }
}
