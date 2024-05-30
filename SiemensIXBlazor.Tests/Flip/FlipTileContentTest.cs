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

public class FlipTileContentTest : TestContextBase
{
    [Fact]
    public void ComponentRendersAndPropertiesSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<FlipTileContent>(parameters => parameters.Add(p => p.ChildContent,
            (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content"))));

        // Assert
        cut.MarkupMatches("<ix-flip-tile-content>Test content</ix-flip-tile-content>");
    }
}