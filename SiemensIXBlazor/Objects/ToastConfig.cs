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
		/// Toast title.
		/// </summary>
		[JsonProperty("title")]
		public string? Title { get; set; }

		/// <summary>
		/// Toast message text.
		/// </summary>
		[JsonProperty("message")]
		public string? Message { get; set; }

		/// <summary>
		/// HTML content for the action area.
		/// </summary>
		[JsonProperty("action")]
		public string? Action { get; set; }

		/// <summary>
		/// Toast type. The official default is info when omitted.
		/// </summary>
		[JsonProperty("type")]
		[JsonConverter(typeof(StringEnumConverter))]
		public ToastType? Type { get; set; }

		/// <summary>
		/// Whether the toast closes automatically.
		/// </summary>
		[JsonProperty("autoClose")]
		public bool? AutoClose { get; set; }

		[JsonProperty("autoCloseDelay")]
		public int? AutoCloseDelay { get; set; }

		[JsonProperty("icon")]
		public string? Icon { get; set; }

		[JsonProperty("iconColor")]
		public string? IconColor { get; set; }

		[JsonProperty("hideIcon")]
		public bool? HideIcon { get; set; }
	}
}
