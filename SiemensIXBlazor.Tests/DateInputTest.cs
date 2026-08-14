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
using SiemensIXBlazor.Components.DateInput;

namespace SiemensIXBlazor.Tests;

public class DateInputTest : TestContextBase
{
    [Fact]
    public void EnableTopLayerDefaultsToFalse()
    {
        // Arrange
        var cut = RenderComponent<DateInput>(parameters => parameters
            .Add(p => p.Id, "test-id"));

        // Assert
        Assert.False(cut.Instance.EnableTopLayer);
        Assert.DoesNotContain("enable-top-layer", cut.Markup);
    }

    [Fact]
    public void EnableTopLayerTrueRendersAttribute()
    {
        // Arrange
        var cut = RenderComponent<DateInput>(parameters => parameters
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
        var cut = RenderComponent<DateInput>(parameters => parameters
            .Add(p => p.Id, "test-id")
            .Add(p => p.ChangeEvent, EventCallback.Factory.Create<string?>(this, (string? val) => received = val)));

        // Act
        cut.Instance.Change(JsonDocument.Parse("\"2026/04/01\"").RootElement);

        // Assert
        Assert.Equal("2026/04/01", received);
    }

    [Fact]
    public void ChangeEventReceivesNullWhenValueIsNull()
    {
        // Arrange
        string? received = "initial";
        var cut = RenderComponent<DateInput>(parameters => parameters
            .Add(p => p.Id, "test-id")
            .Add(p => p.ChangeEvent, EventCallback.Factory.Create<string?>(this, (string? val) => received = val)));

        // Act
        cut.Instance.Change(JsonDocument.Parse("null").RootElement);

        // Assert
        Assert.Null(received);
    }
}
