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
using SiemensIXBlazor.Enums.Button;

namespace SiemensIXBlazor.Tests.SplitButton;

public class SplitButtonTests : TestContextBase
{
	[Fact]
	public void SplitButtonRendersCorrectly()
	{
		// Arrange
		var cut = RenderComponent<Components.SplitButton>(
			("Id", "testId"),
			("Disabled", true),
			("Ghost", true),
			("Icon", "test-icon"),
			("Label", "Test Label"),
			("Outline", true),
			("Placement", "bottom-start"),
			("SplitIcon", "context-menu"),
			("CloseBehavior", CloseBehavior.Both),
			("Variant", ButtonVariant.primary)
		);

		// Assert
		cut.MarkupMatches(
			"<ix-split-button id=\"testId\" disabled ghost icon=\"test-icon\" label=\"Test Label\" outline placement=\"bottom-start\" split-icon=\"context-menu\" variant=\"primary\" close-behavior=\"both\"></ix-split-button>");
	}

	[Fact]
	public void ButtonClickedEventWorks()
	{
		// Arrange
		var buttonClicked = false;
		var cut = RenderComponent<Components.SplitButton>(
			("Id", "testId"),
			("ButtonClickedEvent", EventCallback.Factory.Create(this, () => buttonClicked = true))
		);

		// Act
		cut.Instance.ButtonClicked();

		// Assert
		Assert.True(buttonClicked);
	}

	[Fact]
	public void EnableTopLayerDefaultsToFalse()
	{
		// Arrange
		var cut = RenderComponent<Components.SplitButton>(parameters => parameters
			.Add(p => p.Id, "test-id"));

		// Assert
		Assert.False(cut.Instance.EnableTopLayer);
		Assert.DoesNotContain("enable-top-layer", cut.Markup);
	}

	[Fact]
	public void EnableTopLayerTrueRendersAttribute()
	{
		// Arrange
		var cut = RenderComponent<Components.SplitButton>(parameters => parameters
			.Add(p => p.Id, "test-id")
			.Add(p => p.EnableTopLayer, true));

		// Assert
		Assert.True(cut.Instance.EnableTopLayer);
		Assert.Contains("enable-top-layer", cut.Markup);
	}
}
