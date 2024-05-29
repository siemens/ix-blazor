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
                ("Selected", 0),
                ("Small", true)
            );

            // Assert
            cut.MarkupMatches("<ix-tabs id=\"testId\" layout=\"auto\" placement=\"bottom\" rounded selected=\"0\" small></ix-tabs>");
        }

        [Fact]
        public void SelectedChangeEventWorks()
        {
            // Arrange
            var selectedChanged = false;
            var cut = RenderComponent<Tabs>(
                ("Id", "testId"),
                ("SelectedChangeEvent", EventCallback.Factory.Create<int>(this, value => selectedChanged = true))
            );

            // Act
            cut.Instance.SelectedChange(1);

            // Assert
            Assert.True(selectedChanged);
        }
    }
}