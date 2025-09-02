// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Razor.TagHelpers;
using SiemensIXBlazor.Objects;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SiemensIXBlazor.Playground.Components.Pages.NavigationAndHierarchy.Tree;


public partial class Tree
{
    private int activeTab = 0;
    private SiemensIXBlazor.Components.Tree.Tree treeComponent;
    private SiemensIXBlazor.Components.Tree.Tree treeComponentforCustom;
    private SiemensIXBlazor.Objects.TreeContextNode treeContextNode;
    
    public string ContentForBasic { get; private set; } = @"
        <div class=""tree-style"">
            <SiemensIXBlazor.Components.Tree.Tree Root=""root"" Id=""tree"" @ref=""treeComponent"" @onclick=""setTreeModel""></SiemensIXBlazor.Components.Tree.Tree>
        </div>";

    public string ContentForCustomTreeNode { get; private set; } = @"
        <div class=""tree-style"">
            <SiemensIXBlazor.Components.Button Id=""expand-button"" Ghost=""true"" @onclick=""HandleExpandButton"">
                Expand Button
            </SiemensIXBlazor.Components.Button>
            <SiemensIXBlazor.Components.Tree.Tree Root=""root"" Id=""custom-tree"" @ref=""treeComponentforCustom""></SiemensIXBlazor.Components.Tree.Tree>
        </div>";
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender && treeComponent != null)
        {
            setTreeModel();
            StateHasChanged();
        }
    }
    public async Task setTreeModel()
    {
        Dictionary<string, TreeNode> treeNodes = new();

        treeNodes.Add("root", new TreeNode()
        {
            Id = "root",
            HasChildren = true,
            Children = new List<string>() { "sample" }
        });

        treeNodes.Add("sample", new TreeNode()
        {
            Id = "sample",
            Data = new TreeData()
            {
                Name = "Sample"
            },
            HasChildren = true,
            Children = new List<string>() { "sample-child-1", "sample-child-2" }
        });

        treeNodes.Add("sample-child-1", new TreeNode()
        {
            Id = "sample-child-1",
            Data = new TreeData()
            {
                Name = "Sample Child 1"
            },
            HasChildren = false,
            Children = new List<string>() { }
        });

        treeNodes.Add("sample-child-2", new TreeNode()
        {
            Id = "sample-child-2",
            Data = new TreeData()
            {
                Name = "Sample Child 2"
            },
            HasChildren = false,
            Children = new List<string>() { }
        });

        treeComponent.TreeModel = treeNodes;
        treeComponentforCustom.TreeModel = treeNodes;
        StateHasChanged();
    }

    public async Task HandleExpandButton()
    {
        Dictionary<string, TreeContextNode> context = new Dictionary<string, TreeContextNode>();

        context.Add("sample", new TreeContextNode
        {
            IsExpanded = true,
            IsSelected = false
        });

        context.Add("sample-child-2", new TreeContextNode
        {
            IsSelected = true,
            IsExpanded = false
        });
        treeComponentforCustom.TreeContext = context;
    }
    }


