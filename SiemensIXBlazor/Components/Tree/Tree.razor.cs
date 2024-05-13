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
using System.Dynamic;
using System.Text.Json;

namespace SiemensIXBlazor.Components.Tree
{
    public partial class Tree
    {
        private Dictionary<string, TreeNode>? _treeModel;
        private Dictionary<string, TreeContextNode>? _treeContext;
        private Lazy<Task<IJSObjectReference>>? moduleTask;
        private BaseInterop? _interop;

        [Parameter, EditorRequired]
        public string Id { get; set; } = string.Empty;
        public Dictionary<string, TreeNode>? TreeModel 
        {
            get => _treeModel;
            set
            {
                _treeModel = value;
                InitialParameter("setTreeModel", _treeModel);
            } 
        }
        public Dictionary<string, TreeContextNode>? TreeContext 
        {
            get => _treeContext;
            set
            {
                _treeContext = value;
                InitialParameter("setTreeContext", _treeContext);
            }
        }
        [Parameter]
        public string? Root { get; set; }
        [Parameter]
        public EventCallback<Dictionary<string, TreeContextNode>> ContextChangedEvent { get; set; }
        [Parameter]
        public EventCallback NodeRemovedEvent { get; set; }
        [Parameter]
        public EventCallback<string> NodeClickedEvent { get; set; }
        [Parameter]
        public EventCallback<TreeNodeToggledEventResult> NodeToggledEvent { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                _interop = new(JSRuntime);

                Task.Run(async () =>
                {
                    await _interop.AddEventListener(this, Id, "contextChange", "ContextChanged");
                    await _interop.AddEventListener(this, Id, "nodeRemoved", "NodeRemoved");
                    await _interop.AddEventListener(this, Id, "nodeClicked", "NodeClicked");
                    await _interop.AddEventListener(this, Id, "nodeToggled", "NodeToggled");
                });
            }
        }

        private void InitialParameter(string functionName, object param)
        {

            moduleTask = new(() => JSRuntime.InvokeAsync<IJSObjectReference>(
                "import", $"./_content/SiemensIXBlazor/js/interops/treeInterop.js").AsTask());

            Task.Run(async () =>
            {
                var module = await moduleTask.Value;
                if (module != null)
                {
                    var serObj = JsonConvert.SerializeObject(param);
                    await module.InvokeVoidAsync(functionName, Id, JsonConvert.SerializeObject(param));
                }
            });
        }

        [JSInvokable]
        public async Task ContextChanged(JsonElement context)
        {
            string jsonDataText = context.GetRawText();
            Dictionary<string, TreeContextNode>? changedContext = JsonConvert.DeserializeObject<Dictionary<string, TreeContextNode>>(jsonDataText);
            await ContextChangedEvent.InvokeAsync(changedContext);
        }

        [JSInvokable]
        public async Task NodeRemoved()
        {
            await NodeRemovedEvent.InvokeAsync();
        }

        [JSInvokable]
        public async Task NodeClicked(string label)
        {
            await NodeClickedEvent.InvokeAsync(label);
        }

        [JSInvokable]
        public async Task NodeToggled(JsonElement toggledNode)
        {
            string jsonDataText = toggledNode.GetRawText();
            TreeNodeToggledEventResult result = JsonConvert.DeserializeObject<TreeNodeToggledEventResult>(jsonDataText);
            await NodeToggledEvent.InvokeAsync(result);
        }
    }
}
