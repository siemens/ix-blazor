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
using SiemensIXBlazor.Enums.Button;
using Xunit;

namespace SiemensIXBlazor.Tests.ToggleButton
{
    public class ToggleButtonTests : TestContextBase
    {
        [Fact]
        public void ToggleButtonRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Components.ToggleButton.ToggleButton>(
                ("Id", "testId"),
                ("Disabled", true),
                ("Ghost", true),
                ("Icon", "test-icon"),
                ("Loading", true),
                ("Outline", true),
                ("Pressed", true),
                ("Variant", ButtonVariant.secondary)
            );

            // Assert
            cut.MarkupMatches("<ix-toggle-button id=\"testId\" disabled ghost icon=\"test-icon\" loading outline pressed variant=\"secondary\"></ix-toggle-button>");
        }

        [Fact]
        public async Task PressedChangeEventWorks()
        {
            // Arrange
            var pressedChanged = false;
            var cut = RenderComponent<Components.ToggleButton.ToggleButton>(
                ("Id", "toggleButton"),
                ("PressedChangeEvent", EventCallback.Factory.Create<bool>(this, newValue => { pressedChanged = true; }))
            );

            // Act
            cut.Instance.PressedChange(true);

            // Assert
            Assert.True(pressedChanged);
        }
    }
}