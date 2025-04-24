// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Models;

public class MenuCategoryModel
{
    public string Label { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public List<MenuItemModel> Items { get; set; } = [];
}
