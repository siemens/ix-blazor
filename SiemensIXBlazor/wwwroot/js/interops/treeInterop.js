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
    if (!element) {
      throw new Error(`Element with ID ${id} not found`);
    }
    element.model = JSON.parse(treeModel);
  } catch {
    console.error("Failed to set tree model:", error);
  }
}

export function setTreeContext(id, treeContext) {
  try {
    const element = document.getElementById(id);
    if (!element) {
      throw new Error(`Element with ID ${id} not found`);
    }
    element.context = JSON.parse(treeContext);
  } catch {
    console.error("Failed to set tree context:", error);
  }
}
