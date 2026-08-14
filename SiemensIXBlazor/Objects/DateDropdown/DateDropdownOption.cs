// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace SiemensIXBlazor.Objects.DateDropdown;

public class
    DateDropdownOption
{
    [JsonProperty("id"), JsonPropertyName("id")] public string Id { get; set; } = null!;

    [JsonProperty("label"), JsonPropertyName("label")] public string Label { get; set; } = null!;

    [JsonProperty("from"), JsonPropertyName("from")] public string? From { get; set; }

    [JsonProperty("to"), JsonPropertyName("to")] public string? To { get; set; }
}
