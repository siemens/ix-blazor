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
using SiemensIXBlazor.Objects.Application;

public partial class Application
{
    private Lazy<Task<IJSObjectReference>>? moduleTask;
    private AppSwitchConfig? _appSwitchConfig;
    private bool _hasRendered;
    private int _appSwitchConfigVersion;
    private int _appliedAppSwitchConfigVersion;
    private string[] _breakpoints = ["sm", "md", "lg"];

    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter, EditorRequired]
    public string Id { get; set; } = string.Empty;
    [Parameter]
    public string[] Breakpoints
    {
        get => _breakpoints;
        set
        {
            _breakpoints = value ?? [];

            if (_hasRendered)
            {
                _ = ApplyBreakpointsAsync(_breakpoints);
            }
        }
    }
    [Parameter]
    public ForceBreakpoint? ForceBreakpoint { get; set; }
    [Parameter]
    public string? Theme { get; set; }
    [Parameter]
    public ColorSchema ColorSchema { get; set; } = ColorSchema.System;

    [Parameter]
    public AppSwitchConfig? AppSwitchConfig
    {
        get => _appSwitchConfig;
        set
        {
            if (ReferenceEquals(_appSwitchConfig, value))
            {
                return;
            }

            _appSwitchConfig = value;
            _appSwitchConfigVersion++;

            if (_hasRendered)
            {
                _ = ApplyApplicationConfigAsync(_appSwitchConfigVersion, value);
            }
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var module = await GetModuleAsync();
            RegisterDisposable(module);
            await module.InvokeVoidAsync("setBreakpoints", Id, _breakpoints);
            _hasRendered = true;

            if (_appliedAppSwitchConfigVersion != _appSwitchConfigVersion)
            {
                await ApplyApplicationConfigAsync(_appSwitchConfigVersion, _appSwitchConfig);
            }
        }
    }

    private async Task ApplyBreakpointsAsync(string[] breakpoints)
    {
        try
        {
            var module = await GetModuleAsync();
            RegisterDisposable(module);
            await module.InvokeVoidAsync("setBreakpoints", Id, breakpoints);
        }
        catch (JSDisconnectedException)
        {
            // The component can be disposed while the module is loading.
        }
    }

    private async Task ApplyApplicationConfigAsync(int version, AppSwitchConfig? config)
    {
        try
        {
            var module = await GetModuleAsync();
            RegisterDisposable(module);
            if (version != _appSwitchConfigVersion)
            {
                return;
            }

            await module.InvokeVoidAsync("setApplicationConfig", Id,
                JsonConvert.SerializeObject(config, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));

            _appliedAppSwitchConfigVersion = version;
        }
        catch (JSDisconnectedException)
        {
            // The component can be disposed while the module is loading.
        }
    }

    private Task<IJSObjectReference> GetModuleAsync()
    {
        moduleTask ??= new(() => JSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Siemens.IX.Blazor/js/siemens-ix/interops/applicationInterop.js").AsTask());

        return moduleTask.Value;
    }

}
