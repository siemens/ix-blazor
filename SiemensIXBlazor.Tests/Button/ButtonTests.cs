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

namespace SiemensIXBlazor.Tests
{
    public class ButtonTests: TestContextBase
    {
        [Fact]
        public void ButtonRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<Button>(parameters => {
                parameters.Add(p => p.Id, "testId");
                parameters.Add(p => p.DataToggle, "testDataToggle");
                parameters.Add(p => p.Variant, ButtonVariant.primary);
                parameters.Add(p => p.Disabled, true);
                parameters.Add(p => p.Ghost, true);
                parameters.Add(p => p.Icon, "testIcon");
                parameters.Add(p => p.Loading, true);
                parameters.Add(p => p.Outline, true);
                parameters.Add(p => p.Selected, true);
                parameters.Add(p => p.Type, ButtonType.Button);
                parameters.Add(p => p.DataTooltip, "testDataTooltip");
            });
        
            // Assert
            cut.MarkupMatches("<ix-button  id='testId' disabled='' ghost='' outline='' selected='' icon='testIcon' loading='' type='button' variant='primary' data-toggle='testDataToggle' data-tooltip='testDataTooltip'></ix-button>");
        }

        [Fact]
        public void ButtonComponentHandlesClickEvent()
        {
            // Arrange
            var clickInvoked = false;
            var cut = RenderComponent<Button>(parameters => parameters
                .Add(p => p.ClickEvent, EventCallback.Factory.Create(this, () => clickInvoked = true)));

            // Act
            cut.Find("ix-button").Click();

            // Assert
            Assert.True(clickInvoked);
        }
    }
}