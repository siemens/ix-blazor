// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.Charts.BarChart;

public partial class BarChart
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
        <div class=""barchart"">
            
            <SiemensIXBlazor.Components.ECharts.ECharts Id=""basic-bar-chart"" @ref=""basicbarchart"">

            </SiemensIXBlazor.Components.ECharts.ECharts>
        </div>";

    public string ContentForStackedBarChart { get; private set; } = @"
        <div class=""barchart"">
            <SiemensIXBlazor.Components.ECharts.ECharts Id=""stacked-bar-chart-item"" @ref=""stackedbarchart"">

            </SiemensIXBlazor.Components.ECharts.ECharts>
        </div>";


    SiemensIXBlazor.Components.ECharts.ECharts basicbarchart;
    SiemensIXBlazor.Components.ECharts.ECharts stackedbarchart;
    
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender && basicbarchart is not null && stackedbarchart is not null)
        {
            
            createBasicBarChart();
            createStackedBarChart();
        }
    }

    private List<string> products = new() { "Product A", "Product B", "Product C", "Product D", "Product E", "Product F" };
    private List<double> sales = new() { 10.3, 9.2, 7.3, 6.4, 6.2, 4.4 };
    public async Task createBasicBarChart()
    {
        var dynamicObject1 = new Dictionary<string, object>();

        var xAxis = new Dictionary<string, object>
            {

                    {"data", products },
                    {"name", "Product" },
                    {"nameLocation", "end" }

            };
        dynamicObject1.Add("xAxis", xAxis);

        var yAxis = new Dictionary<string, object>
            {
                    { "name", "Number of sold products \n (in Mio)" },
                    { "nameLocation", "end" }
            };
        dynamicObject1.Add("yAxis", yAxis);

        var legend = new Dictionary<string, object>
            {
                    { "show", true }
            };
        dynamicObject1.Add("legend", legend);

        var series = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "data", sales },
                    { "type", "bar" }
                }
            };
        dynamicObject1.Add("series", series);

        basicbarchart.InitialChart(dynamicObject1);
    }

    private List<string> years = new() { "2023", "2022", "2021", "2020", "2019" };
    private List<int> salesEurope = new() { 87, 22, 28, 43, 79 };
    private List<int> salesUS = new() { 35, 24, 33, 5, 40 };
    private List<int> salesChina = new() { 19, 44, 23, 5, 10 };
    public async Task createStackedBarChart()
    {
        var seriesData = new List<(string name, List<int> data)>
        {
            ("Europe", salesEurope),
            ("U.S", salesUS),
            ("China", salesChina)

        };

        var series = new List<Dictionary<string, object>>
        {
            new Dictionary<string, object>
            {
                {"name", "Europe" },
                {"data", salesEurope },
                {"type", "bar" },
                {"stack", "x" }
            },
            new Dictionary<string, object>
            {
                {"name", "U.S" },
                {"data", salesUS },
                {"type", "bar" },
                {"stack", "x" }
            },
            new Dictionary<string, object>
            {
                {"name", "China" },
                {"data", salesChina },
                {"type", "bar" },
                {"stack", "x" }
            }
        };
        var dynamicobject2 = new Dictionary<string, object>();

        var xAxis = new Dictionary<string, object>
          {
                  {"type", "value" },
                  {"name", "Revenue (in Millions of USD)" },
                  {"nameLocation", "middle" },
                  {"nameGap", 40 }
          };
        dynamicobject2.Add("xAxis", xAxis);

        var yAxis = new Dictionary<string, object>
          {
              {"type", "category" },
              {"data", years },
              {"name", "Years" },
              {"nameLocation", "end" }
          };
        dynamicobject2.Add("yAxis", yAxis);

        var legend = new Dictionary<string, object>
          {
              {"show", true },
              {"left", "0" },
              {"bottom", "0" }

          };
        dynamicobject2.Add("legend", legend);


        dynamicobject2.Add("series", series);

        stackedbarchart.InitialChart(dynamicobject2);

    }
}
