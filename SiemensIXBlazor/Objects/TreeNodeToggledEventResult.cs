// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Newtonsoft.Json;

namespace SiemensIXBlazor.Objects
{
    public class TreeNodeToggledEventResult
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("isExpaned")]
        public bool IsExpaned { get; set; }
    }
}
