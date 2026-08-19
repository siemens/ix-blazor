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
    public class DrawerTest : TestContextBase
    {
        [Fact]
        public void DrawerRendersCorrectly()
        {
            // Arrange
            var cut = Render<Drawer>(parameters => parameters
                .Add(p => p.Id, "testId")
                .Add(p => p.CloseOnClickOutside, true)
                .Add(p => p.FullHeight, false)
                .Add(p => p.MaxWidth, 28)
                .Add(p => p.MinWidth, 16)
                .Add(p => p.Show, true)
                .Add(p => p.Width, 16)
            );

            // Assert
            cut.MarkupMatches("<ix-drawer id=\"testId\" show=\"true\" close-on-click-outside=\"true\" max-width=\"28\" min-width=\"16\" width=\"16\"></ix-drawer>");
        }

        [Fact]
        public async Task ClosedEventWorks()
        {
            // Arrange
            var closed = false;
            var cut = Render<Drawer>(parameters => parameters
                .Add(p => p.Id, "drawer")
                .Add(p => p.ClosedEvent, EventCallback.Factory.Create(this, () => closed = true))
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
            var cut = Render<Drawer>(parameters => parameters
                .Add(p => p.Id, "drawer")
                .Add(p => p.OpenedEvent, EventCallback.Factory.Create(this, () => opened = true))
            );

            // Act
            await cut.Instance.Opened();

            // Assert
            Assert.True(opened);
        }
    }
}
