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
using SiemensIXBlazor.Enums.Tabs;
using Xunit;

namespace SiemensIXBlazor.Tests
{
    public class TabsTests : TestContextBase
    {
        [Fact]
        public void TabsRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Tabs>(
                ("Id", "testId"),
                ("Layout", TabsLayout.Auto),
                ("Placement", TabsPlacement.Bottom),
                ("Rounded", true),
                ("ActiveTabKey", "legal"),
                ("KeyboardNavigation", TabsKeyboardNavigation.Manual),
                ("Small", true)
            );

            // Assert
            cut.MarkupMatches("<ix-tabs id=\"testId\" layout=\"auto\" placement=\"bottom\" rounded active-tab-key=\"legal\" aria-label-more-tabs=\"Show all tabs\" keyboard-navigation=\"manual\" small></ix-tabs>");
        }

        [Fact]
        public async Task TabChangeEventWorks()
        {
            // Arrange
            string? changedTab = null;
            var cut = RenderComponent<Tabs>(
                ("Id", "testId"),
                ("TabChangedEvent", EventCallback.Factory.Create<string?>(this, value => changedTab = value))
            );

            // Act
            await cut.Instance.TabChanged("licenses");

            // Assert
            Assert.Equal("licenses", changedTab);
        }
    }
}
