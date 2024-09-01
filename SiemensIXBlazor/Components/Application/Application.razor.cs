// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

namespace SiemensIXBlazor.Components;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SiemensIXBlazor.Enums;
using SiemensIXBlazor.Interops;
using SiemensIXBlazor.Objects.Application;

public partial class Application
{
    private Lazy<Task<IJSObjectReference>>? moduleTask;
    private BaseInterop? _interop;
    private AppSwitchConfig _appSwitchConfig;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter, EditorRequired]
    public string Id { get; set; } = string.Empty;
    [Parameter]
    public string[] Breakpoints { get; set; } = ["sm", "md", "lg"];
    [Parameter]
    public ForceBreakpoint? ForceBreakpoint { get; set; }
    [Parameter]
    public string? Theme { get; set; }
    [Parameter]
    public bool ThemeSystemAppearance { get; set; } = false;
    public AppSwitchConfig AppSwitchConfig
    {
        get => _appSwitchConfig;
        set
        {
            _appSwitchConfig = value;
            InitialParameter("setApplicationConfig", _appSwitchConfig);
        }
    }

    private void InitialParameter(string functionName, object param)
    {

        moduleTask = new(() => JSRuntime.InvokeAsync<IJSObjectReference>(
            "import", $"./_content/Siemens.IX.Blazor/js/interops/applicationInterop.js").AsTask());

        Task.Run(async () =>
        {
            var module = await moduleTask.Value;
            if (module != null)
            {
                await module.InvokeVoidAsync(functionName, Id, JsonConvert.SerializeObject(param, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
            }
        });
    }
}