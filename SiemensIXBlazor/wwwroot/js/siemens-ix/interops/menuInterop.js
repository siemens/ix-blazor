// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

async function invokeMenuMethod(id, methodName, ...args) {
  const element = document.getElementById(id);
  if (!element) {
    throw new Error("Element with ID " + id + " not found");
  }

  await element[methodName](...args);
}

export function toggleMenu(id, show) {
  return show === null || show === undefined
    ? invokeMenuMethod(id, 'toggleMenu')
    : invokeMenuMethod(id, 'toggleMenu', show);
}

export function toggleMapExpand(id, show) {
  return show === null || show === undefined
    ? invokeMenuMethod(id, 'toggleMapExpand')
    : invokeMenuMethod(id, 'toggleMapExpand', show);
}

export function toggleSettings(id, show) {
  return invokeMenuMethod(id, 'toggleSettings', show);
}

export function toggleAbout(id, show) {
  return invokeMenuMethod(id, 'toggleAbout', show);
}
