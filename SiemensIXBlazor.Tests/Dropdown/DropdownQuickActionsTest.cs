// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Components;

namespace SiemensIXBlazor.Tests.Dropdown;

public class DropdownQuickActionsTest : TestContextBase
{
    [Fact]
    public void RendersChildContent()
    {
        var cut = Render<DropdownQuickActions>(parameters => parameters
            .Add(p => p.ChildContent,
                (RenderFragment)(builder => builder.AddContent(0, "Quick action"))));

        cut.MarkupMatches("<ix-dropdown-quick-actions>Quick action</ix-dropdown-quick-actions>");
    }
}
