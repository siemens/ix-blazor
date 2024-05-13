// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace SiemensIXBlazor.Objects.Application;

public class AppSwitchConfig
{
    public string CurrentAppId { get; set; } = string.Empty;
    public List<App> Apps { get; set; } = [];
    public string? I18nAppSwitch { get; set; }
    public string? I18nLoadingApps { get; set; }

}