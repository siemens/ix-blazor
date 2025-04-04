// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using Xunit;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.PushCard;

namespace SiemensIXBlazor.Tests
{
    public class PushCardTests : TestContextBase
    {
        [Fact]
        public void PushCardRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<PushCard>(
                ("Heading", "Test Heading"),
                ("Icon", "testIcon"),
                ("Notification", "5"),
                ("SubHeading", "Test SubHeading"),
                ("Collapsed", true),
                ("Variant", PushCardVariant.outline)
            );

            // Assert
        
            cut.MarkupMatches("<ix-push-card heading=\"Test Heading\" icon=\"testIcon\" notification=\"5\" subheading=\"Test SubHeading\" collapsed=\"\" variant=\"outline\"></ix-push-card>");
        }
    }
}