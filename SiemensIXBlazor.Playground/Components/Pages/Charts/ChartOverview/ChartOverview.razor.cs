// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using System.Dynamic;

namespace SiemensIXBlazor.Playground.Components.Pages.Charts.ChartOverview;

public partial class ChartOverview
{
    private int activeTab = 0;

    public string ContentForColors { get; private set; } = @"
        <div class=""overview-charts"">
            <SiemensIXBlazor.Components.ECharts.ECharts Id=""color-chart"" @ref=""colorchart"">

            </SiemensIXBlazor.Components.ECharts.ECharts>
        </div>";

    public string ContentForEmptyStates { get; private set; } = @"
        <div class=""my-chart-container"">

            @if (isLoading)
            {
                <div class=""component-loading-overlay"">
                    <div class=""loading-text"">
                        <span class=""spinner""></span>
                        Loading...
                    </div>
                </div>
            }

            @if (showOverlay)
            {
                <div id=""empty-state-container"" class=""empty-state-container"">
                    <SiemensIXBlazor.Components.EmptyState Id=""empty-state""
                                                           Class=""empty-state""
                                                           Header=""No elements available""
                                                           SubHeader=""Failed to retrieve data""
                                                           Icon=""info""
                                                           Action=""Try Again""
                                                           ActionClickedEvent=""LoadData""/>
                </div>
            }

            <SiemensIXBlazor.Components.ECharts.ECharts Id=""empty-states-chart"" @ref=""emptyStateChart"" />
        </div>";

