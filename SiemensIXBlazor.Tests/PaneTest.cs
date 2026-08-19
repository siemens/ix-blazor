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
            var cut = Render<Pane>(parameters => parameters
                .Add(p => p.Id, "testId")
                .Add(p => p.Borderless, true)
                .Add(p => p.NoPadding, true)
                .Add(p => p.Composition, PaneComposition.top)
                .Add(p => p.Expanded, true)
                .Add(p => p.CloseOnClickOutside, true)
                .Add(p => p.Heading, "Test Heading")
                .Add(p => p.HideOnCollapse, true)
                .Add(p => p.Icon, "Test Icon")
                .Add(p => p.Size, "240px")
                .Add(p => p.Variant, PaneVariant.inline)
                .Add(p => p.AriaLabelCollapseCloseButton, "testAriaLabelCollapseCloseButton")
            );

            // Assert
            cut.MarkupMatches("<ix-pane id=\"testId\" borderless='true' composition=\"top\" expanded='true' close-on-click-outside=\"true\" heading=\"Test Heading\" hide-on-collapse='true' icon=\"Test Icon\" size=\"240px\" variant=\"inline\" no-padding='true' aria-label-collapse-close-button=\"testAriaLabelCollapseCloseButton\"></ix-pane>");
        }

        [Fact]
        public void PaneUsesOfficialDefaultValues()
        {
            var cut = Render<Pane>(parameters => parameters
                .Add(p => p.Id, "default-pane")
            );

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
            var cut = Render<Pane>(parameters => parameters
                .Add(p => p.Id, "pane")
                .Add(p => p.ExpandedChangedEvent, EventCallback.Factory.Create<PaneExpandedChangedEventResponse>(this, newValue => { expandedChanged = true; }))
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
            var cut = Render<Pane>(parameters => parameters
                .Add(p => p.Id, "pane")
                .Add(p => p.BorderlessChangedEvent, EventCallback.Factory.Create<PaneBorderlessChangedEventResponse>(this, newValue => { borderlessChanged = true; }))
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
            var cut = Render<Pane>(parameters => parameters
                .Add(p => p.Id, "pane")
                .Add(p => p.VariantChangedEvent, EventCallback.Factory.Create<PaneVariantChangedEventResponse>(this, newValue => { variantChanged = true; }))
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
            var cut = Render<Pane>(parameters => parameters
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
            var cut = Render<Pane>(parameters => {
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
            var cut = Render<Pane>(parameters => parameters
                .Add(p => p.Id, "pane-event")
                .Add(p => p.ExpandedChangedEvent, EventCallback.Factory.Create<PaneExpandedChangedEventResponse>(this, value => response = value)));

            await cut.Instance.ExpandChanged(JsonDocument.Parse("{\"slot\":\"left\",\"expanded\":true}").RootElement);

            Assert.NotNull(response);
            Assert.Equal("left", response!.Slot);
            Assert.True(response.Expanded);
        }
    }
}
