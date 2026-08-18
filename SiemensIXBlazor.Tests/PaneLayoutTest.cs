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
    public class PaneLayoutTests : TestContextBase
    {
        [Fact]
        public void PaneLayoutRendersCorrectly()
        {
            // Arrange
            var cut = Render<PaneLayout>(parameters => parameters
                .Add(p => p.Borderless, true)
                .Add(p => p.Layout, "full-vertical")
                .Add(p => p.Variant, PaneVariant.inline)
                .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "<div>Test content</div>")))
            );

            // Assert
            cut.MarkupMatches("<ix-pane-layout borderless='true' layout=\"full-vertical\" variant=\"inline\"><div>Test content</div></ix-pane-layout>");
        }

        [Fact]
        public void PaneLayoutRendersNamedSlots()
        {
            var cut = Render<PaneLayout>(parameters => parameters
                .Add(p => p.Left, builder => builder.AddContent(0, "Left content"))
                .Add(p => p.Top, builder => builder.AddContent(0, "Top content"))
                .Add(p => p.Content, builder => builder.AddContent(0, "Content area"))
                .Add(p => p.Bottom, builder => builder.AddContent(0, "Bottom content"))
                .Add(p => p.Right, builder => builder.AddContent(0, "Right content")));

            Assert.Equal("Left content", cut.Find("[slot='left']").TextContent.Trim());
            Assert.Equal("Top content", cut.Find("[slot='top']").TextContent.Trim());
            Assert.Equal("Content area", cut.Find("[slot='content']").TextContent.Trim());
            Assert.Equal("Bottom content", cut.Find("[slot='bottom']").TextContent.Trim());
            Assert.Equal("Right content", cut.Find("[slot='right']").TextContent.Trim());
        }
    }
}
