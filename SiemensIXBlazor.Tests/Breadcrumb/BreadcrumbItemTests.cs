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

namespace SiemensIXBlazor.Tests
{
    public class BreadcrumbItemTests: TestContextBase
    {
        [Fact]
        public void BreadcrumbItemRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<BreadcrumbItem>();
        
            // Assert
            cut.MarkupMatches("<ix-breadcrumb-item ></ix-breadcrumb-item>");
        }
    }
}