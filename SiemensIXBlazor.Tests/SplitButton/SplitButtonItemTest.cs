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

namespace SiemensIXBlazor.Tests.SplitButton

{
    public class SplitButtonItemTests : TestContextBase
    {
        [Fact]
        public void SplitButtonItemRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<SplitButtonItem>(
                ("Id", "testId"),
                ("Icon", "test-icon"),
                ("Label", "Test Label")
            );

            // Assert
            cut.MarkupMatches("<ix-split-button-item id=\"testId\" icon=\"test-icon\" label=\"Test Label\"></ix-split-button-item>");
        }

        [Fact]
        public void ItemClickedEventWorks()
        {
            // Arrange
            var itemClicked = false;
            var cut = RenderComponent<SplitButtonItem>(
                ("Id", "testId"),
                ("ItemClickedEvent", EventCallback.Factory.Create(this, () => itemClicked = true))
            );

            // Act
            cut.Instance.ItemClicked();

            // Assert
            Assert.True(itemClicked);
        }
    }
}