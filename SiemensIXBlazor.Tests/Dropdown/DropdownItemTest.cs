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
using SiemensIXBlazor.Enums.Dropdown;

namespace SiemensIXBlazor.Tests.Dropdown;

public class DropdownItemTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithCorrectProperties()
    {
        // Arrange
        var cut = RenderComponent<DropdownItem>(parameters => parameters
            .Add(p => p.Label, "testLabel")
            .Add(p => p.Icon, "test-icon")
            .Add(p => p.Checked, true)
            .Add(p => p.Disabled, true)
            .Add(p => p.Hover, true)
            .Add(p => p.ItemRole, DropdownItemRole.option));

        // Assert
        cut.MarkupMatches("<ix-dropdown-item label=\"testLabel\" icon=\"test-icon\" checked=\"\" disabled=\"\" hover=\"\" item-role=\"option\"></ix-dropdown-item>");
    }
}
