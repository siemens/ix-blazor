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
using SiemensIXBlazor.Components.MenuSettings;
using SiemensIXBlazor.Objects;

namespace SiemensIXBlazor.Tests.MenuSettings
{
	public class MenuSettingsItemTests : TestContextBase
    {
        [Fact]
        public void MenuSettingsItemRendersCorrectly()
        {
            // Arrange
            RenderFragment childContent = builder =>
            {
                builder.AddContent(0, "Simple Text");
            };

            // Act
            var cut = RenderComponent<MenuSettingsItem>(
                ("Label", "Test Label"),
                ("ChildContent", childContent));

            // Assert
			cut.MarkupMatches("<ix-menu-settings-item label=\"Test Label\">Simple Text</ix-menu-settings-item>");
		}

		[Fact]
		public async Task RendersTabKeyAndForwardsLabelChange()
		{
			MenuLabelChangeEvent? changed = null;
			var cut = RenderComponent<MenuSettingsItem>(parameters => parameters
				.Add(p => p.Id, "settings-general")
				.Add(p => p.TabKey, "general")
				.Add(p => p.Label, "General")
				.Add(p => p.LabelChangedEvent, EventCallback.Factory.Create<MenuLabelChangeEvent>(this, value => changed = value)));
			var expected = new MenuLabelChangeEvent { Name = "settings-general", OldLabel = "Old", NewLabel = "General" };

			await cut.Instance.LabelChanged(expected);

			Assert.Contains("tab-key=\"general\"", cut.Markup);
			Assert.Same(expected, changed);
		}
	}
}
