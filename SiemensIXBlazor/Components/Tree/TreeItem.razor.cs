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
using Newtonsoft.Json;
using SiemensIXBlazor.Interops;
using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Components.Tree;

public partial class TreeItem
{
    private readonly string ElementId = $"tree-item-{Guid.NewGuid():N}";
    private Lazy<Task<IJSObjectReference>>? _moduleTask;
    private BaseInterop? _interop;
    private string? _lastContext;
    private bool? _lastHasChildren;
    private bool? _lastDisabled;

    [Parameter]
    public string? Text { get; set; }

    [Parameter]
    public bool HasChildren { get; set; }

    [Parameter]
    public TreeContextNode? Context { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public string? AriaLabelChevronIcon { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public EventCallback ToggleEvent { get; set; }

    [Parameter]
    public EventCallback ItemClickEvent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _interop = new(JSRuntime);
            await _interop.AddEventListener(this, ElementId, "toggle", nameof(Toggle), includeDetail: false);
            await _interop.AddEventListener(this, ElementId, "itemClick", nameof(ItemClick), includeDetail: false);
        }

        await ApplyPropertiesAsync();
    }

    private async Task<IJSObjectReference> GetModuleAsync()
    {
        _moduleTask ??= new(() => JSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Siemens.IX.Blazor/js/siemens-ix/interops/treeInterop.js").AsTask());

        return await _moduleTask.Value;
    }

    private async Task ApplyPropertiesAsync()
    {
        var module = await GetModuleAsync();
        var context = Context is null ? null : JsonConvert.SerializeObject(Context);

        if (!string.Equals(_lastContext, context, StringComparison.Ordinal))
        {
            await module.InvokeVoidAsync("setTreeItemContext", ElementId, context);
            _lastContext = context;
        }

        if (_lastHasChildren != HasChildren)
        {
            await module.InvokeVoidAsync("setTreeItemProperty", ElementId, "hasChildren", HasChildren);
            _lastHasChildren = HasChildren;
        }

        if (_lastDisabled != Disabled)
        {
            await module.InvokeVoidAsync("setTreeItemProperty", ElementId, "disabled", Disabled);
            _lastDisabled = Disabled;
        }
    }

    [JSInvokable]
    public async Task Toggle()
    {
        await ToggleEvent.InvokeAsync();
    }

    [JSInvokable]
    public async Task ItemClick()
    {
        await ItemClickEvent.InvokeAsync();
    }
}
