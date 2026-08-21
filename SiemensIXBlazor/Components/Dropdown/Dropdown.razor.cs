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
using SiemensIXBlazor.Enums.DropdownButton;
using SiemensIXBlazor.Helpers;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components
{
    public partial class Dropdown : IAsyncDisposable
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; } = default!;

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        [Parameter]
        public object? Anchor { get; set; }
        [Parameter]
        public object CloseBehavior { get; set; } = DropdownButtonCloseBehavior.both;
        [Parameter]
        public string? Header { get; set; }
        [Parameter]
        public string Placement { get; set; } = "bottom-start";
        [Parameter]
        public string PositioningStrategy { get; set; } = "fixed";
        [Parameter]
        public bool Show { get; set; } = false;
        [Parameter]
        public bool SuppressAutomaticPlacement { get; set; } = false;
        [Parameter]
        public bool SuppressTriggerVisibilityCheck { get; set; } = false;
        [Parameter]
        public bool DisableFocusHandling { get; set; } = false;
        [Parameter]
        public bool DisableFocusTrap { get; set; } = false;
        [Parameter]
        public bool EnableTopLayer { get; set; } = false;
        [Parameter]
        public bool FocusCheckedItem { get; set; } = false;
        [Parameter]
        public object? Trigger { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public EventCallback<bool> ShowChangeEvent { get; set; }
        [Parameter]
        public EventCallback<bool> ShowChangedEvent { get; set; }

        private DropdownInterop? _interop;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime, Id);

                await _interop.AttachEventAsync(this, Id, "showChange", nameof(ShowChange));
                await _interop.AttachEventAsync(this, Id, "showChanged", nameof(ShowChanged));
            }

            if (Trigger is not null and not string)
            {
                await _interop!.SetPropertyAsync(Id, "trigger", Trigger);
            }

            if (Anchor is not null and not string)
            {
                await _interop!.SetPropertyAsync(Id, "anchor", Anchor);
            }

            if (CloseBehavior is bool)
            {
                await _interop!.SetPropertyAsync(Id, "closeBehavior", CloseBehavior);
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

        public async Task UpdatePositionAsync()
        {
            if (_interop is not null)
            {
                await _interop.UpdatePositionAsync(Id);
            }
        }

        public override async ValueTask DisposeAsync()
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
