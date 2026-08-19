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
            var cut = Render<Button>(parameters => {
                parameters.Add(p => p.Id, "testId");
                parameters.Add(p => p.Variant, ButtonVariant.primary);
                parameters.Add(p => p.Disabled, true);
                parameters.Add(p => p.Icon, "testIcon");
                parameters.Add(p => p.IconRight, "testIconRight");
                parameters.Add(p => p.Loading, true);
                parameters.Add(p => p.Type, ButtonType.Button);
                parameters.Add(p => p.Form, "testForm");
                parameters.Add(p => p.Href, "/test");
                parameters.Add(p => p.Target, ButtonTarget._blank);
                parameters.Add(p => p.Rel, "noopener");
            });

            // Assert
            cut.MarkupMatches("<ix-button id='testId' disabled='true' icon='testIcon' icon-right='testIconRight' loading='true' type='button' variant='primary' form='testForm' href='/test' target='_blank' rel='noopener'></ix-button>");
        }

        [Fact]
        public void ButtonComponentHandlesClickEvent()
        {
            // Arrange
            var clickInvoked = false;
            var cut = Render<Button>(parameters => parameters
                .Add(p => p.ClickEvent, EventCallback.Factory.Create(this, () => clickInvoked = true)));

            // Act
            cut.Find("ix-button").Click();

            // Assert
            Assert.True(clickInvoked);
        }
    }
}
