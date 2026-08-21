// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Enums;

namespace SiemensIXBlazor.Components
{
    public partial class Toast : IAsyncDisposable
    {
        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public RenderFragment? ActionContent { get; set; }
        [Parameter]
        public ToastType Type { get; set; } = ToastType.Info;
        [Parameter]
        public string? ToastTitle { get; set; }
        [Parameter]
        public int AutoCloseDelay { get; set; } = 5000;
        [Parameter]
        public bool PreventAutoClose { get; set; } = false;
        [Parameter]
        public string? Icon { get; set; }
        [Parameter]
        public string? IconColor { get; set; }
        [Parameter]
        public bool HideIcon { get; set; } = false;
        [Parameter]
        public string AriaLabelCloseIconButton { get; set; } = "Close toast";
        [Parameter]
        public EventCallback CloseToastEvent { get; set; }

        private Lazy<Task<IJSObjectReference>>? _moduleTask;
        private DotNetObjectReference<Toast>? _dotNetReference;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _moduleTask = new(() => JSRuntime.InvokeAsync<IJSObjectReference>(
                    "import", "./_content/Siemens.IX.Blazor/js/siemens-ix/interops/toastInterop.js").AsTask());

                _dotNetReference = DotNetObjectReference.Create(this);
                await (await GetModule()).InvokeVoidAsync("listenCloseToast", _dotNetReference, Id);
            }
        }

        public async Task PauseAsync()
        {
            await (await GetModule()).InvokeVoidAsync("pauseToast", Id);
        }

        public async Task ResumeAsync()
        {
            await (await GetModule()).InvokeVoidAsync("resumeToast", Id);
        }

        public async Task<bool> IsPausedAsync()
        {
            return await (await GetModule()).InvokeAsync<bool>("isToastPaused", Id);
        }

        [JSInvokable]
        public async Task CloseToast()
        {
            await CloseToastEvent.InvokeAsync();
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
                    await module.InvokeVoidAsync("removeCloseToast", Id);
                    await module.DisposeAsync();
                }
            }
            catch (JSDisconnectedException)
            {
            }
            finally
            {
                _dotNetReference?.Dispose();
                _dotNetReference = null;
            }
        }
    }
}
