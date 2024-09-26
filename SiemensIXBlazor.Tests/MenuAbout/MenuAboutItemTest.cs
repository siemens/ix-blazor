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
using SiemensIXBlazor.Components.MenuAbout;

namespace SiemensIXBlazor.Tests.MenuAbout
{
	public class MenuAboutItemTest: TestContextBase
    {
        [Fact]
        public void ComponentRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<MenuAboutItem>();

            // Assert
            cut.MarkupMatches("<ix-menu-about-item></ix-menu-about-item>");
        }

        [Fact]
        public void ChildContentPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<MenuAboutItem>(parameters => parameters.Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content"))));

            // Assert
            Assert.NotNull(cut.Instance.ChildContent);
        }

        [Fact]
        public void LablePropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<MenuAboutItem>(parameters => parameters.Add(p => p.Label, "testLabel"));

            // Assert
            Assert.Equal("testLabel", cut.Instance.Label);
        }
    }
}
