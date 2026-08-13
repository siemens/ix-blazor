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

namespace SiemensIXBlazor.Objects.Tabs
{
    public class TabClickDetail
    {
        [JsonPropertyName("tabKey")]
        public string? TabKey { get; set; }

        [JsonPropertyName("nativeEvent")]
        public JsonElement NativeEvent { get; set; }
    }
}
