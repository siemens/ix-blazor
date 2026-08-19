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
using SiemensIXBlazor.Objects.CardList;
using System.Text.Json;

namespace SiemensIXBlazor.Tests
{
	public class CardListTests : TestContextBase
	{
		[Fact]
		public void CardListRendersWithoutCrashing()
		{
			// Arrange
			var cut = Render<CardList>(parameters =>
			{
				parameters.Add(p => p.Id, "testId");
				parameters.Add(p => p.Collapse, true);
				parameters.Add(p => p.I18nMoreCards, "testMoreCards");
				parameters.Add(p => p.I18nShowAll, "testShowAll");
				parameters.Add(p => p.I18nShowLess, "testShowLess");
				parameters.Add(p => p.Label, "testLabel");
				parameters.Add(p => p.ListStyle, Enums.CardList.CardListStyle.Stack);
				parameters.Add(p => p.ShowAllCount, 1);
				parameters.Add(p => p.SuppressOverflowHandling, true);
				parameters.Add(p => p.HideShowAll, true);

			});

			// Assert
			cut.MarkupMatches("<ix-card-list id=\"testId\" label=\"testLabel\" show-all-count=\"1\" list-style=\"stack\" collapse=\"true\" i18n-more-cards=\"testMoreCards\" i18n-show-all=\"testShowAll\" i18n-show-less=\"testShowLess\" suppress-overflow-handling=\"true\" hide-show-all='true'></ix-card-list>");
		}

		[Fact]
		public void CollapsedChangedEventTriggeredCorrectly()
		{
			// Arrange
			var eventTriggered = false;
			var cut = Render<CardList>(parameters => parameters.Add(p => p.CollapseChangedEvent, EventCallback.Factory.Create<bool>(this, () => eventTriggered = true)));

			// Act
			cut.Instance.CollapseChangedEvent.InvokeAsync(true);

			// Assert
			Assert.True(eventTriggered);
		}

		[Fact]
		public async Task ShowAllClickedEventTriggeredCorrectly()
		{
			// Arrange
			var eventTriggered = false;
			string? receivedNativeEvent = null;
			var cut = Render<CardList>(parameters => parameters.Add(p => p.ShowAllClickEvent, EventCallback.Factory.Create<CardListClickEventArgs>(this, args =>
			{
				eventTriggered = true;
				receivedNativeEvent = args.NativeEvent.GetProperty("type").GetString();
			})));

			// Act
			using JsonDocument detail = JsonDocument.Parse("{\"nativeEvent\":{\"type\":\"click\"}}");
			await cut.Instance.ShowAllClicked(detail.RootElement);

			// Assert
			Assert.True(eventTriggered);
			Assert.Equal("click", receivedNativeEvent);
		}

		[Fact]
		public async Task ShowMoreCardClickedEventTriggeredCorrectly()
		{
			// Arrange
			var eventTriggered = false;
			string? receivedKey = null;
			var cut = Render<CardList>(parameters => parameters.Add(p => p.ShowMoreCardClickEvent, EventCallback.Factory.Create<CardListClickEventArgs>(this, args =>
			{
				eventTriggered = true;
				receivedKey = args.NativeEvent.GetProperty("key").GetString();
			})));

			// Act
			using JsonDocument detail = JsonDocument.Parse("{\"nativeEvent\":{\"key\":\"Enter\"}}");
			await cut.Instance.ShowMoreCardClicked(detail.RootElement);

			// Assert
			Assert.True(eventTriggered);
			Assert.Equal("Enter", receivedKey);
		}
	}
}
