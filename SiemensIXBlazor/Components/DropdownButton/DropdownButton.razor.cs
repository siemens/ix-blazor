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
using SiemensIXBlazor.Enums.Button;
using SiemensIXBlazor.Enums.DropdownButton;
using SiemensIXBlazor.Helpers;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class DropdownButton : IAsyncDisposable
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; } = default!;

        private static long _elementSequence;
        private string ElementId { get; } = $"dropdown-button-{Interlocked.Increment(ref _elementSequence)}";

        [Parameter]
        public string? AriaLabelDropdownButton { get; set; }
        [Parameter]
        public bool Disabled { get; set; } = false;
        [Parameter]
        public bool EnableTopLayer { get; set; } = false;
        [Parameter]
        public string? Icon { get; set; }
        [Parameter]
        public string? Label { get; set; }
        [Parameter]
        public DropdownButtonPlacement? Placement { get; set; }
        [Parameter]
        public ButtonVariant Variant { get; set; } = ButtonVariant.primary;
        [Parameter]
        public object CloseBehavior { get; set; } = DropdownButtonCloseBehavior.both;
        [Parameter]
        public bool FocusCheckedItem { get; set; } = false;
        [Parameter]
        public RenderFragment? ButtonLabelContent { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public EventCallback<bool> ShowChangeEvent { get; set; }
        [Parameter]
        public EventCallback<bool> ShowChangedEvent { get; set; }

        private DropdownInterop? _interop;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime, ElementId);
                await _interop.AttachEventAsync(this, ElementId, "showChange", nameof(ShowChange));
                await _interop.AttachEventAsync(this, ElementId, "showChanged", nameof(ShowChanged));
            }

            if (CloseBehavior is bool)
            {
                await _interop!.SetPropertyAsync(ElementId, "closeBehavior", CloseBehavior);
            }
        }

        [JSInvokable]
        public async Task ShowChange(bool value)
        {
            await ShowChangeEvent.InvokeAsync(value);
        }

        [JSInvokable]
        public async Task ShowChanged(bool value)
        {
            await ShowChangedEvent.InvokeAsync(value);
        }

        public async ValueTask DisposeAsync()
        {
            if (_interop is not null)
            {
                await _interop.DisposeAsync();
            }
        }

        private string CloseBehaviorAttribute => CloseBehavior switch
        {
            DropdownButtonCloseBehavior value => EnumParser<DropdownButtonCloseBehavior>.EnumToString(value),
            bool value => value ? "true" : "false",
            string value => value,
            _ => throw new ArgumentException("CloseBehavior must be a dropdown close behavior or boolean.")
        };
    }
}
