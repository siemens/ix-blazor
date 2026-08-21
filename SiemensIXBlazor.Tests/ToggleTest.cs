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
            var cut = Render<Toggle>(parameters => parameters
                .Add(p => p.Id, "testId")
                .Add(p => p.Checked, true)
                .Add(p => p.Disabled, true)
                .Add(p => p.HideText, true)
                .Add(p => p.Indeterminate, true)
                .Add(p => p.Name, "notifications")
                .Add(p => p.Required, true)
                .Add(p => p.TextIndeterminate, "Mixed")
                .Add(p => p.TextOff, "Off")
                .Add(p => p.TextOn, "On")
                .Add(p => p.Value, "enabled")
            );

            // Assert
            cut.MarkupMatches("<ix-toggle id=\"testId\" checked='true' disabled='true' hide-text='true' indeterminate='true' name=\"notifications\" required='true' text-indeterminate=\"Mixed\" text-off=\"Off\" text-on=\"On\" value=\"enabled\"></ix-toggle>");
        }

        [Fact]
        public void CheckedChangeEventWorks()
        {
            // Arrange
            var checkedChanged = false;
            var cut = Render<Toggle>(parameters => parameters
                .Add(p => p.Id, "testId")
                .Add(p => p.CheckedChangeEvent, EventCallback.Factory.Create(this, (bool value) => checkedChanged = true))
            );

            // Act
            cut.Instance.CheckedChanged(true);

            // Assert
            Assert.True(checkedChanged);
            Assert.True(cut.Instance.Checked);
        }

        [Fact]
        public async Task IxBlurEventWorks()
        {
            var blurred = false;
            var cut = Render<Toggle>(parameters => parameters
                .Add(p => p.Id, "testId")
                .Add(p => p.IxBlurEvent, EventCallback.Factory.Create(this, () => blurred = true)));

            await cut.Instance.Blurred();

            Assert.True(blurred);
        }
    }
}
