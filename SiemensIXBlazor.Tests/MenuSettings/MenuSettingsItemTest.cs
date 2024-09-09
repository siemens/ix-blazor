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
using SiemensIXBlazor.Components.MenuSettings;

namespace SiemensIXBlazor.Tests.MenuSettings
{
	public class MenuSettingsItemTests : TestContextBase
    {
        [Fact]
        public void MenuSettingsItemRendersCorrectly()
        {
            // Arrange
            RenderFragment childContent = builder =>
            {
                builder.AddContent(0, "Simple Text");
            };

            // Act
            var cut = RenderComponent<MenuSettingsItem>(
                ("Label", "Test Label"),
                ("ChildContent", childContent));

            // Assert
            cut.MarkupMatches("<ix-menu-settings-item label=\"Test Label\">Simple Text</ix-menu-settings-item>");
        }
    }
}