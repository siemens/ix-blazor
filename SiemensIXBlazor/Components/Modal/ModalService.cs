// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Text.Json;
using Microsoft.JSInterop;

namespace SiemensIXBlazor.Components.Modal;

internal sealed class ModalRequest
{
    internal ModalRequest(ModalConfig config, IModalInstance instance)
    {
        Config = config;
        Instance = instance;
    }

    public ModalConfig Config { get; }
    internal IModalInstance Instance { get; }
}

/// <summary>
/// Opens and controls modals rendered by <see cref="ModalHost"/>.
/// Register this service and render one <see cref="ModalHost"/> in the application layout.
/// </summary>
public sealed class ModalService
{
    private readonly IJSRuntime _jsRuntime;
    private ModalRequest? _current;

    public ModalService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public event Action? Changed;

    internal ModalRequest? Current => _current;

    public Task<ModalInstance<TReason>> OpenAsync<TReason>(ModalConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        if (_current is not null)
        {
            throw new InvalidOperationException("Only one modal can be open per ModalHost.");
        }

        var instance = new ModalInstance<TReason>(this, $"ix-modal-{Guid.NewGuid():N}");
        _current = new ModalRequest(config, instance);
        Changed?.Invoke();
        return Task.FromResult(instance);
    }

    public Task<ModalInstance<TReason>> ShowAsync<TReason>(ModalConfig config) => OpenAsync<TReason>(config);

    internal async Task CloseAsync<TReason>(ModalInstance<TReason> instance, TReason reason)
    {
        EnsureCurrent(instance);
        await _jsRuntime.InvokeVoidAsync("siemensIXInterop.modal.close", instance.Id, reason);
    }

    internal async Task DismissAsync<TReason>(ModalInstance<TReason> instance, TReason? reason)
    {
        EnsureCurrent(instance);
        await _jsRuntime.InvokeVoidAsync("siemensIXInterop.modal.dismiss", instance.Id, reason);
    }

    internal Task<bool> BeforeDismissAsync(string id, JsonElement? reason)
    {
        if (_current is null || !string.Equals(id, _current.Instance.Id, StringComparison.Ordinal))
        {
            return Task.FromResult(true);
        }

        return _current?.Config.BeforeDismiss?.Invoke(reason) ?? Task.FromResult(true);
    }

    internal void HandleClose(string id, JsonElement? reason)
    {
        if (!TryGetCurrent(id, out var instance))
        {
            return;
        }

        InvokeCompletion(instance, close: true, reason);
        _current = null;
        Changed?.Invoke();
    }

    internal void HandleDismiss(string id, JsonElement? reason)
    {
        if (!TryGetCurrent(id, out var instance))
        {
            return;
        }

        InvokeCompletion(instance, close: false, reason);
        _current = null;
        Changed?.Invoke();
    }

    private void EnsureCurrent<TReason>(ModalInstance<TReason> instance)
    {
        if (_current?.Instance != instance)
        {
            throw new InvalidOperationException("The modal instance is no longer active.");
        }
    }

    private bool TryGetCurrent(string id, out IModalInstance instance)
    {
        instance = _current?.Instance!;
        return _current is not null && string.Equals(id, instance.Id, StringComparison.Ordinal);
    }

    private static void InvokeCompletion(IModalInstance instance, bool close, JsonElement? reason)
    {
        if (close)
        {
            instance.CompleteClose(reason);
        }
        else
        {
            instance.CompleteDismiss(reason);
        }
    }
}
