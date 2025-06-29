// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

/**
 * Validates input parameters for date dropdown operations
 * @param {string} id - Element ID
 * @param {string} data - Data to be processed
 * @param {string} operation - Operation name for error reporting
 * @throws {Error} When validation fails
 */
function validateDateDropdownInputs(id, data, operation) {
  if (!id || typeof id !== 'string') {
    throw new Error(`Invalid ID provided for ${operation}`);
  }
  
  if (data === null || data === undefined) {
    throw new Error(`Invalid data provided for ${operation}`);
  }
  
  if (typeof data !== 'string') {
    throw new Error(`Data must be a JSON string for ${operation}`);
  }
}

/**
 * Gets DOM element by ID with error handling for date dropdown operations
 * @param {string} id - Element ID
 * @param {string} operation - Operation name for error reporting
 * @returns {HTMLElement} The DOM element
 * @throws {Error} When element is not found
 */
function getDateDropdownElement(id, operation) {
  const element = document.getElementById(id);
  
  if (!element) {
    throw new Error(`Element with ID '${id}' not found for ${operation}`);
  }
  
  return element;
}

/**
 * Parses and validates date range options JSON
 * @param {string} dateRangeOptions - JSON string containing date range options
 * @param {string} operation - Operation name for error reporting
 * @returns {Object} Parsed date range options object
 * @throws {Error} When JSON parsing fails or options are invalid
 */
function parseDateRangeOptions(dateRangeOptions, operation) {
  let parsedOptions;
  
  try {
    parsedOptions = JSON.parse(dateRangeOptions);
  } catch (err) {
    throw new Error(`Failed to parse date range options JSON for ${operation}: ${err.message}`);
  }
  
  if (typeof parsedOptions !== 'object' || parsedOptions === null) {
    throw new Error(`Date range options must be a valid JSON object for ${operation}`);
  }
  
  return parsedOptions;
}

/**
 * Validates date range options structure
 * @param {Object} options - Parsed date range options
 * @param {string} operation - Operation name for error reporting
 * @throws {Error} When options structure is invalid
 */
function validateDateRangeOptions(options, operation) {
  // Basic validation - can be extended based on expected structure
  if (Array.isArray(options)) {
    // If it's an array, validate each option has required properties
    options.forEach((option, index) => {
      if (typeof option !== 'object' || option === null) {
        throw new Error(`Date range option at index ${index} must be an object for ${operation}`);
      }
    });
  } else if (typeof options === 'object') {
    // If it's an object, basic validation passed
    // Additional specific validations can be added here
  } else {
    throw new Error(`Date range options must be an object or array for ${operation}`);
  }
}

/**
 * Sets date range options for a date dropdown element
 * @param {string} id - ID of the DOM element to configure
 * @param {string} dateRangeOptions - JSON string containing date range options
 * @returns {boolean} True if options were successfully set, false otherwise
 */
export function setDateRangeOptions(id, dateRangeOptions) {
  const operation = 'set date range options';
  
  try {
    // Validate input parameters
    validateDateDropdownInputs(id, dateRangeOptions, operation);
    
    // Get the target element
    const element = getDateDropdownElement(id, operation);
    
    // Parse and validate date range options
    const parsedOptions = parseDateRangeOptions(dateRangeOptions, operation);
    
    // Validate options structure
    validateDateRangeOptions(parsedOptions, operation);
    
    // Set the options on the element
    element.dateRangeOptions = parsedOptions;
    
    return true;
  } catch (error) {
    console.error(`Failed to ${operation}:`, error.message);
    return false;
  }
}

/**
 * Gets the current date range options from a date dropdown element
 * @param {string} id - ID of the DOM element to get options from
 * @returns {Object|null} The current date range options, or null if not found/invalid
 */
export function getDateRangeOptions(id) {
  const operation = 'get date range options';
  
  try {
    if (!id || typeof id !== 'string') {
      throw new Error(`Invalid ID provided for ${operation}`);
    }
    
    const element = getDateDropdownElement(id, operation);
    
    return element.dateRangeOptions || null;
  } catch (error) {
    console.error(`Failed to ${operation}:`, error.message);
    return null;
  }
}
