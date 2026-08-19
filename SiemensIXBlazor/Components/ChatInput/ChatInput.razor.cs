// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiemensIXBlazor.Enums.Chat;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components;

public partial class ChatInput : IAsyncDisposable
{
    [Inject]
    private IJSRuntime JSRuntime { get; set; } = default!;

    [Parameter]
    public ChatInputState State { get; set; } = ChatInputState.Input;

    [Parameter]
    public string? Name { get; set; }

    [Parameter]
    public string Placeholder { get; set; } = "Enter a command, question or topic...";

    [Parameter]
    public string Value { get; set; } = string.Empty;

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public bool Readonly { get; set; }

    [Parameter]
    public string TextareaLabel { get; set; } = "Chat input";

    [Parameter]
    public int? MaxLength { get; set; }

    [Parameter]
    public int? CharacterLimit { get; set; }

    [Parameter]
    public string I18nCharacterLimitReached { get; set; } = "Character limit reached ({current} / {limit} characters)";

    [Parameter]
    public string I18nCharacterLimitWarning { get; set; } = "You're nearing the limit ({current} / {limit} characters)";

    [Parameter]
    public double CharacterLimitWarningThreshold { get; set; } = 0.9;

    [Parameter]
    public int MinRows { get; set; } = 1;

    [Parameter]
    public int MaxRows { get; set; } = 6;

    [Parameter]
    public bool InsertLineBreakOnEnter { get; set; }

    [Parameter]
    public string Disclaimer { get; set; } = "This content is AI-generated. Always verify the information for accuracy.";

    [Parameter]
    public RenderFragment? Attachments { get; set; }

    [Parameter]
    public RenderFragment? FollowUp { get; set; }

    [Parameter]
    public RenderFragment? Start { get; set; }

    [Parameter]
    public RenderFragment? End { get; set; }

    [Parameter]
    public EventCallback<string> ValueChange { get; set; }

    [Parameter]
    public EventCallback IxBlur { get; set; }

    [Parameter]
    public EventCallback<string> IxChange { get; set; }

    [Parameter]
    public EventCallback<string> PromptSubmit { get; set; }

    private readonly string _generatedId = $"chat-input-{Guid.NewGuid():N}";
    private BaseInterop? _interop;

    private string ElementId => _generatedId;

    private string StateAttribute => State switch
    {
        ChatInputState.Input => "input",
        ChatInputState.Processing => "processing",
        _ => throw new ArgumentOutOfRangeException(),
    };

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        _interop = new(JSRuntime);
        await _interop.AddEventListener(this, ElementId, "valueChange", nameof(ValueChanged));
        await _interop.AddEventListener(this, ElementId, "ixBlur", nameof(Blurred), includeDetail: false);
        await _interop.AddEventListener(this, ElementId, "ixChange", nameof(Changed));
        await _interop.AddEventListener(this, ElementId, "promptSubmit", nameof(PromptSubmitted));
    }

    public async Task FocusInputAsync()
    {
        _interop ??= new(JSRuntime);
        await _interop.InvokeElementMethodAsync(ElementId, "focusInput");
    }

    public async Task<IJSObjectReference?> GetNativeInputElementAsync()
    {
        _interop ??= new(JSRuntime);
        return await _interop.InvokeElementMethodAsync<IJSObjectReference>(ElementId, "getNativeInputElement");
    }

    [JSInvokable]
    public async Task ValueChanged(JsonElement value)
    {
        string nextValue = value.ValueKind == JsonValueKind.String ? value.GetString() ?? string.Empty : string.Empty;
        Value = nextValue;
        await ValueChange.InvokeAsync(nextValue);
    }

    [JSInvokable]
    public Task Blurred() => IxBlur.InvokeAsync();

    [JSInvokable]
    public async Task Changed(JsonElement value)
    {
        string nextValue = value.ValueKind == JsonValueKind.String ? value.GetString() ?? string.Empty : string.Empty;
        await IxChange.InvokeAsync(nextValue);
    }

    [JSInvokable]
    public async Task PromptSubmitted(JsonElement value)
    {
        string nextValue = value.ValueKind == JsonValueKind.String ? value.GetString() ?? string.Empty : string.Empty;
        await PromptSubmit.InvokeAsync(nextValue);
    }

    public async ValueTask DisposeAsync()
    {
        if (_interop is not null)
        {
            await _interop.DisposeAsync();
        }
    }
}
