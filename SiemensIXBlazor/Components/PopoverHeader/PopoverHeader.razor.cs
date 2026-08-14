// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components;

public partial class PopoverHeader
{
    [Parameter, EditorRequired]
    public string Id { get; set; } = string.Empty;

    [Parameter]
    public string? Icon { get; set; }

    [Parameter]
    public string? IconColor { get; set; }

    [Parameter]
    public bool HideClose { get; set; }

    [Parameter]
    public string? AriaLabelCloseIconButton { get; set; } = "Close";

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public RenderFragment? AdditionalItems { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> CloseClickEvent { get; set; }

    private BaseInterop _interop = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        _interop = new(JSRuntime);
        await _interop.AddEventListener(this, Id, "closeClick", "CloseClick");
    }

    [JSInvokable]
    public Task CloseClick(MouseEventArgs value) => CloseClickEvent.InvokeAsync(value);
}
