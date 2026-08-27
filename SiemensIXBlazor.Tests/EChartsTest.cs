// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using SiemensIXBlazor.Components.ECharts;

namespace SiemensIXBlazor.Tests;

public class EChartsTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithCorrectProperties()
    {
        // Arrange
        var cut = Render<ECharts>(parameters => parameters
            .Add(p => p.Id, "testId"));

        // Assert
        cut.MarkupMatches("<div id=\"testId\" style=\"display: block; position: relative; width: 100%; height: 40rem\"></div>");
    }

    [Fact]
    public async Task InitialChartSerializesOptionsAndDisposesInitializedChart()
    {
        var jsRuntime = new Mock<IJSRuntime>();
        Services.AddSingleton<IJSRuntime>(jsRuntime.Object);
        var cut = Render<ECharts>(parameters => parameters.Add(p => p.Id, "chart"));

        await cut.Instance.InitialChart(new
        {
            title = new { text = "Load" },
            values = new[] { 1, 2, 3 },
        });
        await cut.Instance.DisposeAsync();

        Assert.Contains(jsRuntime.Invocations, invocation =>
            invocation.Method.Name == nameof(IJSRuntime.InvokeAsync) &&
            invocation.Arguments.Count == 2 &&
            (string)invocation.Arguments[0] == "siemensIXInterop.initializeChart" &&
            invocation.Arguments[1] is object[] arguments &&
            arguments.Length == 2 &&
            (string)arguments[0] == "chart" &&
            ((string)arguments[1]).Contains("\"text\":\"Load\""));
        Assert.Contains(jsRuntime.Invocations, invocation =>
            invocation.Method.Name == nameof(IJSRuntime.InvokeAsync) &&
            invocation.Arguments.Count == 2 &&
            (string)invocation.Arguments[0] == "siemensIXInterop.disposeChart" &&
            invocation.Arguments[1] is object[] arguments &&
            arguments.Length == 1 &&
            (string)arguments[0] == "chart");
    }

    [Fact]
    public async Task DisposeBeforeInitializationDoesNotCallJavaScript()
    {
        var jsRuntime = new Mock<IJSRuntime>();
        Services.AddSingleton<IJSRuntime>(jsRuntime.Object);
        var cut = Render<ECharts>(parameters => parameters.Add(p => p.Id, "chart"));

        await cut.Instance.DisposeAsync();

        Assert.DoesNotContain(jsRuntime.Invocations, invocation =>
            invocation.Method.Name == nameof(IJSRuntime.InvokeAsync) &&
            invocation.Arguments.Count > 0 &&
            (string)invocation.Arguments[0] == "siemensIXInterop.disposeChart");
    }
}
