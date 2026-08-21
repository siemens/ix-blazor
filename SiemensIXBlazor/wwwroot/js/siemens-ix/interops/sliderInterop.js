// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

import { createListenerRegistry } from "./listenerRegistry.js";
import { getElement } from "./elementUtils.js";

const listeners = createListenerRegistry("slider-listener");

export function setMarker(id, markerArray) {
    const el = getElement(id);
    if (el) {
        el.marker = markerArray;
    }
}

export function listenEvent(dotNetRef, id, eventName, callbackName) {
    const el = getElement(id);
    if (!el) {
        return null;
    }

    const listener = e => {
        if (callbackName === "ValueChanged") {
            dotNetRef.invokeMethodAsync(callbackName, e.detail);
        }
    };

    return listeners.add(el, eventName, listener);
}

export function removeEventListener(listenerId) {
    listeners.remove(listenerId);
}
