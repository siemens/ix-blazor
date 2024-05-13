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
    public class TreeNode
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("data")]
        public TreeData? Data { get; set; }
        [JsonProperty("hasChildren")]
        public bool HasChildren { get; set; }
        [JsonProperty("children")]
        public List<string>? Children { get; set; }
    }
}
