// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace SiemensIXBlazor.Objects
{
    public sealed class MenuLabelChangeEvent
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("oldLabel")]
        public string OldLabel { get; set; } = string.Empty;

        [JsonPropertyName("newLabel")]
        public string NewLabel { get; set; } = string.Empty;
    }
}
