// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

export function getElement(id) {
  return document.getElementById(id);
}

export function getElementOrThrow(id) {
  const element = getElement(id);
  if (!element) {
    throw new Error(`Element with ID ${id} not found`);
  }

  return element;
}
