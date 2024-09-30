// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

export function setDateRangeOptions(id, dateRangeOptions) {
  try {
    const element = document.getElementById(id);
    if (!element) {
      throw new Error(`Element with ID ${id} not found`);
    }
    element.dateRangeOptions = JSON.parse(dateRangeOptions);
  } catch (err) {
    console.error("Failed to set date range options:", err);
  }
}
