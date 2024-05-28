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
using SiemensIXBlazor.Components;

namespace SiemensIXBlazor.Tests.Flip;

public class FlipTileTest : TestContextBase
{
    [Fact]
    public void ComponentRendersAndSetsPropertiesCorrectly()
    {
        // Arrange
        var cut = RenderComponent<FlipTile>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content")))
            .Add(p => p.State, "testState")
            .Add(p => p.Height, 20.5)
            .Add(p => p.Width, 25));

        // Assert
        cut.MarkupMatches("<ix-flip-tile state=\"testState\" height=\"20.5\" width=\"25\">Test content</ix-flip-tile>");
    }
}