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
using SiemensIXBlazor.Enums.KeyValue;

namespace SiemensIXBlazor.Tests;

public class KeyValueTest : TestContextBase
{
    [Fact]
    public void ComponentRendersCustomValueThroughNamedSlot()
    {
        // Arrange
        var cut = Render<KeyValue>(parameters => parameters
            .Add(p => p.CustomValue, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content")))
            .Add(p => p.Icon, "testIcon")
            .Add(p => p.AriaLabelIcon, "Test icon")
            .Add(p => p.Label, "testLabel")
            .Add(p => p.LabelPosition, KeyValueLabelPosition.left));

        // Assert
        var element = cut.Find("ix-key-value");
        Assert.Equal("testIcon", element.GetAttribute("icon"));
        Assert.Equal("Test icon", element.GetAttribute("aria-label-icon"));
        Assert.Equal("testLabel", element.GetAttribute("label"));
        Assert.Equal("left", element.GetAttribute("label-position"));

        var customValue = cut.Find("[slot=\"custom-value\"]");
        Assert.Equal("Test content", customValue.TextContent);
    }

    [Fact]
    public void ComponentUsesTextValueInsteadOfCustomValueSlotWhenValueIsSet()
    {
        // Arrange
        var cut = Render<KeyValue>(parameters => parameters
            .Add(p => p.CustomValue, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content")))
            .Add(p => p.Label, "testLabel")
            .Add(p => p.Value, "testValue"));

        // Assert
        var element = cut.Find("ix-key-value");
        Assert.Equal("testValue", element.GetAttribute("value"));
        Assert.Empty(cut.FindAll("[slot=\"custom-value\"]"));
    }

    [Fact]
    public void ComponentUsesOfficialDefaultLabelPosition()
    {
        // Arrange
        var cut = Render<KeyValue>(parameters => parameters
            .Add(p => p.Label, "testLabel"));

        // Assert
        Assert.Equal(KeyValueLabelPosition.top, cut.Instance.LabelPosition);
        Assert.Equal("top", cut.Find("ix-key-value").GetAttribute("label-position"));
    }
}
