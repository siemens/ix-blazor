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
using SiemensIXBlazor.Enums.LayoutGrid;
using SiemensIXBlazor.Components.LayoutGrid;

namespace SiemensIXBlazor.Tests.LayoutGrid;

public class ColTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithParametersSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<Col>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content")))
            .Add(p => p.Size, ColumnSize._12)
            .Add(p => p.SizeLg, ColumnSize._10)
            .Add(p => p.SizeMd, ColumnSize._8)
            .Add(p => p.SizeSm, ColumnSize._6));

        // Assert
        cut.MarkupMatches("<ix-col size=\"12\" size-lg=\"10\" size-md=\"8\" size-sm=\"6\">Test content</ix-col>");
    }

    [Fact]
    public void ComponentRendersWithoutSizeAttributesByDefault()
    {
        var cut = RenderComponent<Col>();

        cut.MarkupMatches("<ix-col></ix-col>");
    }

    [Fact]
    public void ComponentRendersAutoColumnSize()
    {
        var cut = RenderComponent<Col>(parameters => parameters
            .Add(p => p.Size, ColumnSize.auto));

        cut.MarkupMatches("<ix-col size=\"auto\"></ix-col>");
    }
}
