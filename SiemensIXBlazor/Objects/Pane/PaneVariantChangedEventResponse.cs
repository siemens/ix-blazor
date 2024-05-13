// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace SiemensIXBlazor.Objects.Pane
{
    public  class PaneVariantChangedEventResponse
    {
        [JsonPropertyName("slot")]
        public string Slot { get; set; }
        [JsonPropertyName("variant")]
        public string Variant { get; set; }
    }
}
