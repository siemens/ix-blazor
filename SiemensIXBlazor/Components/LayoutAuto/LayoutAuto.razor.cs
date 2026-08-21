// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Interops;
using SiemensIXBlazor.Objects.LayoutAuto;

namespace SiemensIXBlazor.Components.LayoutAuto;

public partial class LayoutAuto
{
    private static LayoutAutoItem[] CreateDefaultLayout() =>
    [
        new() { MinWidth = "0", Columns = 1 },
        new() { MinWidth = "48em", Columns = 2 }
    ];

    private BaseInterop? _interop;
    private LayoutAutoItem[]? _appliedLayout;

    [Parameter, EditorRequired]
    public string Id { get; set; } = string.Empty;

    [Parameter]
    public LayoutAutoItem[] Layout { get; set; } = CreateDefaultLayout();

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (_interop is not null)
        {
            await ApplyLayoutAsync();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _interop = new(JSRuntime);
            RegisterDisposable(_interop);
            await ApplyLayoutAsync();
        }
    }

    private async Task ApplyLayoutAsync()
    {
        var layout = Layout ?? [];
        if (_appliedLayout is not null &&
            _appliedLayout.SequenceEqual(layout, LayoutAutoItemComparer.Instance))
        {
            return;
        }

        await _interop!.SetElementProperty(Id, "layout", layout);
        _appliedLayout = layout
            .Select(item => new LayoutAutoItem
            {
                MinWidth = item.MinWidth,
                Columns = item.Columns
            })
            .ToArray();
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
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
