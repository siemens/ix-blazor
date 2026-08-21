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
using SiemensIXBlazor.Enums.Chat;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components;

public partial class ChatAttachment : IAsyncDisposable
{
    private const string DefaultIcon = "data:image/svg+xml;utf8,<?xml version='1.0' encoding='UTF-8'?><svg width='512px' height='512px' viewBox='0 0 512 512' version='1.1' xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink'><desc>txt-document</desc><g id='Page-1' stroke='none' stroke-width='1' fill='none' fill-rule='evenodd'><g id='icon' transform='translate(64.000000, 42.666667)'><path d='M249.9584,7.10542736e-15 L15.2917333,7.10542736e-15 L15.2917333,234.666667 L57.9584,234.666667 L57.9584,192 L57.9584,169.6 L57.9584,42.6666667 L232.251733,42.6666667 L313.9584,124.373333 L313.9584,169.6 L313.9584,192 L313.9584,234.666667 L356.625067,234.666667 L356.625067,106.666667 L249.9584,7.10542736e-15 L249.9584,7.10542736e-15 Z M-1.42108547e-14,277.5744 L-1.42108547e-14,300.1664 L37.056,300.1664 L37.056,405.7024 L65.92,405.7024 L65.92,300.1664 L103.530667,300.1664 L103.530667,277.5744 L-1.42108547e-14,277.5744 Z M217.1712,277.5744 L186.9632,319.345067 L157.1392,277.5744 L123.581867,277.5744 L168.616533,339.9744 L121.2352,405.7024 L155.304533,405.7024 L185.533867,362.929067 L215.912533,405.7024 L250.7072,405.7024 L203.624533,340.699733 L250.173867,277.5744 L217.1712,277.5744 Z M269.2992,277.5744 L269.2992,300.1664 L306.376533,300.1664 L306.376533,405.7024 L335.240533,405.7024 L335.240533,300.1664 L372.829867,300.1664 L372.829867,277.5744 L269.2992,277.5744 Z' id='TXT'/></g></g></svg>";

    [Inject]
    private IJSRuntime JSRuntime { get; set; } = default!;

    [Parameter]
    public string FileName { get; set; } = string.Empty;

    [Parameter]
    public ChatAttachmentStatus Status { get; set; } = ChatAttachmentStatus.Default;

    [Parameter]
    public string Icon { get; set; } = DefaultIcon;

    [Parameter]
    public bool HideRemoveButton { get; set; }

    [Parameter]
    public bool PreviewSupported { get; set; }

    [Parameter]
    public string RemoveAriaLabel { get; set; } = "Remove attachment";

    [Parameter]
    public EventCallback AttachmentClick { get; set; }

    [Parameter]
    public EventCallback RemoveClick { get; set; }

    private readonly string _generatedId = $"chat-attachment-{Guid.NewGuid():N}";
    private BaseInterop? _interop;

    private string ElementId => _generatedId;

    private string StatusAttribute => Status switch
    {
        ChatAttachmentStatus.Default => "default",
        ChatAttachmentStatus.Loading => "loading",
        ChatAttachmentStatus.Failed => "failed",
        _ => throw new ArgumentOutOfRangeException(),
    };

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        _interop = new(JSRuntime);
        await _interop.AddEventListener(this, ElementId, "attachmentClick", nameof(AttachmentClicked), includeDetail: false);
        await _interop.AddEventListener(this, ElementId, "removeClick", nameof(RemoveClicked), includeDetail: false);
    }

    [JSInvokable]
    public Task AttachmentClicked() => AttachmentClick.InvokeAsync();

    [JSInvokable]
    public Task RemoveClicked() => RemoveClick.InvokeAsync();

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
    }
}
