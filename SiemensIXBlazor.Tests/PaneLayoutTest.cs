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
using SiemensIXBlazor.Enums.Pane;
using Xunit;

namespace SiemensIXBlazor.Tests
{
    public class PaneLayoutTests : TestContext
    {
        [Fact]
        public void PaneLayoutRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<PaneLayout>(
                ("Borderless", true),
                ("Layout", "full-vertical"),
                ("Variant", PaneVariant.inline),
                ("ChildContent", (RenderFragment)(builder => builder.AddMarkupContent(0, "<div>Test content</div>")))
            );

            // Assert
            cut.MarkupMatches("<ix-pane-layout borderless layout=\"full-vertical\" variant=\"inline\"><div>Test content</div></ix-pane-layout>");
        }
    }
}