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
using SiemensIXBlazor.Enums.Popover;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components;

public partial class Popover
{
    [Parameter, EditorRequired]
    public string Id { get; set; } = string.Empty;

    [Parameter]
    public string? Trigger { get; set; }

    [Parameter]
    public bool Show { get; set; }

    [Parameter]
    public PopoverPlacement Placement { get; set; } = PopoverPlacement.Bottom;

    [Parameter]
    public bool HasSpike { get; set; }

    [Parameter]
    public PopoverTriggerMode TriggerMode { get; set; } = PopoverTriggerMode.Click;

    [Parameter]
    public bool CloseOnClickOutside { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public EventCallback<bool> ShowChangeEvent { get; set; }

    [Parameter]
    public EventCallback<bool> ShowChangedEvent { get; set; }

    private BaseInterop _interop = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        _interop = new(JSRuntime);
        await _interop.AddEventListener(this, Id, "showChange", "ShowChange");
        await _interop.AddEventListener(this, Id, "showChanged", "ShowChanged");
    }

    [JSInvokable]
    public Task ShowChange(bool value) => ShowChangeEvent.InvokeAsync(value);

    [JSInvokable]
    public Task ShowChanged(bool value) => ShowChangedEvent.InvokeAsync(value);

    public Task ShowPopover() => _interop.InvokeElementMethodAsync(Id, "showPopover");

    public Task HidePopover() => _interop.InvokeElementMethodAsync(Id, "hidePopover");
}
