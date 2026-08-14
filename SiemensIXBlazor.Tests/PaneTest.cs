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
using System.Text.Json;

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
                ("NoPadding", true),
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
            cut.MarkupMatches("<ix-pane id=\"testId\" borderless composition=\"top\" expanded close-on-click-outside=\"\" heading=\"Test Heading\" hide-on-collapse icon=\"Test Icon\" size=\"240px\" variant=\"inline\" no-padding aria-label-collapse-close-button=\"testAriaLabelCollapseCloseButton\"></ix-pane>");
        }

        [Fact]
        public void PaneUsesOfficialDefaultValues()
        {
            var cut = RenderComponent<Pane>(("Id", "default-pane"));

            Assert.DoesNotContain("close-on-click-outside", cut.Markup);
            Assert.DoesNotContain("no-padding", cut.Markup);
            Assert.False(cut.Instance.CloseOnClickOutside);
            Assert.False(cut.Instance.NoPadding);
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

        [Fact]
        public void PaneRendersHeaderSlot()
        {
            // Arrange
            var expectedHeaderContent = "Header content";

            // Act
            var cut = RenderComponent<Pane>(parameters => parameters
                .Add(p => p.Id, "testPane")
                .Add(p => p.HeaderContent, builder => 
                {
                    builder.AddContent(0, expectedHeaderContent);
                }));

            // Assert
            var markup = cut.Markup;
            Assert.Contains("slot=\"header\"", markup);
            Assert.Contains(expectedHeaderContent, markup);
        }

        [Fact]
        public void PaneDoesNotRenderHeaderSlotWhenNull()
        {
            // Arrange & Act
            var cut = RenderComponent<Pane>(parameters => {
                parameters.Add(p => p.Id, "testPane");
                parameters.Add(p => p.Heading, "Test Heading");
            });

            // Assert
            Assert.DoesNotContain("slot=\"header\"", cut.Markup);
        }

        [Fact]
        public async Task ExpandedChangedDeserializesOfficialBooleanPayload()
        {
            PaneExpandedChangedEventResponse? response = null;
            var cut = RenderComponent<Pane>(parameters => parameters
                .Add(p => p.Id, "pane-event")
                .Add(p => p.ExpandedChangedEvent, EventCallback.Factory.Create<PaneExpandedChangedEventResponse>(this, value => response = value)));

            await cut.Instance.ExpandChanged(JsonDocument.Parse("{\"slot\":\"left\",\"expanded\":true}").RootElement);

            Assert.NotNull(response);
            Assert.Equal("left", response!.Slot);
            Assert.True(response.Expanded);
        }
    }
}
