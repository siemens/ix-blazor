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
using SiemensIXBlazor.Enums.Breadcrumb;

namespace SiemensIXBlazor.Tests
{
    public class BreadcrumbItemTests : TestContextBase
    {
        [Fact]
        public void BreadcrumbItemRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<BreadcrumbItem>();

            // Assert
            cut.MarkupMatches("<ix-breadcrumb-item target='_self'></ix-breadcrumb-item>");
        }

        [Fact]
        public void AllPropertiesAreSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<BreadcrumbItem>(parameters => parameters
                .Add(p => p.Icon, "testIcon")
                .Add(p => p.Label, "testLabel")
                .Add(p => p.AriaLabelButton, "testButton")
                .Add(p => p.Href, "/test")
                .Add(p => p.Target, BreadcrumbTarget._blank)
                .Add(p => p.Rel, "testRel"));

            // Assert
            cut.MarkupMatches(@"
                <ix-breadcrumb-item icon='testIcon' 
                                    label='testLabel' 
                                    aria-label-button='testButton'
                                    href='/test'
                                    target='_blank'
                                    rel='testRel'>
                </ix-breadcrumb-item>");
        }
    }
}