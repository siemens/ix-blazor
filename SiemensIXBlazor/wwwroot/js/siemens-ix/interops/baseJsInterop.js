// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

import { createListenerRegistry } from "./listenerRegistry.js";
import { getElementOrThrow } from "./elementUtils.js";

const eventListeners = createListenerRegistry("base-listener");

export function listenEvent(caller, elementId, eventName, functionName, includeDetail = true) {
  const element = getElementOrThrow(elementId);

  const listener = (e) => {
    if (includeDetail) {
      caller.invokeMethodAsync(functionName, e.detail);
    } else {
      caller.invokeMethodAsync(functionName);
    }
  };
  return eventListeners.add(element, eventName, listener);
}

export function removeEventListener(listenerId) {
  eventListeners.remove(listenerId);
}

export function setElementProperty(elementId, propertyName, propertyValue) {
  const element = getElementOrThrow(elementId);

  element[propertyName] = propertyValue;
}

export function invokeElementMethod(elementId, methodName) {
  const element = getElementOrThrow(elementId);

  if (typeof element[methodName] !== "function") {
    throw new Error(`Method ${methodName} not found on element ${elementId}`);
  }

  return element[methodName]();
}

export async function invokeMethod(elementId, methodName) {
  const element = getElementOrThrow(elementId);

  const result = await element[methodName]();

  if (methodName === 'getValidityState' && result) {
    return {
      badInput: result.badInput,
      customError: result.customError,
      patternMismatch: result.patternMismatch,
      rangeOverflow: result.rangeOverflow,
      rangeUnderflow: result.rangeUnderflow,
      stepMismatch: result.stepMismatch,
      tooLong: result.tooLong,
      tooShort: result.tooShort,
      typeMismatch: result.typeMismatch,
      valid: result.valid,
      valueMissing: result.valueMissing,
    };
  }

  return result;
}

export async function invokeVoidMethod(elementId, methodName) {
  const element = getElementOrThrow(elementId);

  await element[methodName]();
}
