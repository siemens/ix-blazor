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
using SiemensIXBlazor.Enums.Tile;
using Xunit;

namespace SiemensIXBlazor.Tests
{
    public class TileTests : TestContextBase
    {
        [Fact]
        public void TileRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Tile>(
                ("Size", TileSize.Medium),
                ("ChildContent", (RenderFragment)(builder =>
                {
                    builder.AddMarkupContent(0, "<div>Test child content</div>");
                }))
            );

            // Assert
            cut.MarkupMatches("<ix-tile size=\"medium\"><div>Test child content</div></ix-tile>");
        }
    }
}