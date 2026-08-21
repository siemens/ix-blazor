// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

import { getElementOrThrow } from "./elementUtils.js";

const listeners = new Map();

export function initialize(caller, id) {
  dispose(id);

  const element = getElementOrThrow(id);
  let resumeClear = false;
  let clearPending = false;
  let active = true;

  const categoryChanged = (event) => caller.invokeMethodAsync("CategoryChanged", event.detail ?? null);
  const filterChanged = (event) => caller.invokeMethodAsync("FilterChanged", event.detail);
  const inputChanged = (event) => caller.invokeMethodAsync("InputChanged", event.detail);
  const filterCleared = async (event) => {
    if (resumeClear) {
      resumeClear = false;
      return;
    }

    event.preventDefault();
    if (clearPending) {
      return;
    }

    clearPending = true;
    try {
      const cancel = await caller.invokeMethodAsync("FilterCleared");
      if (!cancel && active && element.isConnected) {
        const resetButton = element.shadowRoot?.querySelector(".reset-button");
        if (resetButton) {
          resumeClear = true;
          resetButton.click();
        }
      }
    } finally {
      clearPending = false;
    }
  };

  element.addEventListener("categoryChanged", categoryChanged);
  element.addEventListener("filterChanged", filterChanged);
  element.addEventListener("inputChanged", inputChanged);
  element.addEventListener("filterCleared", filterCleared);

  listeners.set(id, () => {
    active = false;
    element.removeEventListener("categoryChanged", categoryChanged);
    element.removeEventListener("filterChanged", filterChanged);
    element.removeEventListener("inputChanged", inputChanged);
    element.removeEventListener("filterCleared", filterCleared);
  });
}

export function dispose(id) {
  listeners.get(id)?.();
  listeners.delete(id);
}

export function setCategories(id, categories) {
  try {
    getElementOrThrow(id).categories = categories ?? undefined;
  } catch (err) {
    console.error("Failed to set categories:", err);
  }
}

export function setFilterState(id, filterState) {
  try {
    getElementOrThrow(id).filterState = filterState ?? undefined;
  } catch (err) {
    console.error("Failed to set filter state:", err);
  }
}

export function setNonSelectableCategories(id, nonSelectableCategories) {
  try {
    getElementOrThrow(id).nonSelectableCategories = nonSelectableCategories ?? undefined;
  } catch (error) {
    console.error("Failed to set non-selectable categories:", error);
  }
}

export function setSuggestions(id, suggestionsObject) {
  try {
    getElementOrThrow(id).suggestions = suggestionsObject ?? undefined;
  } catch (error) {
    console.error("Failed to set suggestions:", error);
  }
}

export function setStaticOperator(id, logicalFilter) {
    try {
        getElementOrThrow(id).staticOperator = logicalFilter ?? undefined;
    } catch (err) {
        console.error("Failed on setting staticOperator", err);
    }
}
