using System;
using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components
{
	public partial class KeyValue
	{
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        /// <summary>
        /// Optional key value icon
        /// </summary>
        [Parameter]
		public string? Icon { get; set; }
        /// <summary>
        /// Key value label
        /// </summary>
        [Parameter]
		public string? Label { get; set; }
        /// <summary>
        /// Optional key value label position - 'top' or 'left'
        /// </summary>
        [Parameter]
		public string LabelPosition { get; set; } = "top";
        /// <summary>
        /// Optional key value text value
        /// </summary>
		[Parameter]
		public string? Value { get; set; }
	}
}

