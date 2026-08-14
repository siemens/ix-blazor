// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Components.LayoutAuto;
using SiemensIXBlazor.Objects.LayoutAuto;
using System.Text.Json;

namespace SiemensIXBlazor.Tests.LayoutAutoComponent;

public class LayoutAutoTests : TestContextBase
{
    [Fact]
    public void ComponentRendersChildContentAndId()
    {
        var cut = RenderComponent<LayoutAuto>(parameters => parameters
            .Add(p => p.Id, "layout-auto")
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
                builder.AddMarkupContent(0, "Test content"))));

        cut.MarkupMatches("<ix-layout-auto id=\"layout-auto\">Test content</ix-layout-auto>");
    }

    [Fact]
    public void LayoutDefaultsMatchOfficialValues()
    {
        var cut = RenderComponent<LayoutAuto>(parameters => parameters
            .Add(p => p.Id, "layout-auto"));

        Assert.Equal(
            [
                new LayoutAutoItem { MinWidth = "0", Columns = 1 },
                new LayoutAutoItem { MinWidth = "48em", Columns = 2 }
            ],
            cut.Instance.Layout,
            LayoutAutoItemComparer.Instance);
    }

    [Fact]
    public void LayoutAcceptsCustomBreakpointObjects()
    {
        var layout = new[]
        {
            new LayoutAutoItem { MinWidth = "0", Columns = 1 },
            new LayoutAutoItem { MinWidth = "64em", Columns = 3 }
        };

        var cut = RenderComponent<LayoutAuto>(parameters => parameters
            .Add(p => p.Id, "layout-auto")
            .Add(p => p.Layout, layout));

        Assert.Same(layout, cut.Instance.Layout);
    }

    [Fact]
    public void LayoutItemsSerializeWithOfficialPropertyNames()
    {
        var layout = new[]
        {
            new LayoutAutoItem { MinWidth = "48em", Columns = 2 }
        };

        Assert.Equal(
            "[{\"minWidth\":\"48em\",\"columns\":2}]",
            JsonSerializer.Serialize(layout));
    }

    private sealed class LayoutAutoItemComparer : IEqualityComparer<LayoutAutoItem>
    {
        public static LayoutAutoItemComparer Instance { get; } = new();

        public bool Equals(LayoutAutoItem? x, LayoutAutoItem? y) =>
            x is not null && y is not null &&
            x.MinWidth == y.MinWidth && x.Columns == y.Columns;

        public int GetHashCode(LayoutAutoItem obj) =>
            HashCode.Combine(obj.MinWidth, obj.Columns);
    }
}
