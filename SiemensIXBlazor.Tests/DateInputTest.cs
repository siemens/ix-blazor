// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
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
}
