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

        [Fact]
        public void TooltipRendersTitleSlots()
        {
            var cut = Render<Tooltip>(parameters => parameters
                .Add(p => p.Id, "tooltipId")
                .Add(p => p.TitleIconContent, (RenderFragment)(builder => builder.AddContent(0, "Icon")))
                .Add(p => p.TitleContentSlot, (RenderFragment)(builder => builder.AddContent(0, "Title"))));

            Assert.Equal("Icon", cut.Find("[slot='title-icon']").TextContent);
            Assert.Equal("Title", cut.Find("[slot='title-content']").TextContent);
        }

        [Fact]
        public void TooltipAcceptsElementBackedTargets()
        {
            var cut = Render<Tooltip>(parameters => parameters
                .Add(p => p.Id, "tooltipId")
                .Add(p => p.For, new object()));

            Assert.False(cut.Find("ix-tooltip").HasAttribute("for"));
        }


    }
}
