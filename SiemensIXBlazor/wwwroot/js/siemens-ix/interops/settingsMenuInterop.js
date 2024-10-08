﻿// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

export async function toggleSettings(id, status) {
  try {
    const element = document.getElementById(id);
    if (!element) {
      throw new Error(`Element with ID ${id} not found`);
    }
    await element.toggleSettings(status);
  } catch {
    console.error("Failed to toggle settings:", error);
  }
}
