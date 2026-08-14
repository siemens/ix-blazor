// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using SiemensIXBlazor.Components;

namespace SiemensIXBlazor.Tests.Dropdown
{
    public class DropdownHeaderTests : TestContextBase
    {
        [Fact]
        public void DropdownHeaderRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<DropdownHeader>(parameters => {
                parameters.Add(p => p.Label, "testLabel");
            });

            // Assert
            cut.MarkupMatches("<ix-dropdown-header label='testLabel'></ix-dropdown-header>");
        }

        [Fact]
        public void DropdownHeaderDoesNotExposeChildContent()
        {
            var cut = RenderComponent<DropdownHeader>();

            Assert.DoesNotContain("ChildContent", cut.Markup);
        }
    }
}
