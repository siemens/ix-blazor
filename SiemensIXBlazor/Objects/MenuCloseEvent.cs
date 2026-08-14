// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Web;

namespace SiemensIXBlazor.Objects
{
    public sealed class MenuCloseEvent
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("nativeEvent")]
        public MouseEventArgs NativeEvent { get; set; } = new();
    }
}
