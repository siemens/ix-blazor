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
using SiemensIXBlazor.Enums.Button;
using SiemensIXBlazor.Interops;

namespace SiemensIXBlazor.Components;

public partial class SplitButton
{
	[Parameter]
	public RenderFragment? ChildContent { get; set; }
	[Parameter, EditorRequired]
	public string Id { get; set; } = string.Empty;
    [Parameter]
    public string? AriaLabelButton { get; set; }
    [Parameter]
    public string? AriaLabelSplitIconButton { get; set; }	
    [Parameter]
	public bool Disabled { get; set; } = false;
	[Parameter]
	public bool EnableTopLayer { get; set; } = false;
	[Parameter]
	public bool DisableButton { get; set; } = false;
	[Parameter]
	public bool DisableDropdownButton { get; set; } = false;
	[Parameter]
	public string? Icon { get; set; }
	[Parameter]
	public string? Label { get; set; }
	[Parameter]
	public string? SplitIcon { get; set; }
	[Parameter]
	public CloseBehavior CloseBehavior { get; set; } = CloseBehavior.Both;
	[Parameter]
	public ButtonVariant Variant { get; set; } = ButtonVariant.primary;
	[Parameter]
	public EventCallback<MouseEventArgs> ButtonClickedEvent { get; set; }

	private BaseInterop? _interop;

	protected async override Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			_interop = new(JSRuntime);

			await _interop.AddEventListener(this, Id, "buttonClick", "ButtonClicked");
		}
	}

	[JSInvokable]
	public async Task ButtonClicked(MouseEventArgs args)
	{
		await ButtonClickedEvent.InvokeAsync(args);
	}

}
