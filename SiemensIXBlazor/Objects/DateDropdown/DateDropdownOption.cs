// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Newtonsoft.Json;

namespace SiemensIXBlazor.Objects.DateDropdown;

public class
    DateDropdownOption
{
    [JsonProperty("id")] public string Id { get; set; } = null!;

    [JsonProperty("label")] public string Label { get; set; } = null!;

    [JsonProperty("from")] public string? From { get; set; }

    [JsonProperty("to")] public string? To { get; set; }
}