    SiemensIXBlazor.Components.ECharts.ECharts colorchart;
    SiemensIXBlazor.Components.ECharts.ECharts emptyStateChart;

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender && colorchart is not null && emptyStateChart is not null)
        {
            createColorChart();
            createEmptyStateChart();
        }
    }

    public async Task createColorChart()
    {

        var dynamicObject1 = new Dictionary<string, object>();

        var tooltip = new Dictionary<string, object>
            {
                {"trigger", "axis" },
                {"axisPointer", new Dictionary<string, object>
                    {
                        {"type","shadow" }
                    }
                }
            };
        dynamicObject1.Add("tooltip", tooltip);

        var legend = new Dictionary<string, object>
            {
                {"icon", "rect" },
                {"bottom", 0 },
                {"left", 0 }
            };
        dynamicObject1.Add("legend", legend);

        var grid = new Dictionary<string, object>
            {
                {"left", "3%"},
                {"right", "4%" },
                {"bottom", "7%" },
                {"containLabel", true }
            };
        dynamicObject1.Add("grid", grid);

        var xAxis = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    {"type", "category" },
                    {"data", new List<string> {"Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"} }
                }
            };
        dynamicObject1.Add("xAxis", xAxis);

        var yAxis = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    {"type", "value" }
                }
            };
        dynamicObject1.Add("yAxis", yAxis);

        var series = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    {"name", "Direct" },
                    {"type", "bar" },
                    {"emphasis", new Dictionary<string, object>
                    {
                        {"focus", "series" }
                    }
                    },
                    {"data", new List<int> { 320, 332, 301, 334, 390, 330, 320 } }
                },

                new Dictionary<string, object>
                {
                    {"name", "Email" },
                    {"type", "bar" },
                    {"stack", "Ad" },
                    {"emphasis", new Dictionary<string, object>
                    {
                        {"focus", "series" }
                    }
                    },
                    {"data", new List<int>{ 120, 132, 101, 134, 90, 230, 210 } }
                },

                new Dictionary<string, object>
                {
                    {"name", "Union Ads" },
                    {"type", "bar" },
                    {"stack", "Ad" },
                    {"emphasis", new Dictionary<string, object>
                    {
                        {"focus", "series" }
                    }
                    },
                    {"data", new List<int>{ 220, 182, 191, 234, 290, 330, 310 } }
                },

                new Dictionary<string, object>
                {
                    {"name", "Video Ads" },
                    {"type", "bar" },
                    {"stack", "Ad" },
                    {"emphasis", new Dictionary<string, object>
                    {
                        {"focus", "series" }
                    }
                    },
                    {"data", new List<int>{ 150, 232, 201, 154, 190, 330, 410 } }
                },

                new Dictionary<string, object>
                {
                    {"name", "Search Engine" },
                    {"type", "bar" },
                    {"data", new List<int>{ 862, 1018, 964, 1026, 1679, 1600, 1570 } },
                    {"emphasis", new Dictionary<string, object>
                        {
                            {"focus", "series" }
                        }
                    },
                    {"markLine", new Dictionary<string, object>
                        {
                            {"lineStyle", new Dictionary<string,object>
                                {
                                    {"type" , "dashed"}
                                }
                            },
                            {"data", new List<List<Dictionary<string, object>>>
                                {
                                    new List<Dictionary<string,object>>
                                    {
                                        new Dictionary<string,object>
                                        {
                                            {"type", "min"}
                                        },
                                        new Dictionary<string, object>
                                        {
                                            {"type", "max"}
                                        }
                                    }
                                }
                            }
                        }
                    }
                },

                new Dictionary<string, object>
                {
                    {"name", "Baidu" },
                    {"type", "bar" },
                    {"barWidth", 5 },
                    {"stack", "Search Engine" },
                    {"emphasis", new Dictionary<string,object>
                    {
                        {"focus", "series" }
                    }
                    },
                    {"data", new List<int>{ 620, 732, 701, 734, 1090, 1130, 1120 } }
                },

                new Dictionary<string, object>
                {
                    {"name", "Google" },
                    {"type", "bar" },
                    {"stack", "Search Engine" },
                    {"emphasis", new Dictionary<string,object>
                    {
                        {"focus", "series" }
                    }
                    },
                    {"data", new List<int>{ 120, 132, 101, 134, 290, 230, 220 } }
                },

                new Dictionary<string, object>
                {
                    {"name", "Bing" },
                    {"type", "bar" },
                    {"stack", "Search Engine" },
                    {"emphasis", new Dictionary<string,object>
                    {
                        {"focus", "series" }
                    }
                    },
                    {"data", new List<int>{ 60, 72, 71, 74, 190, 130, 110 } }
                },

                new Dictionary<string, object>
                {
                    {"name", "Others" },
                    {"type", "bar" },
                    {"stack", "Search Engine" },
                    {"emphasis", new Dictionary<string,object>
                    {
                        {"focus", "series" }
                    }
                    },
                    {"data", new List<int>{ 62, 82, 91, 84, 109, 110, 120 } }
                },
            };
        dynamicObject1.Add("series", series);

        colorchart.InitialChart(dynamicObject1);

    }

    bool showOverlay = true;
    bool isLoading = false;
    async Task LoadData()
    {
        isLoading = true;
        StateHasChanged();
        await Task.Delay(2000);
        isLoading = false;
        StateHasChanged();
    }

    void ToggleOverlay()
    {
        showOverlay = !showOverlay;
    }


    private List<string> weekdays = new() { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
    private List<double> value = new List<double>();
    public async Task createEmptyStateChart()
    {

        var dynamicObject2 = new Dictionary<string, object>();
        var xAxis = new Dictionary<string, object>
        {
            {"type", "category" },
            {"data", weekdays}
        };
        dynamicObject2.Add("xAxis", xAxis);

        var yAxis = new Dictionary<string, object>
            {
                {"type", "value" }
            };
        dynamicObject2.Add("yAxis", yAxis);

        var series = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    {"data", value },
                    {"type", "line" }
                }
            };
        dynamicObject2.Add("series", series);

        var grid = new Dictionary<string, object>
            {
                {"left", "3%"},
                {"right", "4%" },
                {"bottom", "7%" },
                {"containLabel", true }
            };
        dynamicObject2.Add("grid", grid);

        emptyStateChart.InitialChart(dynamicObject2);
    }
}
