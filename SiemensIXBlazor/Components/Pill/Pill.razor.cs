using System;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.Pill;

namespace SiemensIXBlazor.Components
{
	public partial class Pill
	{
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
		public bool AllignLeft { get; set; } = false;
		[Parameter]
		public string? Background { get; set; }
		[Parameter]
		public string? Color { get; set; }
		[Parameter]
		public string? Icon { get; set; }
		[Parameter]
		public bool Outline { get; set; } = false;
		[Parameter]
		public PillVariant? Variant { get; set; }
	}
}

