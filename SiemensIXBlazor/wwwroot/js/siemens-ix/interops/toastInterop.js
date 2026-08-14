// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

const toastHandles = new Map();
const closeToastListeners = new Map();
let nextHandle = 0;

function getElement(id) {
  const element = document.getElementById(id);
  if (!element) {
    throw new Error(`Element with ID ${id} not found`);
  }

  return element;
}

export function listenCloseToast(dotNetReference, id) {
  const element = getElement(id);
  removeCloseToast(id);

  const listener = () => dotNetReference.invokeMethodAsync("CloseToast");
  element.addEventListener("closeToast", listener);
  closeToastListeners.set(id, { element, listener });
}

export function removeCloseToast(id) {
  const entry = closeToastListeners.get(id);
  if (!entry) {
    return;
  }

  entry.element.removeEventListener("closeToast", entry.listener);
  closeToastListeners.delete(id);
}

export function pauseToast(id) {
  return getElement(id).pause();
}

export function resumeToast(id) {
  return getElement(id).resume();
}

export function isToastPaused(id) {
  return getElement(id).isPaused();
}

export async function showToast(dotNetReference, containerId, configJson) {
  const container = getElement(containerId);
  const config = JSON.parse(configJson);

  if (typeof config.action === "string") {
    const action = document.createElement("div");
    action.innerHTML = config.action;
    config.action = action;
  }

  const result = await container.showToast(config);
  const handle = String(++nextHandle);
  result.onClose.on((value) => {
    toastHandles.delete(handle);
    dotNetReference.invokeMethodAsync("ToastClosed", handle, value ?? null);
  });
  toastHandles.set(handle, { result, containerId });
  return handle;
}

function getResult(handle) {
  const entry = toastHandles.get(handle);
  if (!entry) {
    throw new Error(`Toast handle ${handle} not found`);
  }

  return entry.result;
}

export function pause(handle) {
  return getResult(handle).pause();
}

export function resume(handle) {
  return getResult(handle).resume();
}

export function isPaused(handle) {
  return getResult(handle).isPaused();
}

export function close(handle, result) {
  return getResult(handle).close(result);
}

export function dispose(containerId) {
  for (const [handle, entry] of toastHandles) {
    if (entry.containerId === containerId) {
      toastHandles.delete(handle);
    }
  }
  for (const id of closeToastListeners.keys()) {
    removeCloseToast(id);
  }
}
