// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Text.Json;

namespace SiemensIXBlazor.Components.Modal;

internal interface IModalInstance
{
    string Id { get; }
    void CompleteClose(JsonElement? reason);
    void CompleteDismiss(JsonElement? reason);
}

/// <summary>
/// Handle for an opened modal and its close/dismiss results.
/// </summary>
public sealed class ModalInstance<TReason> : IModalInstance
{
    private readonly ModalService _service;
    private readonly TaskCompletionSource<TReason?> _closed = new(TaskCreationOptions.RunContinuationsAsynchronously);
    private readonly TaskCompletionSource<TReason?> _dismissed = new(TaskCreationOptions.RunContinuationsAsynchronously);

    internal ModalInstance(ModalService service, string id)
    {
        _service = service;
        Id = id;
    }

    internal string Id { get; }
    public Task<TReason?> Closed => _closed.Task;
    public Task<TReason?> Dismissed => _dismissed.Task;

    public Task CloseAsync(TReason reason) => _service.CloseAsync(this, reason);
    public Task DismissAsync(TReason? reason = default) => _service.DismissAsync(this, reason);

    string IModalInstance.Id => Id;
    void IModalInstance.CompleteClose(JsonElement? reason) => _closed.TrySetResult(ConvertReason(reason));
    void IModalInstance.CompleteDismiss(JsonElement? reason) => _dismissed.TrySetResult(ConvertReason(reason));

    private static TReason? ConvertReason(JsonElement? reason)
    {
        if (!reason.HasValue || reason.Value.ValueKind is JsonValueKind.Null or JsonValueKind.Undefined)
        {
            return default;
        }

        return reason.Value.Deserialize<TReason>();
    }
}
