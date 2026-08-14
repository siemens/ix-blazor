// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using SiemensIXBlazor.Components.TabItem;
using Xunit;

namespace SiemensIXBlazor.Tests
{
    public class TabItemTest : TestContextBase
    {
        [Fact]
        public void TabItem_Should_Render_With_Default_Properties()
        {
            // Arrange & Act
            var component = RenderComponent<TabItem>();

            // Assert
            var tabItem = component.Find("ix-tab-item");
            Assert.NotNull(tabItem);
            Assert.Null(tabItem.GetAttribute("disabled"));
            Assert.Null(tabItem.GetAttribute("selected"));
            Assert.Null(tabItem.GetAttribute("counter"));
        }

        [Fact]
        public void TabItem_Should_Render_With_Id()
        {
            // Arrange & Act
            var component = RenderComponent<TabItem>(parameters => parameters
                .Add(p => p.Id, "my-tab")
            );

            // Assert
            var tabItem = component.Find("ix-tab-item");
            Assert.Equal("my-tab", tabItem.GetAttribute("id"));
        }

        [Fact]
        public void TabItem_Should_Render_TabKey_And_Label()
        {
            // Arrange & Act
            var component = RenderComponent<TabItem>(parameters => parameters
                .Add(p => p.TabKey, "overview")
                .Add(p => p.Label, "Overview")
            );

            // Assert
            var tabItem = component.Find("ix-tab-item");
            Assert.Equal("overview", tabItem.GetAttribute("tab-key"));
            Assert.Equal("Overview", tabItem.GetAttribute("label"));
        }

        [Fact]
        public void TabItem_Should_Render_Icon_Attribute()
        {
            // Arrange & Act
            var component = RenderComponent<TabItem>(parameters => parameters
                .Add(p => p.Icon, "star")
            );

            // Assert
            var tabItem = component.Find("ix-tab-item");
            Assert.Equal("star", tabItem.GetAttribute("icon"));
        }

        [Fact]
        public void TabItem_Should_Render_Closable_And_CloseLabel()
        {
            // Arrange & Act
            var component = RenderComponent<TabItem>(parameters => parameters
                .Add(p => p.Closable, true)
                .Add(p => p.AriaLabelCloseButton, "Close this tab")
            );

            // Assert
            var tabItem = component.Find("ix-tab-item");
            Assert.Equal("", tabItem.GetAttribute("closable"));
            Assert.Equal("Close this tab", tabItem.GetAttribute("aria-label-close-button"));
        }

        [Fact]
        public void TabItem_Should_Render_With_Disabled()
        {
            // Arrange & Act
            var component = RenderComponent<TabItem>(parameters => parameters
                .Add(p => p.Disabled, true)
            );

            // Assert
            var tabItem = component.Find("ix-tab-item");
            Assert.Equal("", tabItem.GetAttribute("disabled"));
        }

        [Fact]
        public void TabItem_Should_Render_With_Selected()
        {
            // Arrange & Act
            var component = RenderComponent<TabItem>(parameters => parameters
                .Add(p => p.Selected, true)
            );

            // Assert
            var tabItem = component.Find("ix-tab-item");
            Assert.Equal("", tabItem.GetAttribute("selected"));
        }

        [Fact]
        public void TabItem_Should_Render_With_Counter()
        {
            // Arrange & Act
            var component = RenderComponent<TabItem>(parameters => parameters
                .Add(p => p.Counter, 5)
            );

            // Assert
            var tabItem = component.Find("ix-tab-item");
            Assert.Equal("5", tabItem.GetAttribute("counter"));
        }

        [Fact]
        public void TabItem_Should_Render_With_Class()
        {
            // Arrange & Act
            var component = RenderComponent<TabItem>(parameters => parameters
                .Add(p => p.Class, "custom-class")
            );

            // Assert
            var tabItem = component.Find("ix-tab-item");
            Assert.Equal("custom-class", tabItem.GetAttribute("class"));
        }

        [Fact]
        public void TabItem_Should_Render_With_Style()
        {
            // Arrange & Act
            var component = RenderComponent<TabItem>(parameters => parameters
                .Add(p => p.Style, "color: red;")
            );

            // Assert
            var tabItem = component.Find("ix-tab-item");
            Assert.Equal("color: red;", tabItem.GetAttribute("style"));
        }

        [Fact]
        public void TabItem_Should_Render_ChildContent()
        {
            // Arrange & Act
            var component = RenderComponent<TabItem>(parameters => parameters
                .AddChildContent("<span>Content</span>")
            );

            // Assert
            var tabItem = component.Find("ix-tab-item");
            Assert.Contains("Content", tabItem.InnerHtml);
        }

        [Fact]
        public void TabItem_Should_Not_Render_IxIcon_When_No_Icon_Set()
        {
            // Arrange & Act
            var component = RenderComponent<TabItem>(parameters => parameters
                .Add(p => p.Label, "No Icon Tab")
            );

            // Assert
            var tabItem = component.Find("ix-tab-item");
            Assert.Null(tabItem.GetAttribute("icon"));
        }
    }
}
