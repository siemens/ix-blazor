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
        public static string EnumToString(TEnum enumValue, bool toLowerCase = true)
        {
            if (!Enum.IsDefined(typeof(TEnum), enumValue))
            {
                throw new ArgumentException($"The value '{enumValue}' is not a valid enum value for type '{typeof(TEnum).Name}'");
            }

            var enumName = Enum.GetName(typeof(TEnum), enumValue);
            return toLowerCase ? enumName?.ToLowerInvariant() ?? string.Empty : enumName ?? string.Empty;
        }
    }
}
