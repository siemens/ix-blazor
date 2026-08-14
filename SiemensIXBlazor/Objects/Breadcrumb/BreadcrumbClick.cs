// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

namespace SiemensIXBlazor.Objects.Breadcrumb;

using System.Text.Json.Serialization;

public sealed class BreadcrumbClick
{
    [JsonPropertyName("breadcrumbKey")]
    public string BreadcrumbKey { get; set; } = string.Empty;
    [JsonPropertyName("label")]
    public string? Label { get; set; }
}
