// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.KPI;

namespace SiemensIXBlazor.Tests;

public class KPITest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithParametersSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<KPI>(parameters => parameters
            .Add(p => p.Label, "testLabel")
            .Add(p => p.Orientation, KpiOrientation.Horizontal)
            .Add(p => p.State, KpiState.Neutral)
            .Add(p => p.Unit, "testUnit")
            .Add(p => p.Value, "testValue")
            .Add(p => p.AriaLabelWarningIcon, "Warning status"));

        // Assert
        cut.MarkupMatches("<ix-kpi label=\"testLabel\" value=\"testValue\" aria-label-warning-icon=\"Warning status\" orientation=\"horizontal\" state=\"neutral\" unit=\"testUnit\"></ix-kpi>");

    }

    [Fact]
    public void ComponentRendersNumericValueWithoutUnsupportedAttributes()
    {
        var cut = RenderComponent<KPI>(parameters => parameters
            .Add(p => p.Label, "Temperature")
            .Add(p => p.Value, 42)
            .Add(p => p.State, KpiState.Warning)
            .Add(p => p.AriaLabelWarningIcon, "Warning status"));

        Assert.Equal(42, cut.Instance.Value);
        Assert.Contains("value=\"42\"", cut.Markup);
        Assert.Contains("aria-label-warning-icon=\"Warning status\"", cut.Markup);
        Assert.DoesNotContain("onreadystatechange", cut.Markup);
    }
}
