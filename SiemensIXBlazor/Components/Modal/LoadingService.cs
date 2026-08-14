// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.JSInterop;

namespace SiemensIXBlazor.Components.Modal;

public sealed class ModalLoadingOptions
{
    public string Message { get; set; } = string.Empty;
    public bool Centered { get; set; }
}

public sealed class ModalLoadingContext : IAsyncDisposable
{
    private readonly IJSObjectReference _reference;

    internal ModalLoadingContext(IJSObjectReference reference)
    {
        _reference = reference;
    }

    public Task UpdateAsync(string text) => _reference.InvokeVoidAsync("update", text).AsTask();
    public Task FinishAsync() => _reference.InvokeVoidAsync("finish").AsTask();

    public Task FinishAsync(string text) => _reference.InvokeVoidAsync("finish", text).AsTask();

    public Task FinishAsync(string text, int timeout) =>
        _reference.InvokeVoidAsync("finish", text, timeout).AsTask();

    public ValueTask DisposeAsync() => _reference.DisposeAsync();
}

/// <summary>
/// Displays and updates the official loading-modal integration.
/// </summary>
public sealed class LoadingService
{
    private readonly IJSRuntime _jsRuntime;

    public LoadingService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<ModalLoadingContext> ShowModalLoadingAsync(ModalLoadingOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        var reference = await _jsRuntime.InvokeAsync<IJSObjectReference>(
            "siemensIXInterop.modal.showLoading", options);
        return new ModalLoadingContext(reference);
    }
}
