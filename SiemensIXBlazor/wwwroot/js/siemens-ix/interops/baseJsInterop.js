// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

const eventListeners = new Map();
let nextEventListenerId = 0;

export function listenEvent(caller, elementId, eventName, functionName, includeDetail = true) {
  const element = document.getElementById(elementId);

  if (!element) {
    throw new Error(`Element with ID ${elementId} not found`);
  }

  const listenerId = `base-listener-${++nextEventListenerId}`;
  const listener = (e) => {
    if (includeDetail) {
      caller.invokeMethodAsync(functionName, e.detail);
    } else {
      caller.invokeMethodAsync(functionName);
    }
  };
  element.addEventListener(eventName, listener);
  eventListeners.set(listenerId, { element, eventName, listener });
  return listenerId;
}

export function removeEventListener(listenerId) {
  const registration = eventListeners.get(listenerId);
  if (!registration) {
    return;
  }

  registration.element.removeEventListener(registration.eventName, registration.listener);
  eventListeners.delete(listenerId);
}

export function setElementProperty(elementId, propertyName, propertyValue) {
  const element = document.getElementById(elementId);

  if (!element) {
    throw new Error(`Element with ID ${elementId} not found`);
  }

  element[propertyName] = propertyValue;
}

export function invokeElementMethod(elementId, methodName) {
  const element = document.getElementById(elementId);

  if (!element) {
    throw new Error(`Element with ID ${elementId} not found`);
  }

  if (typeof element[methodName] !== "function") {
    throw new Error(`Method ${methodName} not found on element ${elementId}`);
  }

  return element[methodName]();
}

export async function invokeMethod(elementId, methodName) {
  const element = document.getElementById(elementId);

  if (!element) {
    throw new Error(`Element with ID ${elementId} not found`);
  }

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
  const element = document.getElementById(elementId);

  if (!element) {
    throw new Error(`Element with ID ${elementId} not found`);
  }

  await element[methodName]();
}
