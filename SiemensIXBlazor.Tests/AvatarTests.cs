// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using SiemensIXBlazor.Components.Avatar;

namespace SiemensIXBlazor.Tests
{
    public class AvatarTests : TestContextBase
    {
        [Fact]
        public void AvatarRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<Avatar>(parameters => {
                parameters.Add(p => p.Image, "testImage");
                parameters.Add(p => p.Initials, "testInitials");
            });
        
            // Assert
            cut.MarkupMatches("<ix-avatar image='testImage' initials='testInitials'></ix-avatar>");
        }
    }
}