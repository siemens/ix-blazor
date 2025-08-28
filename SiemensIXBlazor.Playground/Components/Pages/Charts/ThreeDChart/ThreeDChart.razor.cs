// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.Charts.ThreeDChart;

public partial class ThreeDChart
{
    private int activeTab = 0;
    public string ContentForBasic { get; private set; } = @"
        <div class=""tree-d-chart"">
            <SiemensIXBlazor.Components.ECharts.ECharts Id=""basic-tree-d-chart"" @ref=""treeDChart"">

            </SiemensIXBlazor.Components.ECharts.ECharts>
        </div>";

    SiemensIXBlazor.Components.ECharts.ECharts treeDChart;

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender && treeDChart is not null)
        {
            createTreeDChart();
        }
    }


    private Dictionary<string,object> gridConfig()
    {
        return new Dictionary<string, object>
        {
            {"type", "value" },
            {"axisLine", new Dictionary<string,object>
                {
                    {"lineStyle", new Dictionary<string,object>
                        {
                            {"color", "#ccc" } 
                        }
                    }
                }
            },
            {"splitLine",  new Dictionary<string,object>
                {
                    {"lineStyle", new Dictionary<string,object>
                        {
                            {"color", "#eee" }
                        }
                    }
                }
            },
            {"axisLabel",  new Dictionary<string,object>
                {
                    {"color", "#888" }
                }
            }
        };
    }

    public async Task createTreeDChart()
    {
        double xMin = -1, xMax = 1, xStep = 0.05; 
        double yMin = -1, yMax = 1, yStep = 0.05;
        var data = new List<List<double?>>();

        for (double x = xMin; x <= xMax; x += xStep) 
        {
            for (double y = yMin; y <= yMax; y += yStep)
            {
                double? z;
                if (Math.Abs(x) < 0.1 && Math.Abs(y) < 0.1)
                    z = null;
                else
                    z = Math.Sin(x * Math.PI) * Math.Sin(y * Math.PI);

                data.Add(new List<double?> { x, y, z });
            }
        }

        if (treeDChart is not null)
        {

            var dynamicObject = new Dictionary<string, object>();

            var tooltip = new Dictionary<string, object>();
            dynamicObject.Add("tooltip", tooltip);

            var visualMap = new Dictionary<string, object>
            {
                {"show",false },
                {"dimension", 2 },
                {"min", -1 },
                {"max", 1 }
            };
            dynamicObject.Add("visualMap",visualMap);

            dynamicObject.Add("xAxis3D", gridConfig());
            dynamicObject.Add("yAxis3D", gridConfig());
            dynamicObject.Add("zAxis3D", gridConfig());

            var grid3D = new Dictionary<string, object>
            {
                {"viewControl", new Dictionary<string, object>
                    {
                        {"projection", "orthographic" }
                    }
                }
            };
            dynamicObject.Add("grid3D", grid3D);

            var series = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    {"type", "surface" },
                    {"data", data }
                }
            };
            dynamicObject.Add("series", series);

            treeDChart.InitialChart(dynamicObject);
        }
    }
}
