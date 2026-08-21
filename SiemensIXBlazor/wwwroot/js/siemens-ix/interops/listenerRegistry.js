// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

export function createListenerRegistry(prefix) {
  const listeners = new Map();
  let nextId = 0;

  return {
    add(element, eventName, listener) {
      const listenerId = `${prefix}-${++nextId}`;
      element.addEventListener(eventName, listener);
      listeners.set(listenerId, { element, eventName, listener });
      return listenerId;
    },

    remove(listenerId) {
      const registration = listeners.get(listenerId);
      if (!registration) {
        return;
      }

      registration.element.removeEventListener(registration.eventName, registration.listener);
      listeners.delete(listenerId);
    },
  };
}
