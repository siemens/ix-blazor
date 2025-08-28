// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using System.Dynamic;
using System.Runtime.CompilerServices;

namespace SiemensIXBlazor.Playground.Components.Pages.Charts.LineChart;

public partial class LineChart
{
    private int activeTab = 0;
    public string ContentForBasic { get; private set; } = @"
        <div class=""echarts"">
            <SiemensIXBlazor.Components.ECharts.ECharts Id=""basic-chart"" @ref=""chart1"">

            </SiemensIXBlazor.Components.ECharts.ECharts>
        </div>";
    public string ContentForMultiYAxis { get; private set; } = @"
        <div class=""echarts"">
            <SiemensIXBlazor.Components.ECharts.ECharts Id=""multi-y-axis-chart"" @ref=""chart2"">

            </SiemensIXBlazor.Components.ECharts.ECharts>
        </div>";
    public string ContentForAdvanced { get; private set; } = @"
        <div class=""echarts"">
            <SiemensIXBlazor.Components.ECharts.ECharts Id=""advanced-chart"" @ref=""chart3"">

            </SiemensIXBlazor.Components.ECharts.ECharts>
        </div>";

    SiemensIXBlazor.Components.ECharts.ECharts chart1;
    SiemensIXBlazor.Components.ECharts.ECharts chart2;
    SiemensIXBlazor.Components.ECharts.ECharts chart3;

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender && chart1 is not null && chart2 is not null && chart3 is not null)
        {
            createBasicLineChart();
            createMultiYLineChart();
            createAdvancedLine();
        }
    }

    private List<string> weekdays = new() { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
    private List<int> values = new() { 150, 230, 224, 218, 135, 147, 260 };
    public async Task createBasicLineChart()
    {

        var dynamicObject1 = new Dictionary<string, object>();

        var xAxis = new Dictionary<string, object>
            {
                    { "type", "category" },
                    { "data", weekdays }
            };

        dynamicObject1.Add("xAxis", xAxis);

        var yAxis = new Dictionary<string, object>
            {
                 { "type", "value" }
            };
        dynamicObject1.Add("yAxis", yAxis);

        var series = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "data", values },
                    { "type", "line" }
                }
            };
        dynamicObject1.Add("series", series);

        var grid = new Dictionary<string, object>
            {
                { "left", "3%" },
                { "right", "4%" },
                { "bottom", "3%" },
                { "containLabel", true }
            };
        dynamicObject1.Add("grid", grid);


        chart1.InitialChart(dynamicObject1);

    }

    private List<string> months = new() { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

    private List<double> evaporation = new();
    private List<double> precipitation = new();
    private List<double> temperature = new();
    private List<string> themeChartList = new()
    {
        "#5470C6", "#91CC75", "#FAC858", "#EE6666", "#73C0DE",
        "#3BA272", "#FC8452", "#9A60B4", "#EA7CCC", "#dedede",
        "#A8A878", "#FFE793", "#FF93B9", "#B4A7D6", "#A2C4C9",
        "#76A5AF", "#6D9EEB"
    };

    protected override void OnInitialized()
    {
        var rand = new Random();
        for (int i = 0; i < months.Count; i++)
        {
            evaporation.Add(Math.Round(rand.NextDouble() * 100, 2));
            precipitation.Add(Math.Round(rand.NextDouble() * 200, 2));
            temperature.Add(Math.Round(rand.NextDouble() * 30, 2));
        }
    }
    public async Task createMultiYLineChart()
    {
        Dictionary<string, object> createYAxis(string name, string position, string color, string formatter, int offset = 0)
        {
            return new Dictionary<string, object>
            {
                    { "type", "value" },
                    { "name", name },
                    { "position", position },
                    { "offset", offset },
                    { "alignTicks", true },
                    { "axisLabel", new Dictionary<string, object>
                        {
                            { "formatter", formatter }
                        }
                    },
                    { "axisTick", new Dictionary<string, object>
                        {
                            { "lineStyle", new Dictionary<string, object>
                                {
                                    { "color", color }
                                }
                            }
                        }
                    },
                    { "axisLine", new Dictionary<string, object>
                        {
                            { "lineStyle", new Dictionary<string, object>
                                {
                                    { "color", color }
                                }
                            }
                        }
                    },
            };
        }

        Dictionary<string, object> createSeries(string name, int yAxisIndex, IEnumerable<double> data, string color)
        {
            return new Dictionary<string, object>
                {
                    { "name", name },
                    { "type", "line" },
                    { "yAxisIndex", yAxisIndex },
                    { "data", data },
                    { "lineStyle", new Dictionary<string, object>
                        {
                            { "color", color }
                        }
                    },
                    { "itemStyle", new Dictionary<string, object>
                        {
                            { "color", color }
                        }
                    }
                };
        }

        var options = new Dictionary<string, object>
            {

                {"tooltip", new Dictionary<string, object>
                    {
                        { "trigger", "axis" },
                        { "axisPointer", new Dictionary<string, object>
                            {
                                { "type", "cross" }
                            }
                        }
                    }
                },

                {"grid", new Dictionary<string, object>
                    {
                        { "right", "20%" }
                    }
                },

                {"legend", new Dictionary<string, object>
                    {
                        { "show", true },
                        { "bottom", "0" },
                        { "left", "0" }
                    }
                },

                {"xAxis", new List<Dictionary<string, object>>
                    {
                        new Dictionary<string, object>
                        {
                            { "type", "category" },
                            { "axisTick", new Dictionary<string, object>
                                {
                                    { "alignWithLabel", true }
                                }
                            },
                            { "data", months }
                        }
                    }
                },

                {"yAxis", new List<Dictionary<string, object>>{
                        createYAxis("Evaporation", "right", themeChartList[0], "{value} ml"),
                        createYAxis("Precipitation", "right", themeChartList[7], "{value} ml", 80),
                        createYAxis("Temperature", "left", themeChartList[12], "{value} °C")
                    }
                },

                {"series", new List<Dictionary<string, object>>
                    {
                        createSeries("Evaporation", 0, evaporation, themeChartList[0]),
                        createSeries("Precipitation", 1, precipitation, themeChartList[7]),
                        createSeries("Temperature", 2, temperature, themeChartList[12])
                    }
                }
            };

        chart2.InitialChart(options);

    }

    private List<double> stockData = new() { 77.67, 82.81, 84.09, 91.75, 118.15, 107.48, 99.36, 93.07, 137.18, 104.38, 156.48, 168.52 };
    private List<string> dates = Enumerable.Range(2013, 2025 - 2013).Select(year => year.ToString()).ToList();

    public async Task createAdvancedLine()
    {
        var dynamicObjects3 = new Dictionary<string, object>();

        var tooltip = new Dictionary<string, object>
            {
                { "trigger", "axis" }

            };
        dynamicObjects3.Add("tooltip", tooltip);

        var xAxis = new Dictionary<string, object>
            {
                    { "type", "category" },
                    { "data", dates }
            };
        dynamicObjects3.Add("xAxis", xAxis);

        var yAxis = new Dictionary<string, object>
            {
                    {"type","value"},
                    { "splitLine", new Dictionary<string, object> 
                        { 
                            { "lineStyle", new Dictionary<string, object>
                                {
                                    {"type","dashed" }
                                }
                            } 
                        } 
                    }
            };
        dynamicObjects3.Add("yAxis", yAxis);

        var areaStyle = new Dictionary<string, object>
            {
                { "color", new Dictionary<string, object>
                    {
                        { "type", "linear" },
                        { "x", 0 },
                        { "y", 0 },
                        { "x2", 0 },
                        { "y2", 1 },
                        { "colorStops", new List<Dictionary<string, object>>
                            {
                                new Dictionary<string, object>
                                {
                                    { "offset", 0 },
                                    { "color", "#3a8ee6" } 
                                },
                                new Dictionary<string, object>
                                {
                                    { "offset", 1 },
                                    { "color", "transparent" }
                                }
                            }
                        }
                    }
                }
            };
        var markPoint = new Dictionary<string, object>
            {
                { "data", new List<Dictionary<string, object>>
                    {
                        new Dictionary<string, object>
                        {
                            { "type", "max" },
                            { "name", "Max" },
                            { "symbol", "circle" },
                            { "symbolSize", 60 }
                        },
                        new Dictionary<string, object>
                        {
                            { "type", "min" },
                            { "name", "Min" },
                            { "symbol", "circle" },
                            { "symbolSize", 60 }
                        }
                    }
                },
                { "label", new Dictionary<string, object>
                    {
                        { "fontWeight", "bold" },
                        { "color", "#fff" } 
                    }
                }
            };
        var series = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    {"type", "line"},
                    {"data", stockData},
                    {"smooth", true },
                    {"lineStyle", new Dictionary<string, object>
                        {
                            {"width", 4 },
                            {"shadowBlur",10 }
                        }
                    },
                    {"areaStyle", areaStyle},
                    {"markPoint",  new Dictionary<string, object>
                        {
                            { "data", new List<Dictionary<string, object>>
                                {
                                    new Dictionary<string, object>
                                    {
                                        { "type", "max" },
                                        { "name", "Max" },
                                        { "symbol", "circle" },
                                        { "symbolSize", 60 }
                                    },
                                    new Dictionary<string, object>
                                    {
                                        { "type", "min" },
                                        { "name", "Min" },
                                        { "symbol", "circle" },
                                        { "symbolSize", 60 }
                                    }
                                }
                            },
                            { "label", new Dictionary<string, object>
                                {
                                    { "fontWeight", "bold" },
                                    { "color", "#fff" } 
                                }
                            }
                        }
                    },
                    {"markLine", new Dictionary<string,object>
                        {
                            {"data", new List<Dictionary<string,object>>
                                {
                                    new Dictionary<string, object>
                                    {
                                        {"type", "average" },
                                        {"name", "Avg" }
                                    }
                                }
                            },
                            {"lineStyle", new Dictionary<string,object>
                                {
                                    {"type", "dashed" }
                                }
                            }
                        }
                    }
                }
            };
        dynamicObjects3.Add("series", series);
        chart3.InitialChart(dynamicObjects3);

    }
}

