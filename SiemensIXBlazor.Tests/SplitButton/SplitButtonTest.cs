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
using Microsoft.AspNetCore.Components.Web;
using SiemensIXBlazor.Enums.Button;

namespace SiemensIXBlazor.Tests.SplitButton;

public class SplitButtonTests : TestContextBase
{
	[Fact]
	public void SplitButtonRendersCorrectly()
	{
		// Arrange
		var cut = Render<Components.SplitButton>(parameters => parameters
		    .Add(p => p.Id, "testId")
		    .Add(p => p.Disabled, true)
		    .Add(p => p.DisableButton, true)
		    .Add(p => p.DisableDropdownButton, true)
		    .Add(p => p.Icon, "test-icon")
		    .Add(p => p.Label, "Test Label")
		    .Add(p => p.SplitIcon, "context-menu")
		    .Add(p => p.CloseBehavior, CloseBehavior.Both)
		    .Add(p => p.Variant, ButtonVariant.primary)
		);

		// Assert
		cut.MarkupMatches(
			"<ix-split-button id=\"testId\" disabled='true' disable-button='true' disable-dropdown-button='true' icon=\"test-icon\" label=\"Test Label\" split-icon=\"context-menu\" variant=\"primary\" close-behavior=\"both\"></ix-split-button>");
	}

	[Fact]
	public void ButtonClickedEventWorks()
	{
		// Arrange
		var buttonClicked = false;
		var cut = Render<Components.SplitButton>(parameters => parameters
		    .Add(p => p.Id, "testId")
		    .Add(p => p.ButtonClickedEvent, EventCallback.Factory.Create<MouseEventArgs>(this, _ => buttonClicked = true))
		);

		// Act
		cut.Instance.ButtonClicked(new MouseEventArgs());

		// Assert
		Assert.True(buttonClicked);
	}

	[Fact]
	public void EnableTopLayerDefaultsToFalse()
	{
		// Arrange
		var cut = Render<Components.SplitButton>(parameters => parameters
			.Add(p => p.Id, "test-id"));

		// Assert
		Assert.False(cut.Instance.EnableTopLayer);
		Assert.DoesNotContain("enable-top-layer", cut.Markup);
	}

	[Fact]
	public void EnableTopLayerTrueRendersAttribute()
	{
		// Arrange
		var cut = Render<Components.SplitButton>(parameters => parameters
			.Add(p => p.Id, "test-id")
			.Add(p => p.EnableTopLayer, true));

		// Assert
		Assert.True(cut.Instance.EnableTopLayer);
		Assert.Contains("enable-top-layer", cut.Markup);
	}
}
