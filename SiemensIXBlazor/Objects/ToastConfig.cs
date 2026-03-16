// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SiemensIXBlazor.Enums;

namespace SiemensIXBlazor.Objects
{
	public class ToastConfig
	{
		/// <summary>
		/// Sets the HTML content for the toast action area.
		/// This property accepts an HTML string containing interactive elements such as <c>&lt;ix-button&gt;</c>.
		/// </summary>
		/// <remarks>
		/// The HTML string is rendered directly within the toast's action container.
		/// Ensure content is properly escaped to prevent XSS vulnerabilities.
		/// </remarks>
		[JsonProperty("action")]
		public string? Action { get; set; }
        [JsonProperty("preventAutoClose")]
        public bool PreventAutoClose { get; set; } = false;
        [JsonProperty("autoCloseDelay")]
		public int AutoCloseDelay { get; set; } = 5000;
		[JsonProperty("hideIcon")]
		public bool HideIcon { get; set; } = false;
		[JsonProperty("icon")]
		public string? Icon { get; set; }
		[JsonProperty("iconColor")]
		public string? IconColor { get; set; }
		[JsonProperty("message")]
		public string? Message { get; set; }
		[JsonProperty("messageHtml")]
		public string? MessageHtml { get; set; }
		[JsonProperty("position")]
		[JsonConverter(typeof(StringEnumConverter))]
		public ToastPosition? Position { get; set; }
		[JsonProperty("title")]
		public string? Title { get; set; }
		[JsonProperty("type")]
		public string Type { get; set; } = "info";
	}
}

