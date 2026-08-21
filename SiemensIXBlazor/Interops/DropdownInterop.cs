// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.JSInterop;

namespace SiemensIXBlazor.Interops;

internal sealed class DropdownInterop(IJSRuntime jsRuntime, string elementId) : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
        "import", "./_content/Siemens.IX.Blazor/js/siemens-ix/interops/dropdownInterop.js").AsTask());
    private DotNetObjectReference<object>? dotNetReference;

    public async Task AttachEventAsync(object caller, string elementId, string eventName, string callbackName)
    {
        dotNetReference ??= DotNetObjectReference.Create(caller);
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("attachEvent", dotNetReference, elementId, eventName, callbackName);
    }

    public async Task SetPropertyAsync(string elementId, string propertyName, object propertyValue)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("setProperty", elementId, propertyName, propertyValue);
    }

    public async Task UpdatePositionAsync(string elementId)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("updatePosition", elementId);
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.InvokeVoidAsync("detachEvents", elementId);
                await module.DisposeAsync();
            }
        }
        catch (JSDisconnectedException)
        {
        }
        finally
        {
            dotNetReference?.Dispose();
            dotNetReference = null;
        }
    }
}
