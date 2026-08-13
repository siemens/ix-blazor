// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using System.Globalization;

namespace SiemensIXBlazor.Enums.LayoutGrid;

public static class LayoutGridExtensions
{
    public static string ToAttributeValue(this ColumnSize size) =>
        size == ColumnSize.auto ? "auto" : ((int)size).ToString(CultureInfo.InvariantCulture);

    public static string ToAttributeValue(this LayoutGridGap gap) =>
        ((int)gap).ToString(CultureInfo.InvariantCulture);
}
