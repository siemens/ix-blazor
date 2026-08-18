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
using SiemensIXBlazor.Enums.Pill;

namespace SiemensIXBlazor.Tests
{
    public class PillTests : TestContextBase
    {
        [Fact]
        public void PillRendersWithAllParameters()
        {
            // Arrange & Act
            var cut = Render<Pill>(parameters =>
            {
                parameters.Add(p => p.AlignLeft, true);
                parameters.Add(p => p.Background, "red");
                parameters.Add(p => p.PillColor, "white");
                parameters.Add(p => p.Icon, "testIcon");
                parameters.Add(p => p.TooltipText, "tooltipText");
                parameters.Add(p => p.Outline, true);
                parameters.Add(p => p.Variant, PillVariant.primary);
                parameters.Add(p => p.ChildContent, builder =>
                {
                    builder.OpenElement(0, "div");
                    builder.AddContent(1, "Test child content");
                    builder.CloseElement();
                });
            });

            // Assert
            cut.MarkupMatches("<ix-pill align-left=\"true\" background=\"red\" pill-color=\"white\" icon=\"testIcon\" outline=\"true\" variant=\"primary\" tooltip-text='tooltipText'><div>Test child content</div></ix-pill>");
        }

        [Fact]
        public void PillRendersWithDefaultParameters()
        {
            // Act
            var cut = Render<Pill>((Action<Bunit.ComponentParameterCollectionBuilder<Pill>>)(_ => { }));

            // Assert
            cut.MarkupMatches("<ix-pill variant=\"primary\"></ix-pill>");
        }

        [Fact]
        public void PillRendersChildContentOnly()
        {
            // Arrange
            var content = "Simple Text";

            // Act
            var cut = Render<Pill>(parameters =>
            {
                parameters.Add(p => p.ChildContent, (RenderFragment)(builder =>
                {
                    builder.AddContent(0, content);
                }));
            });

            // Assert
            Assert.Contains(content, cut.Markup);
        }

        [Fact]
        public void PillHasBooleanAttributes()
        {
            // Act
            var cut = Render<Pill>(parameters =>
            {
                parameters.Add(p => p.AlignLeft, true);
                parameters.Add(p => p.Outline, true);
            });

            // Assert
            var element = cut.Find("ix-pill");
            Assert.True(element.HasAttribute("align-left"));
            Assert.True(element.HasAttribute("outline"));
        }

        [Fact]
        public void PillTooltipTrueUsesPresenceOnlyAttribute()
        {
            var cut = Render<Pill>(parameters => parameters
                .Add(p => p.TooltipText, true));

            var element = cut.Find("ix-pill");
            Assert.True(element.HasAttribute("tooltip-text"));
            Assert.Equal(string.Empty, element.GetAttribute("tooltip-text"));
        }

        [Fact]
        public void PillTooltipFalseOmitsAttribute()
        {
            var cut = Render<Pill>(parameters => parameters
                .Add(p => p.TooltipText, false));

            Assert.False(cut.Find("ix-pill").HasAttribute("tooltip-text"));
        }
    }
}
