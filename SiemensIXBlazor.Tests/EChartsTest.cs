// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using SiemensIXBlazor.Components.ECharts;

namespace SiemensIXBlazor.Tests;

public class EChartsTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithCorrectProperties()
    {
        // Arrange
        var cut = RenderComponent<ECharts>(parameters => parameters
            .Add(p => p.Id, "testId"));

        // Assert
        cut.MarkupMatches("<div id=\"testId\" style=\"display: block; position: relative; width: 100%; height: 40rem\"></div>");
    }
}