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
using SiemensIXBlazor.Components.LayoutGrid;

namespace SiemensIXBlazor.Tests.LayoutGrid;

public class RowTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithChildContent()
    {
        // Arrange
        var cut = RenderComponent<Row>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content"))));

        // Assert
        cut.MarkupMatches("<ix-row>Test content</ix-row>");
    }
}