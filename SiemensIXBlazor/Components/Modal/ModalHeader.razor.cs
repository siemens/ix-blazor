// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace SiemensIXBlazor.Components.Modal
{
    /// <summary>
    /// Blazor wrapper for ix-modal-header web component
    /// Provides header section for modal dialogs with icon support
    /// </summary>
    public partial class ModalHeader : IXBaseComponent, IAsyncDisposable
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// Hide the close button.
        /// </summary>
        [Parameter]
        public bool HideClose { get; set; }

        /// <summary>
        /// Icon of the header.
        /// </summary>
        [Parameter]
        public string? Icon { get; set; }

        /// <summary>
        /// ARIA label for the icon.
        /// </summary>
        [Parameter]
        public string? AriaLabelIcon { get; set; }

        /// <summary>
        /// ARIA label for the close icon button.
        /// </summary>
        [Parameter]
        public string AriaLabelCloseIconButton { get; set; } = "Close modal";

        /// <summary>
        /// Icon color.
        /// </summary>
        [Parameter]
        public string? IconColor { get; set; }

        /// <summary>
        /// Raised when the close icon is clicked.
        /// </summary>
        [Parameter]
        public EventCallback<MouseEventArgs> CloseClickEvent { get; set; }

        private readonly string ElementId = $"ix-modal-header-{Guid.NewGuid():N}";
        private DotNetObjectReference<ModalHeader>? _dotNetReference;
        private bool _listenerAttached;

        protected override async Task OnAfterRenderAsync(bool _)
        {
            if (!HideClose && !_listenerAttached)
            {
                _dotNetReference = DotNetObjectReference.Create(this);
                await JSRuntime.InvokeVoidAsync(
                    "siemensIXInterop.modalHeader.attach", ElementId, _dotNetReference);
                _listenerAttached = true;
            }
            else if (HideClose && _listenerAttached)
            {
                await JSRuntime.InvokeVoidAsync(
                    "siemensIXInterop.modalHeader.detach", ElementId);
                _dotNetReference?.Dispose();
                _dotNetReference = null;
                _listenerAttached = false;
            }
        }

        [Inject]
        private IJSRuntime JSRuntime { get; set; } = default!;

        [JSInvokable]
        public Task CloseClick(MouseEventArgs eventArgs)
        {
            return CloseClickEvent.InvokeAsync(eventArgs);
        }

        public async ValueTask DisposeAsync()
        {
            if (_dotNetReference is not null)
            {
                await JSRuntime.InvokeVoidAsync("siemensIXInterop.modalHeader.detach", ElementId);
                _dotNetReference.Dispose();
            }
        }
    }
}
