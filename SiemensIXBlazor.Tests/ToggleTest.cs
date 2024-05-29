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
using Xunit;

namespace SiemensIXBlazor.Tests
{
    public class ToggleTests : TestContextBase
    {
        [Fact]
        public void ToggleRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Toggle>(
                ("Id", "testId"),
                ("Checked", true),
                ("Disabled", true),
                ("HideText", true),
                ("Indeterminate", true),
                ("TextIndeterminate", "Mixed"),
                ("TextOff", "Off"),
                ("TextOn", "On")
            );

            // Assert
            cut.MarkupMatches("<ix-toggle id=\"testId\" checked disabled hide-text indeterminate text-indeterminate=\"Mixed\" text-off=\"Off\" text-on=\"On\"></ix-toggle>");
        }

        [Fact]
        public void CheckedChangeEventWorks()
        {
            // Arrange
            var checkedChanged = false;
            var cut = RenderComponent<Toggle>(
                ("Id", "testId"),
                ("CheckedChangeEvent", EventCallback.Factory.Create(this, (bool value) => checkedChanged = true))
            );

            // Act
            cut.Instance.CheckedChannged(true);

            // Assert
            Assert.True(checkedChanged);
        }
    }
}
