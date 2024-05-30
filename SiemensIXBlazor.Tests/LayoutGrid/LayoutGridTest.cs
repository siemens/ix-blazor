// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Tests.LayoutGrid;

public class LayoutGridTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithParametersSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<Components.LayoutGrid.LayoutGrid>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content")))
            .Add(p => p.Columns, 12)
            .Add(p => p.Gap, "24")
            .Add(p => p.NoMargin, false));

        // Assert
        cut.MarkupMatches(
            "<ix-layout-grid columns=\"12\" gap=\"24\">Test content</ix-layout-grid>");
    }
}