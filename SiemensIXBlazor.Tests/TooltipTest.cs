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
using SiemensIXBlazor.Enums.Tooltip;
using Xunit;

namespace SiemensIXBlazor.Tests
{
    public class TooltipTests : TestContextBase
    {
        [Fact]
        public void TooltipRendersCorrectly()
        {
            // Arrange
            var cut = Render<Tooltip>(parameters => parameters
                .Add(p => p.Id, "tooltipId")
                .Add(p => p.TitleContent, "Test Tooltip")
                .Add(p => p.Interactive, true)
                .Add(p => p.Placement, TooltipVariant.bottom)
                .Add(p => p.For, "testElement")
            );

            // Assert
            cut.MarkupMatches("<ix-tooltip id=\"tooltipId\" title-content=\"Test Tooltip\" interactive='true' placement=\"bottom\" for=\"testElement\"></ix-tooltip>");
        }


    }
}
