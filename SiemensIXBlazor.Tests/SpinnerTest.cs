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
using SiemensIXBlazor.Enums.Spinner;
using Xunit;

namespace SiemensIXBlazor.Tests
{
    public class SpinnerTests : TestContextBase
    {
        [Fact]
        public void SpinnerRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Spinner>(
                ("Size", "large"),
                ("Variant", SpinnerVariant.primary),
                ("Style", "color: red;"),
                ("Class", "test-class")
            );

            // Assert
            cut.MarkupMatches("<ix-spinner size=\"large\" variant=\"primary\" style=\"color: red;\" class=\"test-class\"></ix-spinner>");
        }
    }
}