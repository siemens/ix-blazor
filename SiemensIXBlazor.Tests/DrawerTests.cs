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

namespace SiemensIXBlazor.Tests
{
    public class DrawerTests : TestContextBase
    {
        [Fact]
        public void DrawerRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Drawer>(
                ("Id", "testId"),
                ("CloseOnClickOutside", true),
                ("FullHeight", false),
                ("MaxWidth", 28),
                ("MinWidth", 16),
                ("Show", true),
                ("Width", 16)
            );

            // Assert
            cut.MarkupMatches("<ix-drawer minwidth=\"16\" id=\"testId\" show=\"\" close-on-click-outside=\"\" max-width=\"28\" min-width=\"16\" width=\"16\"></ix-drawer>");
        }

        [Fact]
        public async Task ClosedEventWorks()
        {
            // Arrange
            var closed = false;
            var cut = RenderComponent<Drawer>(
                ("Id", "drawer"),
                ("ClosedEvent", EventCallback.Factory.Create(this, () => closed = true))
            );

            // Act
            await cut.Instance.Closed();

            // Assert
            Assert.True(closed);
        }

        [Fact]
        public async Task OpenedEventWorks()
        {
            // Arrange
            var opened = false;
            var cut = RenderComponent<Drawer>(
                ("Id", "drawer"),
                ("OpenedEvent", EventCallback.Factory.Create(this, () => opened = true))
            );

            // Act
            await cut.Instance.Opened();

            // Assert
            Assert.True(opened);
        }
    }
}