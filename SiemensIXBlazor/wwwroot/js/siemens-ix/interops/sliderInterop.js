// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

export function setMarker(id, markerArray) {
    const el = document.getElementById(id);
    if (el) {
        el.marker = markerArray;
    }
}

export function listenEvent(dotNetRef, id, eventName, callbackName) {
    const el = document.getElementById(id);
    if (el) {
        el.addEventListener(eventName, e => {
            if (callbackName === "ValueChanged") {
                dotNetRef.invokeMethodAsync(callbackName, e.detail);
            }
        });
    }
}
