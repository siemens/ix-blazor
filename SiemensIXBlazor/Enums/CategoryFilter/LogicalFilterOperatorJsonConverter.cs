// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Text.Json;
using System.Text.Json.Serialization;

namespace SiemensIXBlazor.Enums.CategoryFilter
{
    public sealed class LogicalFilterOperatorJsonConverter : JsonConverter<LogicalFilterOperator>
    {
        public override LogicalFilterOperator Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString() switch
            {
                "Equal" => LogicalFilterOperator.Equal,
                "Not equal" => LogicalFilterOperator.NotEqual,
                var value => throw new JsonException($"Unsupported logical filter operator '{value}'.")
            };
        }

        public override void Write(Utf8JsonWriter writer, LogicalFilterOperator value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToEnumString());
        }
    }
}
