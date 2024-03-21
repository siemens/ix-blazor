using System;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Enums.PushCard;

namespace SiemensIXBlazor.Components
{
	public partial class PushCard
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
        /// Card KPI value
        /// </summary>
        [Parameter]
        public string? Notification { get; set; }
        /// <summary>
        /// Card subheading
        /// </summary>
        [Parameter]
        public string? SubHeading { get; set; }
        [Parameter]
        public bool Collapsed { get; set; } = true;
        /// <summary>
        /// Card variant
        /// </summary>
        [Parameter]
        public PushCardVariant Variant { get; set; } = PushCardVariant.insight;
    }
}

