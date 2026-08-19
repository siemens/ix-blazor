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
using SiemensIXBlazor.Enums.ToggleButton;
using Xunit;

namespace SiemensIXBlazor.Tests.ToggleButton
{
    public class ToggleButtonTests : TestContextBase
    {
        [Fact]
        public void ToggleButtonRendersCorrectly()
        {
            // Arrange
            var cut = Render<Components.ToggleButton.ToggleButton>(parameters => parameters
                .Add(p => p.Id, "testId")
                .Add(p => p.Disabled, true)
                .Add(p => p.Icon, "test-icon")
                .Add(p => p.IconRight, "test-icon-right")
                .Add(p => p.Loading, true)
                .Add(p => p.Pressed, true)
                .Add(p => p.Variant, ToggleButtonVariant.subtle_secondary)
            );

            // Assert
            cut.MarkupMatches("<ix-toggle-button id=\"testId\" disabled='true' icon=\"test-icon\" icon-right=\"test-icon-right\" loading='true' pressed='true' variant=\"subtle-secondary\"></ix-toggle-button>");
        }

        [Fact]
        public async Task PressedChangeEventWorks()
        {
            // Arrange
            var pressedChanged = false;
            var cut = Render<Components.ToggleButton.ToggleButton>(parameters => parameters
                .Add(p => p.Id, "toggleButton")
                .Add(p => p.PressedChangeEvent, EventCallback.Factory.Create<bool>(this, newValue => { pressedChanged = true; }))
            );

            // Act
            await cut.Instance.PressedChange(true);

            // Assert
            Assert.True(pressedChanged);
        }
    }
}
