// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System;
using Newtonsoft.Json;

namespace SiemensIXBlazor.Objects
{
	public class ToastConfig
	{
		[JsonProperty("autoClose")]
		public bool AutoClose { get; set; } = true;
        [JsonProperty("autoCloseDelay")]
        public int AutoCloseDelay { get; set; } = 5000;
		[JsonProperty("icon")]
		public string? Icon { get; set; }
		[JsonProperty("iconColor")]
		public string? IconColor { get; set; }
		[JsonProperty("message")]
		public string? Message { get; set; }
		[JsonProperty("title")]
		public string? Title { get; set; }
		[JsonProperty("type")]
		public string Type { get; set; } = "info";
	}
}

