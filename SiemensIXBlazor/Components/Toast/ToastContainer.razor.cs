// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using SiemensIXBlazor.Enums;
using SiemensIXBlazor.Objects;
using System.Text.Json;

namespace SiemensIXBlazor.Components
{
    public partial class ToastContainer : IAsyncDisposable
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public ToastPosition Position { get; set; } = ToastPosition.BottomRight;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        private Lazy<Task<IJSObjectReference>>? _moduleTask;
        private DotNetObjectReference<ToastContainer>? _dotNetReference;
        private readonly Dictionary<string, ToastResult> _toastResults = [];

        public async Task<ToastResult> ShowToast(ToastConfig config)
        {
            ArgumentNullException.ThrowIfNull(config);

            var module = await GetModule();
            _dotNetReference ??= DotNetObjectReference.Create(this);

            var handle = await module.InvokeAsync<string>(
                "showToast",
                _dotNetReference,
                Id,
                JsonConvert.SerializeObject(config));

            var result = new ToastResult(module, handle);
            _toastResults[handle] = result;
            return result;
        }

        [JSInvokable]
        public Task ToastClosed(string handle, JsonElement? value)
        {
            if (_toastResults.Remove(handle, out var result))
            {
                result.NotifyClosed(value);
            }

            return Task.CompletedTask;
        }

        private Task<IJSObjectReference> GetModule()
        {
            _moduleTask ??= new(() => JSRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/Siemens.IX.Blazor/js/siemens-ix/interops/toastInterop.js").AsTask());

            return _moduleTask.Value;
        }

        public override async ValueTask DisposeAsync()
        {
            try
            {
                if (_moduleTask is not null && _moduleTask.IsValueCreated)
                {
                    var module = await _moduleTask.Value;
                    await module.InvokeVoidAsync("dispose", Id);
                    await module.DisposeAsync();
                }
            }
            catch (JSDisconnectedException)
            {
            }
            finally
            {
                _toastResults.Clear();
                _dotNetReference?.Dispose();
                _dotNetReference = null;
            }
        }
    }
}
