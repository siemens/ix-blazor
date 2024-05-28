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

namespace SiemensIXBlazor.Tests;

public class KeyValueTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithParametersSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<KeyValue>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content")))
            .Add(p => p.Icon, "testIcon")
            .Add(p => p.Label, "testLabel")
            .Add(p => p.LabelPosition, "left")
            .Add(p => p.Value, "testValue"));

        // Assert
        cut.MarkupMatches(
            "<ix-key-value label=\"testLabel\" value=\"testValue\" icon=\"testIcon\" label-position=\"left\">Test content</ix-key-value>");
    }
}