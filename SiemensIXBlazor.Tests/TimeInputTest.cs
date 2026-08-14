// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Text.Json;
using Bunit;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Components.TimeInput;

namespace SiemensIXBlazor.Tests;

public class TimeInputTest : TestContextBase
{
    [Fact]
    public void EnableTopLayerDefaultsToFalse()
    {
        // Arrange
        var cut = RenderComponent<TimeInput>(parameters => parameters
            .Add(p => p.Id, "test-id"));

        // Assert
        Assert.False(cut.Instance.EnableTopLayer);
        Assert.DoesNotContain("enable-top-layer", cut.Markup);
    }

    [Fact]
    public void EnableTopLayerTrueRendersAttribute()
    {
        // Arrange
        var cut = RenderComponent<TimeInput>(parameters => parameters
            .Add(p => p.Id, "test-id")
            .Add(p => p.EnableTopLayer, true));

        // Assert
        Assert.True(cut.Instance.EnableTopLayer);
        Assert.Contains("enable-top-layer", cut.Markup);
    }

    [Fact]
    public void ChangeEventWorks()
    {
        // Arrange
        var received = string.Empty;
        var cut = RenderComponent<TimeInput>(parameters => parameters
            .Add(p => p.Id, "test-id")
            .Add(p => p.ChangeEvent, EventCallback.Factory.Create<string>(this, (string val) => received = val)));

        // Act
        cut.Instance.Change(JsonDocument.Parse("\"14:30:00\"").RootElement);

        // Assert
        Assert.Equal("14:30:00", received);
    }

    [Fact]
    public void ChangeEventReceivesEmptyStringWhenValueIsNull()
    {
        // Arrange
        var received = "initial";
        var cut = RenderComponent<TimeInput>(parameters => parameters
            .Add(p => p.Id, "test-id")
            .Add(p => p.ChangeEvent, EventCallback.Factory.Create<string>(this, (string val) => received = val)));

        // Act
        cut.Instance.Change(JsonDocument.Parse("null").RootElement);

        // Assert
        Assert.Equal(string.Empty, received);
    }

    [Fact]
    public void OfficialDefaultsAreExposed()
    {
        var cut = RenderComponent<TimeInput>(parameters => parameters.Add(p => p.Id, "time-input"));

        Assert.Equal("TT", cut.Instance.Format);
        Assert.Equal(100, cut.Instance.MillisecondInterval);
        Assert.Equal("Toggle time picker", cut.Instance.AriaLabelTimeToggleButton);
        Assert.Contains("aria-label-time-toggle-button", cut.Markup);
    }
}
