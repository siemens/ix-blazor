using System;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.PushCard;

namespace SiemensIXBlazor.Components
{
	public partial class ActionCard
	{
        /// <summary>
        /// Card heading
        /// </summary>
        [Parameter]
        public string? Heading { get; set; }
        /// <summary>
        /// Card icon
        /// </summary>
        [Parameter]
        public string? Icon { get; set; }
        /// <summary>
        /// Card selection
        /// </summary>
        [Parameter]
        public bool Selected { get; set; } = false;
        /// <summary>
        /// Card subheading
        /// </summary>
        [Parameter]
        public string? SubHeading { get; set; }
        /// <summary>
        /// Card variant
        /// </summary>
        [Parameter]
        public PushCardVariant Variant { get; set; } = PushCardVariant.insight;
    }
}

