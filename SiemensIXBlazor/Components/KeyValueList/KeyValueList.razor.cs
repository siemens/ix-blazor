using System;
using Microsoft.AspNetCore.Components;

namespace SiemensIXBlazor.Components
{
	public partial class KeyValueList
	{
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        /// <summary>
        /// Optional striped key value list style. Default value is null.
        /// </summary>
        [Parameter]
        public bool? Striped { get; set; }
    }
}

