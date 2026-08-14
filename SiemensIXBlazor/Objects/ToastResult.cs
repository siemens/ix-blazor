// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.JSInterop;
using System.Text.Json;

namespace SiemensIXBlazor.Objects
{
    public sealed class ToastResult
    {
        private readonly IJSObjectReference _interop;
        private readonly string _handle;

        internal ToastResult(IJSObjectReference interop, string handle)
        {
            _interop = interop;
            _handle = handle;
        }

        public event EventHandler<JsonElement?>? OnClose;

        public ValueTask PauseAsync() => _interop.InvokeVoidAsync("pause", _handle);

        public ValueTask ResumeAsync() => _interop.InvokeVoidAsync("resume", _handle);

        public ValueTask<bool> IsPausedAsync() => _interop.InvokeAsync<bool>("isPaused", _handle);

        public ValueTask CloseAsync(object? result = null) => _interop.InvokeVoidAsync("close", _handle, result);

        internal void NotifyClosed(JsonElement? result)
        {
            OnClose?.Invoke(this, result);
        }
    }
}
