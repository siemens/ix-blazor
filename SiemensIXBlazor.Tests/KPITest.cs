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
            .Add(p => p.Value, "testValue"));

        // Assert
        cut.MarkupMatches("<ix-kpi label=\"testLabel\" value=\"testValue\" orientation=\"horizontal\" state=\"neutral\" onreadystatechange=\"Neutral\" unit=\"testUnit\"></ix-kpi>");
    }
}