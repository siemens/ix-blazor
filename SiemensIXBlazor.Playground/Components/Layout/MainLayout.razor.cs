// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components.Routing;
using SiemensIXBlazor.Components;

namespace SiemensIXBlazor.Playground.Components.Layout;

public partial class MainLayout : IDisposable
{
    private Theme? _themeProvider;
    private string CurrentPath =>
        Navigation?.ToAbsoluteUri(Navigation.Uri).AbsolutePath.TrimEnd('/') ?? string.Empty;

    private static string NormalizePath(string? path) =>
        "/" + (path ?? string.Empty).Trim('/').ToLowerInvariant();

    private static readonly HashSet<string> RootAliases = new(
        new[] { "", "/", "overview" }.Select(NormalizePath)
    );

    protected override void OnInitialized()
    {
        if (Navigation is not null)
        {
            Navigation.LocationChanged += OnLocationChanged;
        }
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e) => _ = InvokeAsync(StateHasChanged);

    private async Task ToggleThemeAsync()
    {
        if (_themeProvider is not null)
        {
            await _themeProvider.ToggleTheme();
        }
    }

    public void RedirectPage(string route)
    {
        if (!string.IsNullOrWhiteSpace(route))
        {
            Navigation?.NavigateTo($"/{route.TrimStart('/')}");
        }
    }

    public bool IsActive(string route)
    {
        var normalizedRoute = NormalizePath(route);
        var current = NormalizePath(CurrentPath);

        if (RootAliases.Contains(normalizedRoute))
        {
            return RootAliases.Any(alias =>
                current == alias || current.StartsWith(alias + "/", StringComparison.OrdinalIgnoreCase));
        }

        return current == normalizedRoute || current.StartsWith(normalizedRoute + "/", StringComparison.OrdinalIgnoreCase);
    }

    public bool IsMenuItemActive(params string[] routes) =>
        routes.Any(IsActive);

    public void Dispose()
    {
        if (Navigation is not null)
        {
            Navigation.LocationChanged -= OnLocationChanged;
        }
        GC.SuppressFinalize(this);
    }
}
