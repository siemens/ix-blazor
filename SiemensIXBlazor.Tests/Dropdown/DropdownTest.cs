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
using SiemensIXBlazor.Enums.DropdownButton;

namespace SiemensIXBlazor.Tests.Dropdown;

public class DropdownTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithCorrectProperties()
    {
        // Arrange
        var cut = Render<Components.Dropdown>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.Anchor, "testAnchor")
            .Add(p => p.CloseBehavior, DropdownButtonCloseBehavior.both)
            .Add(p => p.Header, "testHeader")
            .Add(p => p.Placement, "bottom-start")
            .Add(p => p.PositioningStrategy, "fixed")
            .Add(p => p.Show, false)
            .Add(p => p.SuppressAutomaticPlacement, false)
            .Add(p => p.SuppressTriggerVisibilityCheck, true)
            .Add(p => p.DisableFocusHandling, true)
            .Add(p => p.DisableFocusTrap, true)
            .Add(p => p.EnableTopLayer, true)
            .Add(p => p.FocusCheckedItem, true)
            .Add(p => p.Trigger, "testTrigger")
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content"))));

        // Assert
        cut.MarkupMatches(
            "<ix-dropdown id=\"testId\" trigger=\"testTrigger\" anchor=\"testAnchor\" close-behavior=\"both\" header=\"testHeader\" placement=\"bottom-start\" positioning-strategy=\"fixed\" suppress-trigger-visibility-check disable-focus-handling disable-focus-trap enable-top-layer focus-checked-item>Test content</ix-dropdown>");
    }

    [Fact]
    public async Task EventCallbacksAreTriggeredCorrectly()
    {
        // Arrange
        var showChange = false;
        var showChanged = false;

        var cut = Render<Components.Dropdown>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.ShowChangeEvent,
                EventCallback.Factory.Create<bool>(this, value => showChange = value))
            .Add(p => p.ShowChangedEvent,
                EventCallback.Factory.Create<bool>(this, value => showChanged = value)));

        // Act
        await cut.Instance.ShowChange(true);
        await cut.Instance.ShowChanged(true);

        // Assert
        Assert.True(showChange);
        Assert.True(showChanged);
    }

    [Fact]
    public void SupportsBooleanCloseBehavior()
    {
        var cut = Render<Components.Dropdown>(parameters => parameters
            .Add(p => p.Id, "testId")
            .Add(p => p.CloseBehavior, false));

        Assert.False((bool)cut.Instance.CloseBehavior);
        Assert.DoesNotContain("close-behavior", cut.Markup);
    }
}
