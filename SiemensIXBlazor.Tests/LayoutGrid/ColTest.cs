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

public class ColTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithParametersSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<Col>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content")))
            .Add(p => p.Size, "12")
            .Add(p => p.SizeLg, "10")
            .Add(p => p.SizeMd, "8")
            .Add(p => p.SizeSm, "6"));

        // Assert
        cut.MarkupMatches("<ix-col size=\"12\" size-lg=\"10\" size-md=\"8\" size-sm=\"6\">Test content</ix-col>");
    }
}