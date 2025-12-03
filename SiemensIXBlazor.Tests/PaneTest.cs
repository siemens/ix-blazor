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
using SiemensIXBlazor.Enums.Pane;
using SiemensIXBlazor.Objects.Pane;

namespace SiemensIXBlazor.Tests
{
    public class PaneTests : TestContextBase
    {
        [Fact]
        public void PaneRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Pane>(
                ("Id", "testId"),
                ("Borderless", true),
                ("Composition", PaneComposition.top),
                ("Expanded", true),
                ("CloseOnClickOutside", true),
                ("Heading", "Test Heading"),
                ("HideOnCollapse", true),
                ("Icon", "Test Icon"),
                ("Size", "240px"),
                ("Variant", PaneVariant.inline),
                ("AriaLabelCollapseCloseButton", "testAriaLabelCollapseCloseButton")
            );

            // Assert
            cut.MarkupMatches("<ix-pane id=\"testId\" borderless composition=\"top\" expanded close-on-click-outside=\"\" heading=\"Test Heading\" hide-on-collapse icon=\"Test Icon\" size=\"240px\" variant=\"inline\" aria-label-collapse-close-button=\"testAriaLabelCollapseCloseButton\"></ix-pane>");
        }

        [Fact]
        public async Task ExpandedChangedEventWorks()
        {
            // Arrange
            var expandedChanged = false;
            var cut = RenderComponent<Pane>(
                ("Id", "pane"),
                ("ExpandedChangedEvent", EventCallback.Factory.Create<PaneExpandedChangedEventResponse>(this, newValue => { expandedChanged = true; }))
            );

            // Act
            await cut.Instance.ExpandedChangedEvent.InvokeAsync(new PaneExpandedChangedEventResponse());

            // Assert
            Assert.True(expandedChanged);
        }

        [Fact]
        public async Task BorderlessChangedEventWorks()
        {
            // Arrange
            var borderlessChanged = false;
            var cut = RenderComponent<Pane>(
                ("Id", "pane"),
                ("BorderlessChangedEvent", EventCallback.Factory.Create<PaneBorderlessChangedEventResponse>(this, newValue => { borderlessChanged = true; }))
            );

            // Act
            await cut.Instance.BorderlessChangedEvent.InvokeAsync(new PaneBorderlessChangedEventResponse());

            // Assert
            Assert.True(borderlessChanged);
        }

        [Fact]
        public async Task VariantChangedEventWorks()
        {
            // Arrange
            var variantChanged = false;
            var cut = RenderComponent<Pane>(
                ("Id", "pane"),
                ("VariantChangedEvent", EventCallback.Factory.Create<PaneVariantChangedEventResponse>(this, newValue => { variantChanged = true; }))
            );

            // Act
            await cut.Instance.VariantChangedEvent.InvokeAsync(new PaneVariantChangedEventResponse());

            // Assert
            Assert.True(variantChanged);
        }
    }
}
