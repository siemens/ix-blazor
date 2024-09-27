// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

export function listenEvent(caller, elementId, eventName, funtionName) {
  const element = document.getElementById(elementId);

  if (!element) {
    throw new Error(`Element with ID ${elementId} not found`);
  }

  element.addEventListener(eventName, (e) => {
    caller.invokeMethodAsync(funtionName, e.detail);
  });
}
