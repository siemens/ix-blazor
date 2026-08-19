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
using SiemensIXBlazor.Enums.Button;
using Xunit;

namespace SiemensIXBlazor.Tests.ToggleButton
{
    public class IconToggleButtonTests : TestContextBase
    {
        [Fact]
        public void IconToggleButtonRendersCorrectly()
        {
            // Arrange
            var cut = Render<IconToggleButton>(parameters => parameters
                .Add(p => p.Id, "testId")
                .Add(p => p.Disabled, true)
                .Add(p => p.Ghost, true)
                .Add(p => p.Icon, "test-icon")
                .Add(p => p.Loading, true)
                .Add(p => p.Outline, true)
                .Add(p => p.Pressed, true)
                .Add(p => p.Size, IconButtonSize._16)
                .Add(p => p.Variant, ButtonVariant.subtle_secondary)
                .Add(p => p.Oval, true)
            );

            // Assert
            cut.MarkupMatches("<ix-icon-toggle-button id=\"testId\" disabled='true' ghost='true' icon=\"test-icon\" loading='true' outline='true' pressed='true' size=\"16\" variant=\"subtle-secondary\" oval='true'></ix-icon-toggle-button>");
        }

        [Fact]
        public async Task PressedChangeEventWorks()
        {
            // Arrange
            var pressedChanged = false;
            var cut = Render<IconToggleButton>(parameters => parameters
                .Add(p => p.Id, "iconToggleButton")
                .Add(p => p.PressedChangeEvent, EventCallback.Factory.Create<bool>(this, newValue => { pressedChanged = true; }))
            );

            // Act
            await cut.Instance.PressedChange(true);

            // Assert
            Assert.True(pressedChanged);
        }
    }
}
