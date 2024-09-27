// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

export function setCategories(id, categories) {
  try {
    const element = document.getElementById(id);
    if (!element) {
      throw new Error(`Element with ID ${id} not found`);
    }
    element.categories = JSON.parse(categories);
  } catch (err) {
    console.error("Failed to set categories:", err);
  }
}

export function setFilterState(id, filterState) {
  try {
    const element = document.getElementById(id);
    if (!element) {
      throw new Error(`Element with ID ${id} not found`);
    }
    element.filterState = JSON.parse(filterState);
  } catch (err) {
    console.error("Failed to set filter state:", err);
  }
}

export function setNonSelectableCategories(id, nonSelectableCategories) {
  try {
    const element = document.getElementById(id);
    if (!element) {
      throw new Error(`Element with ID ${id} not found`);
    }
    element.nonSelectableCategories = JSON.parse(nonSelectableCategories);
  } catch {
    console.error("Failed to set non-selectable categories:", error);
  }
}

export function setSuggestions(id, suggestionsObject) {
  try {
    const element = document.getElementById(id);
    if (!element) {
      throw new Error(`Element with ID ${id} not found`);
    }
    element.suggestions = JSON.parse(suggestionsObject).suggestions;
  } catch {
    console.error("Failed to set suggestions:", error);
  }
}
