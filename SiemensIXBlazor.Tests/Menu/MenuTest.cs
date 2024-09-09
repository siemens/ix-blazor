// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Tests.Menu
{
	public class MenuTest : TestContextBase
    {
        [Fact]
        public void MenuRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Components.Menu.Menu>(
                    ("Id", "testId"),
                    ("Class", "test-class"),
                    ("Style", "width: 100%"),
                    ("ApplicationName", "Test Application"),
                    ("ApplicationDescription", "Test Description"),
                    ("EnableMapExpand", true),
                    ("EnableSettings", true),
                    ("EnableToggleTheme", true),
                    ("Expand", true),
                    ("I18NCollapse", "Collapse"),
                    ("I18NExpand", "Expand"),
                    ("I18NLegal", "Legal"),
                    ("I18NMore", "More"),
                    ("I18NSettings", "Settings"),
                    ("I18NToggleTheme", "Toggle Theme"),
                    ("MaxVisibleMenuItems", 5),
                    ("ShowAbout", true),
                    ("ShowSettings", true),
                    ("ChildContent", (RenderFragment)(builder =>
                    {
                        builder.OpenElement(0, "div");
                        builder.AddContent(1, "Test child content");
                        builder.CloseElement();
                    }))
                   );

            // Assert
            cut.MarkupMatches($@"
                <ix-menu 
                    id='testId' 
                    class='test-class' 
                    style='width: 100%' 
                    application-name='Test Application' 
                    application-description='Test Description' 
                    enable-map-expand='' 
                    enable-settings='' 
                    enable-toggle-theme=''
                    expand=''
                    i-1-8n-collapse='Collapse'
                    i-1-8n-expand='Expand'
                    i-1-8n-legal='Legal'
                    i-1-8n-more='More'
                    i-1-8n-settings='Settings'
                    i-1-8n-toggle-theme='Toggle Theme'
                    max-visible-menu-items='5'
                    show-about=''
                    show-settings=''>
                    <div>Test child content</div>
                </ix-menu>");
            //cut.MarkupMatches("<ix-menu id=\"testId\" class=\"test-class\" style=\"width: 100%\" application-name=\"Test Application\" application-description=\"Test Description\" enable-map-expand=\"\" enable-settings=\"\" enable-toggle-theme=\"\" expand=\"\" i-1-8n-collapse=\"Collapse\" i-1-8n-expand=\"Expand\" i-1-8n-legal=\"Legal\" i-1-8n-more=\"More\" i-1-8n-settings=\"Settings\" i-1-8n-toggle-theme=\"Toggle Theme\" max-visible-menu-items=\"5\" show-about=\"\" show-settings=\"\"><div>Test child content</div></ix-menu>");

        }

        [Fact]
        public async Task ExpandChangedEventWorks()
        {
            // Arrange
            var expanded = false;
            var cut = RenderComponent<Components.Menu.Menu>(
                ("Id", "navMenu"),
                ("ExpandChangedEvent", EventCallback.Factory.Create(this, (bool value) => expanded = value))
            );

            // Act
            await cut.Instance.ExpandChanged(true);

            // Assert
            Assert.True(expanded);
        }
        
        [Fact]
        public async Task MapExpandChangedEventWorks()
        {
            // Arrange
            var mapExpanded = false;
            var cut = RenderComponent<Components.Menu.Menu>(
                ("Id", "navMenu"),
                ("MapExpandChangedEvent", EventCallback.Factory.Create(this, (bool value) => mapExpanded = value))
            );

            // Act
            await cut.Instance.MapExpandChanged(true);

            // Assert
            Assert.True(mapExpanded);
        }
    }
}