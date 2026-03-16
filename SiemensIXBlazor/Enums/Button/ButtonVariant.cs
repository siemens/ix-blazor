// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

namespace SiemensIXBlazor.Enums.Button
{
    public enum ButtonVariant
    {
        primary,
        secondary,
        tertiary,
        [Obsolete("Use danger_primary instead for supported components.")]
        danger,
        danger_primary,
        danger_secondary,
        danger_tertiary,
        subtle_primary,
        subtle_secondary,
        subtle_tertiary
    }
}
