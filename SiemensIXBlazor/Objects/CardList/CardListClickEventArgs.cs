// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using System.Text.Json;

namespace SiemensIXBlazor.Objects.CardList;

/// <summary>
/// Event detail emitted by the CardList show-all and show-more events.
/// </summary>
public sealed class CardListClickEventArgs
{
    public JsonElement NativeEvent { get; init; }
}
