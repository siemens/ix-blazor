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

namespace SiemensIXBlazor.Tests;

public class DividerTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithCorrectProperties()
    {
        // Arrange
        var cut = RenderComponent<Divider>(parameters => parameters
            .Add(p => p.Class, "testClass")
            .Add(p => p.Style, "testStyle"));

        // Assert
        cut.MarkupMatches("<ix-divider class=\"testClass\" style=\"testStyle\"></ix-divider>");
    }
}