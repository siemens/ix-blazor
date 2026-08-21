// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using System.Text.Json;
using Bunit;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.Chat;

namespace SiemensIXBlazor.Tests;

public class ChatTests : TestContextBase
{
    [Fact]
    public void ChatRendersPromptAndMessagesInNamedSlots()
    {
        var cut = Render<Chat>(parameters =>
        {
            parameters.AddChildContent("AI response");
            parameters.Add(p => p.Prompt, (RenderFragment)(builder => builder.AddContent(0, "Prompt")));
        });

        Assert.Contains("AI response", cut.Markup);
        Assert.Contains("slot=\"prompt\"", cut.Markup);
    }

    [Fact]
    public void ChatMessagesRenderOfficialPropertiesAndSlots()
    {
        var cut = Render<ChatUserMessage>(parameters =>
        {
            parameters.Add(p => p.Message, "Message text");
            parameters.Add(p => p.Actions, (RenderFragment)(builder => builder.AddContent(0, "Actions")));
            parameters.Add(p => p.Attachments, (RenderFragment)(builder => builder.AddContent(0, "Attachments")));
        });

        Assert.Equal("Message text", cut.Find("ix-chat-user-message").GetAttribute("message"));
        Assert.Contains("slot=\"actions\"", cut.Markup);
        Assert.Contains("slot=\"attachments\"", cut.Markup);

        var aiCut = Render<ChatAiMessage>(parameters =>
        {
            parameters.Add(p => p.Actions, (RenderFragment)(builder => builder.AddContent(0, "Actions")));
            parameters.Add(p => p.Sources, (RenderFragment)(builder => builder.AddContent(0, "Sources")));
        });

        Assert.Contains("slot=\"actions\"", aiCut.Markup);
        Assert.Contains("slot=\"sources\"", aiCut.Markup);
    }

    [Fact]
    public void ChatAttachmentRendersTypedStateAndInvokesEvents()
    {
        var attachmentClicked = false;
        var removed = false;
        var cut = Render<ChatAttachment>(parameters =>
        {
            parameters.Add(p => p.FileName, "report.pdf");
            parameters.Add(p => p.Status, ChatAttachmentStatus.Failed);
            parameters.Add(p => p.HideRemoveButton, true);
            parameters.Add(p => p.PreviewSupported, true);
            parameters.Add(p => p.AttachmentClick, EventCallback.Factory.Create(this, () => attachmentClicked = true));
            parameters.Add(p => p.RemoveClick, EventCallback.Factory.Create(this, () => removed = true));
        });

        var element = cut.Find("ix-chat-attachment");
        Assert.Equal("report.pdf", element.GetAttribute("file-name"));
        Assert.Equal("failed", element.GetAttribute("status"));
        Assert.Equal("true", element.GetAttribute("hide-remove-button"));
        Assert.Equal("true", element.GetAttribute("preview-supported"));

        cut.Instance.AttachmentClicked();
        cut.Instance.RemoveClicked();
        Assert.True(attachmentClicked);
        Assert.True(removed);
    }

    [Fact]
    public void ChatInputRendersOfficialDefaultsAndSlots()
    {
        var cut = Render<ChatInput>(parameters =>
        {
            parameters.Add(p => p.State, ChatInputState.Processing);
            parameters.Add(p => p.Value, "Question");
            parameters.Add(p => p.Disabled, true);
            parameters.Add(p => p.Readonly, true);
            parameters.Add(p => p.InsertLineBreakOnEnter, true);
            parameters.Add(p => p.Start, (RenderFragment)(builder => builder.AddContent(0, "Start")));
            parameters.Add(p => p.End, (RenderFragment)(builder => builder.AddContent(0, "End")));
        });

        var element = cut.Find("ix-chat-input");
        Assert.Equal("processing", element.GetAttribute("state"));
        Assert.Equal("Question", element.GetAttribute("value"));
        Assert.Equal("true", element.GetAttribute("disabled"));
        Assert.Equal("true", element.GetAttribute("readonly"));
        Assert.Equal("true", element.GetAttribute("insert-line-break-on-enter"));
        Assert.Contains("slot=\"start\"", cut.Markup);
        Assert.Contains("slot=\"end\"", cut.Markup);
    }

    [Fact]
    public async Task ChatInputInvokesTypedValueEventsAndMethods()
    {
        string? valueChange = null;
        string? change = null;
        string? prompt = null;
        var blurred = false;
        var cut = Render<ChatInput>(parameters =>
        {
            parameters.Add(p => p.ValueChange, EventCallback.Factory.Create<string>(this, value => valueChange = value));
            parameters.Add(p => p.IxChange, EventCallback.Factory.Create<string>(this, value => change = value));
            parameters.Add(p => p.PromptSubmit, EventCallback.Factory.Create<string>(this, value => prompt = value));
            parameters.Add(p => p.IxBlur, EventCallback.Factory.Create(this, () => blurred = true));
        });

        using JsonDocument value = JsonDocument.Parse("\"hello\"");
        await cut.Instance.ValueChanged(value.RootElement);
        await cut.Instance.Changed(value.RootElement);
        await cut.Instance.PromptSubmitted(value.RootElement);
        await cut.Instance.Blurred();

        Assert.Equal("hello", valueChange);
        Assert.Equal("hello", change);
        Assert.Equal("hello", prompt);
        Assert.True(blurred);

        await cut.Instance.FocusInputAsync();
        await cut.Instance.GetNativeInputElementAsync();
    }
}
