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
            var cut = RenderComponent<Pill>(parameters =>
            {
                parameters.Add(p => p.AlignLeft, true);
                parameters.Add(p => p.Background, "red");
                parameters.Add(p => p.PillColor, "white");
                parameters.Add(p => p.Icon, "testIcon");
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
            cut.MarkupMatches("<ix-pill align-left=\"\" background=\"red\" pill-color=\"white\" icon=\"testIcon\" outline=\"\" variant=\"primary\"><div>Test child content</div></ix-pill>");
        }

        [Fact]
        public void PillRendersWithDefaultParameters()
        {
            // Act
            var cut = RenderComponent<Pill>();

            // Assert
            cut.MarkupMatches("<ix-pill variant=\"primary\"></ix-pill>");
        }

        [Fact]
        public void PillRendersChildContentOnly()
        {
            // Arrange
            var content = "Simple Text";

            // Act
            var cut = RenderComponent<Pill>(parameters =>
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
            var cut = RenderComponent<Pill>(parameters =>
            {
                parameters.Add(p => p.AlignLeft, true);
                parameters.Add(p => p.Outline, true);
            });

            // Assert
            var element = cut.Find("ix-pill");
            Assert.True(element.HasAttribute("align-left"));
            Assert.True(element.HasAttribute("outline"));
        }
    }
}
