// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

import { getElement } from "./elementUtils.js";

export function setChecked(elementId, value) {
    const element = getElement(elementId);
    if (element) {
        element.checked = JSON.parse(value);
    }
}

export function setIndeterminate(elementId, value) {
    const element = getElement(elementId);
    if (element) {
        element.indeterminate = JSON.parse(value);
    }
}

export function setValue(elementId, value) {
    const element = getElement(elementId);
    if (element) {
        element.value = JSON.parse(value);
    }
}
