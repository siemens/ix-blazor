// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

import { getElementOrThrow } from "./elementUtils.js";

const listeners = new Map();

export function attachEvent(dotNetReference, elementId, eventName, callbackName) {
  const element = getElementOrThrow(elementId);
  const key = `${elementId}:${eventName}`;

  detachEvent(elementId, eventName);

  const handler = (event) => {
    dotNetReference.invokeMethodAsync(callbackName, event.detail);
  };

  element.addEventListener(eventName, handler);
  listeners.set(key, { element, handler, eventName });
}

export function detachEvent(elementId, eventName) {
  const key = `${elementId}:${eventName}`;
  const listener = listeners.get(key);

  if (listener) {
    listener.element.removeEventListener(eventName, listener.handler);
    listeners.delete(key);
  }
}

export function detachEvents(elementId) {
  const prefix = `${elementId}:`;

  for (const [key, listener] of listeners) {
    if (!key.startsWith(prefix)) {
      continue;
    }

    listener.element.removeEventListener(listener.eventName, listener.handler);
    listeners.delete(key);
  }
}

export function setProperty(elementId, propertyName, propertyValue) {
  getElementOrThrow(elementId)[propertyName] = propertyValue;
}

export async function updatePosition(elementId) {
  const element = getElementOrThrow(elementId);
  await element.updatePosition();
}
