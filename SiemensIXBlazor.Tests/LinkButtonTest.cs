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
using SiemensIXBlazor.Enums.LinkButton;

namespace SiemensIXBlazor.Tests;

public class LinkButtonTest : TestContextBase
{
    [Fact]
    public void ComponentRendersWithParametersSetCorrectly()
    {
        // Arrange
        var cut = RenderComponent<LinkButton>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content")))
            .Add(p => p.Disabled, true)
            .Add(p => p.Target, LinkButtonTarget._blank)
            .Add(p => p.Url, "https://example.com"));

        // Assert
        cut.MarkupMatches("<ix-link-button url=\"https://example.com\" disabled=\"\" target=\"_blank\">Test content</ix-link-button>");
    }
}