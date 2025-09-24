// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using SiemensIXBlazor.Enums.CardList;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components
{
	public partial class CardList
	{
		[Parameter, EditorRequired]
		public string Id { get; set; } = string.Empty;
		[Parameter]
		public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public string? AriaLabelExpandButton { get; set; }	
        [Parameter]
		public bool Collapse { get; set; } = false;
		[Parameter]
		public string I18NMoreCards { get; set; } = "There are more cards available";
		[Parameter]
		public string I18NShowAll { get; set; } = "Show all";
		[Parameter]
		public string? Label { get; set; }
		[Parameter]
		public CardListStyle ListStyle { get; set; } = CardListStyle.Stack;
		[Parameter]
		public int? ShowAllCount { get; set; }
		[Parameter]
		public bool HideShowAll { get; set; } = false;
		[Parameter]
		public bool SuppressOverflowHandling { get; set; } = false;
		[Parameter]
		public EventCallback<bool> CollapseChangedEvent { get; set; }
		[Parameter]
		public EventCallback ShowAllClickEvent { get; set; }
		[Parameter]
		public EventCallback ShowMoreCardClickEvent { get; set; }

		private BaseInterop _interop;

		protected async override Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				_interop = new(JSRuntime);

				await _interop.AddEventListener(this, Id, "collapseChanged", "CollapseChanged");
				await _interop.AddEventListener(this, Id, "showAllClick", "ShowAllClicked");
				await _interop.AddEventListener(this, Id, "showMoreCardClick", "ShowMoreCardClicked");
			}
		}

		[JSInvokable]
		public async void CollapseChanged(bool value)
		{
			await CollapseChangedEvent.InvokeAsync(value);
		}

		[JSInvokable]
		public async void ShowAllClicked()
		{
			await ShowAllClickEvent.InvokeAsync();
		}

		[JSInvokable]
		public async void ShowMoreCardClicked()
		{
			await ShowMoreCardClickEvent.InvokeAsync();
		}
	}
}
