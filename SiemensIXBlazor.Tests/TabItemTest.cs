// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Text.Json;
using Bunit;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Components.TabItem;
using SiemensIXBlazor.Objects.Tabs;
using Xunit;

namespace SiemensIXBlazor.Tests
{
    public class TabItemTest : TestContextBase
    {
        [Fact]
        public void TabItemRendersOfficialDefaults()
        {
            var component = Render<TabItem>(parameters => parameters
                .Add(p => p.TabKey, "tab-1"));

            var tabItem = component.Find("ix-tab-item");
            Assert.Equal("tab-1", tabItem.GetAttribute("tab-key"));
            Assert.Equal("Close tab", tabItem.GetAttribute("aria-label-close-button"));
            Assert.Null(tabItem.GetAttribute("disabled"));
            Assert.Null(tabItem.GetAttribute("selected"));
            Assert.Null(tabItem.GetAttribute("closable"));
            Assert.Null(tabItem.GetAttribute("counter"));
        }

        [Fact]
        public void TabItemRendersPublicPropertiesAndContent()
        {
            var component = Render<TabItem>(parameters => parameters
                .Add(p => p.Id, "my-tab")
                .Add(p => p.TabKey, "tab-1")
                .Add(p => p.Class, "custom-class")
                .Add(p => p.Style, "color: red;")
                .Add(p => p.Label, "Overview")
                .Add(p => p.Icon, "star")
                .Add(p => p.Counter, 5)
                .Add(p => p.Disabled, true)
                .Add(p => p.Selected, true)
                .Add(p => p.Closable, true)
                .Add(p => p.AriaLabelCloseButton, "Close overview")
                .AddChildContent("<span>Content</span>"));

            var tabItem = component.Find("ix-tab-item");
            Assert.Equal("my-tab", tabItem.GetAttribute("id"));
            Assert.Equal("custom-class", tabItem.GetAttribute("class"));
            Assert.Equal("color: red;", tabItem.GetAttribute("style"));
            Assert.Equal("Overview", tabItem.GetAttribute("label"));
            Assert.Equal("star", tabItem.GetAttribute("icon"));
            Assert.Equal("5", tabItem.GetAttribute("counter"));
            Assert.Equal("", tabItem.GetAttribute("disabled"));
            Assert.Equal("", tabItem.GetAttribute("selected"));
            Assert.Equal("", tabItem.GetAttribute("closable"));
            Assert.Equal("Close overview", tabItem.GetAttribute("aria-label-close-button"));
            Assert.Contains("Content", tabItem.InnerHtml);
        }

        [Fact]
        public async Task TabClickEventDeserializesTypedDetail()
        {
            TabClickDetail? clicked = null;
            var component = Render<TabItem>(parameters => parameters
                .Add(p => p.TabKey, "tab-1")
                .Add(p => p.TabClickEvent, EventCallback.Factory.Create<TabClickDetail>(this, value => clicked = value)));

            using var document = JsonDocument.Parse("""{"tabKey":"tab-1","nativeEvent":{}}""");
            await component.Instance.TabClicked(document.RootElement.Clone());

            Assert.NotNull(clicked);
            Assert.Equal("tab-1", clicked!.TabKey);
            Assert.Equal(JsonValueKind.Object, clicked.NativeEvent.ValueKind);
        }

        [Fact]
        public async Task TabCloseEventDeserializesTypedDetail()
        {
            TabClickDetail? closed = null;
            var component = Render<TabItem>(parameters => parameters
                .Add(p => p.TabKey, "tab-1")
                .Add(p => p.TabCloseEvent, EventCallback.Factory.Create<TabClickDetail>(this, value => closed = value)));

            using var document = JsonDocument.Parse("""{"tabKey":"tab-1","nativeEvent":{}}""");
            await component.Instance.TabClosed(document.RootElement.Clone());

            Assert.NotNull(closed);
            Assert.Equal("tab-1", closed!.TabKey);
        }
    }
}
