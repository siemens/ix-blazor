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
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.Tabs;
using System.Text.Json;
using Xunit;

namespace SiemensIXBlazor.Tests
{
    public class TabsTests : TestContextBase
    {
        [Fact]
        public void TabsRendersOfficialProperties()
        {
            var cut = Render<Tabs>(parameters => parameters
                .Add(p => p.Id, "testId")
                .Add(p => p.Layout, TabsLayout.Stretched)
                .Add(p => p.Placement, TabsPlacement.Top)
                .Add(p => p.Rounded, true)
                .Add(p => p.ActiveTabKey, "tab-2")
                .Add(p => p.AriaLabelMoreTabs, "Show every tab")
                .Add(p => p.KeyboardNavigation, TabsKeyboardNavigation.Manual)
                .Add(p => p.Small, true)
            );

            cut.MarkupMatches("<ix-tabs id=\"testId\" layout=\"stretched\" placement=\"top\" rounded active-tab-key=\"tab-2\" aria-label-more-tabs=\"Show every tab\" keyboard-navigation=\"manual\" small></ix-tabs>");
        }

        [Fact]
        public async Task TabChangeEventUpdatesActiveKeyAndInvokesCallback()
        {
            string? changedKey = null;
            var cut = Render<Tabs>(parameters => parameters
                .Add(p => p.Id, "testId")
                .Add(p => p.TabChangeEvent, EventCallback.Factory.Create<string?>(this, value => changedKey = value)));

            await cut.Instance.TabChanged("tab-2");

            Assert.Equal("tab-2", cut.Instance.ActiveTabKey);
            Assert.Equal("tab-2", changedKey);
        }

        [Fact]
        public async Task TabCloseEventInvokesCallback()
        {
            string? closedKey = null;
            var cut = Render<Tabs>(parameters => parameters
                .Add(p => p.Id, "testId")
                .Add(p => p.TabCloseEvent, EventCallback.Factory.Create<string?>(this, value => closedKey = value)));

            using var document = JsonDocument.Parse("""{"tabKey":"tab-3","nativeEvent":{}}""");
            await cut.Instance.TabClosed(document.RootElement.Clone());

            Assert.Equal("tab-3", closedKey);

            using var stringValue = JsonDocument.Parse("\"tab-4\"");
            await cut.Instance.TabClosed(stringValue.RootElement.Clone());

            Assert.Equal("tab-4", closedKey);
        }
    }
}
