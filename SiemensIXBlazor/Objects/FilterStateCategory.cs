// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using SiemensIXBlazor.Enums.CategoryFilter;
using System.Text.Json.Serialization;

namespace SiemensIXBlazor.Objects
{
    public class FilterStateCategory
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("value")]
        public string Value { get; set; } = string.Empty;
        [JsonPropertyName("operator")]
        public LogicalFilterOperator Operator { get; set; } = LogicalFilterOperator.Equal;
    }
}
