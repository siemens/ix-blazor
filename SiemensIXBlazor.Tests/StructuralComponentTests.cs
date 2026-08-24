// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using Bunit;

namespace SiemensIXBlazor.Tests;

public class StructuralComponentTests : TestContextBase
{
    [Fact]
    public void CardContentRendersDefaultContent()
    {
        var cut = Render<Components.CardContent>(parameters => parameters
            .AddChildContent("Card content"));

        Assert.Equal("Card content", cut.Find("ix-card-content").TextContent);
    }

    [Fact]
    public void GroupContextMenuRendersDefaultContent()
    {
        var cut = Render<Components.GroupContextMenu>(parameters => parameters
            .AddChildContent("Group action"));

        Assert.Equal("Group action", cut.Find("ix-group-context-menu").TextContent);
    }
}
