// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components;

public partial class FilterChip
{
    [Parameter, EditorRequired]
    public string Id { get; set; } = string.Empty;

    [Parameter]
    public string? AriaLabelCloseIconButton { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public bool HideCloseButton { get; set; }

    [Parameter]
    public bool Readonly { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public EventCallback CloseClickEvent { get; set; }

    private BaseInterop? _interop;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        _interop ??= new(JSRuntime);
        await _interop.AddEventListener(this, Id, "closeClick", nameof(CloseClick), includeDetail: false);
    }

    [JSInvokable]
    public Task CloseClick() => CloseClickEvent.InvokeAsync();
}
