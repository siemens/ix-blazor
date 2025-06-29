// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

/**
 * Validates input parameters for category filter operations
 * @param {string} id - Element ID
 * @param {string} data - Data to be processed
 * @param {string} operation - Operation name for error reporting
 * @throws {Error} When validation fails
 */
function validateInputs(id, data, operation) {
  if (!id || typeof id !== 'string') {
    throw new Error(`Invalid ID provided for ${operation}`);
  }
  
  if (data === null || data === undefined) {
    throw new Error(`Invalid data provided for ${operation}`);
  }
}

/**
 * Gets DOM element by ID with error handling
 * @param {string} id - Element ID
 * @param {string} operation - Operation name for error reporting
 * @returns {HTMLElement} The DOM element
 * @throws {Error} When element is not found
 */
function getElementById(id, operation) {
  const element = document.getElementById(id);
  
  if (!element) {
    throw new Error(`Element with ID '${id}' not found for ${operation}`);
  }
  
  return element;
}

/**
 * Parses JSON data with enhanced error handling
 * @param {string} data - JSON string to parse
 * @param {string} operation - Operation name for error reporting
 * @returns {any} Parsed JSON object
 * @throws {Error} When JSON parsing fails
 */
function parseJsonData(data, operation) {
  try {
    return JSON.parse(data);
  } catch (err) {
    throw new Error(`Failed to parse JSON data for ${operation}: ${err.message}`);
  }
}

/**
 * Generic function to set element property with proper error handling
 * @param {string} id - Element ID
 * @param {string} data - JSON data to set
 * @param {string} property - Property name to set on element
 * @param {string} operation - Operation name for error reporting
 * @param {Function} [dataTransform] - Optional function to transform parsed data
 */
function setElementProperty(id, data, property, operation, dataTransform = null) {
  try {
    validateInputs(id, data, operation);
    
    const element = getElementById(id, operation);
    let parsedData = parseJsonData(data, operation);
    
    if (dataTransform && typeof dataTransform === 'function') {
      parsedData = dataTransform(parsedData);
    }
    
    element[property] = parsedData;
  } catch (err) {
    console.error(`Failed to ${operation}:`, err.message);
  }
}

/**
 * Sets categories for a category filter element
 * @param {string} id - Element ID
 * @param {string} categories - JSON string containing categories data
 */
export function setCategories(id, categories) {
  setElementProperty(id, categories, 'categories', 'set categories');
}

/**
 * Sets filter state for a category filter element
 * @param {string} id - Element ID
 * @param {string} filterState - JSON string containing filter state data
 */
export function setFilterState(id, filterState) {
  setElementProperty(id, filterState, 'filterState', 'set filter state');
}

/**
 * Sets non-selectable categories for a category filter element
 * @param {string} id - Element ID
 * @param {string} nonSelectableCategories - JSON string containing non-selectable categories data
 */
export function setNonSelectableCategories(id, nonSelectableCategories) {
  setElementProperty(id, nonSelectableCategories, 'nonSelectableCategories', 'set non-selectable categories');
}

/**
 * Sets suggestions for a category filter element
 * @param {string} id - Element ID
 * @param {string} suggestionsObject - JSON string containing suggestions data
 */
export function setSuggestions(id, suggestionsObject) {
  setElementProperty(
    id, 
    suggestionsObject, 
    'suggestions', 
    'set suggestions',
    (data) => data.suggestions
  );
}

/**
 * Sets static operator for a category filter element
 * @param {string} id - Element ID
 * @param {string} logicalFilter - JSON string containing logical filter data
 */
export function setStaticOperator(id, logicalFilter) {
  setElementProperty(id, logicalFilter, 'staticOperator', 'set static operator');
}
