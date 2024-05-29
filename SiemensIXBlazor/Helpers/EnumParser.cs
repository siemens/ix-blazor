// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

namespace SiemensIXBlazor.Helpers
{
    public static class EnumParser<TEnum> where TEnum : Enum
    {
        public static string ParseEnumToString(object enumValue)
        {
            var enumName = Enum.GetName(typeof(TEnum), enumValue);
            return enumName != null ? enumName.ToLowerInvariant() : string.Empty;
        }

    }
}
