// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

export const subscribeEvents = (caller, id, eventName, functionName) => {
  const element = document.getElementById(id);
  if (!element) {
    console.error(`Element with ID ${id} not found.`);
    return;
  }

  element.addEventListener(eventName, (e) => {
    if (caller && typeof caller.invokeMethodAsync === "function") {
      caller.invokeMethodAsync(functionName, e.detail).catch((error) => {
        console.error(`Error invoking method '${functionName}':`, error);
      });
    } else {
      console.error("Invalid caller or missing invokeMethodAsync function.");
    }
  });
};
