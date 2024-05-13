// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------
export function setTreeModel(id, treeModel) {
    try {
        const element = document.getElementById(id);
        element.model = JSON.parse(treeModel);
        console.log(element, JSON.parse(treeModel));
    }
    catch {

    }
}

export function setTreeContext(id, treeContext) {
    try {
        const element = document.getElementById(id);
        element.context = JSON.parse(treeContext);
    }
    catch {

    }
}