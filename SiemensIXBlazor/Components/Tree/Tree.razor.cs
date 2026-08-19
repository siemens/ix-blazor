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
using Newtonsoft.Json;
using SiemensIXBlazor.Interops;
using SiemensIXBlazor.Objects;
using System.Text.Json;

namespace SiemensIXBlazor.Components.Tree;

public partial class Tree
{
    private Lazy<Task<IJSObjectReference>>? _moduleTask;
    private BaseInterop? _interop;
    private string? _lastModel;
    private string? _lastContext;
    private bool? _lastToggleOnItemClick;

    [Parameter, EditorRequired]
    public string Id { get; set; } = string.Empty;

    [Parameter]
    public Dictionary<string, TreeNode> Model { get; set; } = new();

    [Parameter]
    public Dictionary<string, TreeContextNode> Context { get; set; } = new();

    [Parameter]
    public string Root { get; set; } = "root";

    [Parameter]
    public bool ToggleOnItemClick { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public EventCallback<Dictionary<string, TreeContextNode>> ContextChangedEvent { get; set; }

    [Parameter]
    public EventCallback NodeRemovedEvent { get; set; }

    [Parameter]
    public EventCallback<string> NodeClickedEvent { get; set; }

    [Parameter]
    public EventCallback<TreeNodeToggledEventResult> NodeToggledEvent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _interop = new(JSRuntime);

            await _interop.AddEventListener(this, Id, "contextChange", nameof(ContextChanged));
            await _interop.AddEventListener(this, Id, "nodeRemoved", nameof(NodeRemoved), includeDetail: false);
            await _interop.AddEventListener(this, Id, "nodeClicked", nameof(NodeClicked));
            await _interop.AddEventListener(this, Id, "nodeToggled", nameof(NodeToggled));
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
        var model = JsonConvert.SerializeObject(Model ?? new Dictionary<string, TreeNode>());
        var context = JsonConvert.SerializeObject(Context ?? new Dictionary<string, TreeContextNode>());

        if (!string.Equals(_lastModel, model, StringComparison.Ordinal))
        {
            await module.InvokeVoidAsync("setTreeModel", Id, model);
            _lastModel = model;
        }

        if (!string.Equals(_lastContext, context, StringComparison.Ordinal))
        {
            await module.InvokeVoidAsync("setTreeContext", Id, context);
            _lastContext = context;
        }

        if (_lastToggleOnItemClick != ToggleOnItemClick)
        {
            await module.InvokeVoidAsync("setToggleOnItemClick", Id, ToggleOnItemClick);
            _lastToggleOnItemClick = ToggleOnItemClick;
        }
    }

    [JSInvokable]
    public async Task ContextChanged(JsonElement context)
    {
        var changedContext = JsonConvert.DeserializeObject<Dictionary<string, TreeContextNode>>(context.GetRawText())
            ?? new Dictionary<string, TreeContextNode>();
        await ContextChangedEvent.InvokeAsync(changedContext);
    }

    [JSInvokable]
    public async Task NodeRemoved()
    {
        await NodeRemovedEvent.InvokeAsync();
    }

    [JSInvokable]
    public async Task NodeClicked(string nodeId)
    {
        await NodeClickedEvent.InvokeAsync(nodeId);
    }

    [JSInvokable]
    public async Task NodeToggled(JsonElement toggledNode)
    {
        var result = JsonConvert.DeserializeObject<TreeNodeToggledEventResult>(toggledNode.GetRawText())
            ?? new TreeNodeToggledEventResult();
        await NodeToggledEvent.InvokeAsync(result);
    }

    public async Task MarkItemsAsDirty(params string[] itemIdentifiers)
    {
        if (itemIdentifiers == null || itemIdentifiers.Length == 0)
        {
            return;
        }

        var module = await GetModuleAsync();
        await module.InvokeVoidAsync("markItemsAsDirty", Id, itemIdentifiers);
    }

    public Task MarkItemAsDirty(params string[] itemIdentifiers)
    {
        return MarkItemsAsDirty(itemIdentifiers);
    }

    public async Task RefreshTree(RefreshTreeOptions? options = null)
    {
        var module = await GetModuleAsync();
        await module.InvokeVoidAsync("refreshTree", Id, options ?? new RefreshTreeOptions());
    }
}
