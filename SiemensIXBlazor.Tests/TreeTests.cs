// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using Bunit;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Components.Tree;
using SiemensIXBlazor.Objects;
using System.Text.Json;

namespace SiemensIXBlazor.Tests;

public class TreeTests : TestContextBase
{
    [Fact]
    public void Render_WithOfficialTreeProperties_ShouldRenderTreeComponent()
    {
        var model = new Dictionary<string, TreeNode>
        {
            ["root"] = new() { Id = "root", HasChildren = true, Children = ["node"] },
            ["node"] = new() { Id = "node", Data = new TreeData { Name = "Node" } }
        };

        var cut = RenderComponent<Tree>(parameters => parameters
            .Add(p => p.Id, "tree-id")
            .Add(p => p.Root, "root")
            .Add(p => p.Model, model)
            .Add(p => p.ToggleOnItemClick, true)
            .AddChildContent("Slotted content"));

        var tree = cut.Find("ix-tree");
        Assert.Equal("tree-id", tree.GetAttribute("id"));
        Assert.Equal("root", tree.GetAttribute("root"));
        Assert.Contains("Slotted content", cut.Markup);
    }

    [Fact]
    public async Task ContextChanged_ShouldDeserializeDisabledState()
    {
        var cut = RenderComponent<Tree>(parameters => parameters
            .Add(p => p.Id, "tree-id")
            .Add(p => p.ContextChangedEvent, EventCallback.Factory.Create<Dictionary<string, TreeContextNode>>(
                this, _ => { })));

        Dictionary<string, TreeContextNode>? receivedContext = null;
        cut.SetParametersAndRender(parameters => parameters
            .Add(p => p.ContextChangedEvent, EventCallback.Factory.Create<Dictionary<string, TreeContextNode>>(
                this, context => receivedContext = context)));

        using var document = JsonDocument.Parse("{\"node1\":{\"isExpanded\":true,\"isSelected\":false,\"isDisabled\":true}}");
        await cut.Instance.ContextChanged(document.RootElement);

        Assert.NotNull(receivedContext);
        Assert.True(receivedContext!["node1"].IsExpanded);
        Assert.True(receivedContext["node1"].IsDisabled);
    }

    [Fact]
    public async Task NodeToggled_ShouldExposeOfficialIsExpandedProperty()
    {
        var cut = RenderComponent<Tree>(parameters => parameters
            .Add(p => p.Id, "tree-id"));
        TreeNodeToggledEventResult? result = null;

        cut.SetParametersAndRender(parameters => parameters
            .Add(p => p.NodeToggledEvent, EventCallback.Factory.Create<TreeNodeToggledEventResult>(
                this, value => result = value)));

        using var document = JsonDocument.Parse("{\"id\":\"node1\",\"isExpanded\":true}");
        await cut.Instance.NodeToggled(document.RootElement);

        Assert.NotNull(result);
        Assert.Equal("node1", result!.Id);
        Assert.True(result.IsExpanded);
    }

    [Fact]
    public async Task TreeMethods_ShouldAcceptOfficialMethodShapes()
    {
        var cut = RenderComponent<Tree>(parameters => parameters
            .Add(p => p.Id, "tree-methods"));

        await cut.Instance.MarkItemsAsDirty("item1", "item2");
        await cut.Instance.RefreshTree(new RefreshTreeOptions { Force = true });
    }

    [Fact]
    public void TreeNode_ShouldSerializeDisabledProperty()
    {
        var node = new TreeNode { Id = "node", Disabled = true };
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(node);

        Assert.Contains("\"disabled\":true", json);
    }

    [Fact]
    public async Task TreeItem_ShouldRenderPropertiesAndInvokeEvents()
    {
        var toggled = false;
        var clicked = false;

        var cut = RenderComponent<TreeItem>(parameters => parameters
            .Add(p => p.Text, "Node")
            .Add(p => p.HasChildren, true)
            .Add(p => p.Disabled, true)
            .Add(p => p.Context, new TreeContextNode { IsSelected = true, IsDisabled = true })
            .Add(p => p.ToggleEvent, EventCallback.Factory.Create(this, () => toggled = true))
            .Add(p => p.ItemClickEvent, EventCallback.Factory.Create(this, () => clicked = true))
            .AddChildContent("Custom content"));

        var item = cut.Find("ix-tree-item");
        Assert.Equal("Node", item.GetAttribute("text"));
        Assert.Contains("Custom content", cut.Markup);

        await cut.Instance.Toggle();
        await cut.Instance.ItemClick();

        Assert.True(toggled);
        Assert.True(clicked);
    }
}
