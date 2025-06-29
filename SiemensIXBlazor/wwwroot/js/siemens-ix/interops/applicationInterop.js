// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

/**
 * Validates input parameters for application configuration operations
 * @param {string} id - Element ID
 * @param {string} config - Configuration data
 * @param {string} operation - Operation name for error reporting
 * @throws {Error} When validation fails
 */
function validateApplicationInputs(id, config, operation) {
  if (!id || typeof id !== 'string') {
    throw new Error(`Invalid ID provided for ${operation}`);
  }
  
  if (config === null || config === undefined) {
    throw new Error(`Invalid configuration data provided for ${operation}`);
  }
  
  if (typeof config !== 'string') {
    throw new Error(`Configuration data must be a JSON string for ${operation}`);
  }
}

/**
 * Gets DOM element by ID with error handling for application operations
 * @param {string} id - Element ID
 * @param {string} operation - Operation name for error reporting
 * @returns {HTMLElement} The DOM element
 * @throws {Error} When element is not found
 */
function getApplicationElement(id, operation) {
  const element = document.getElementById(id);
  
  if (!element) {
    throw new Error(`Element with ID '${id}' not found for ${operation}`);
  }
  
  return element;
}

/**
 * Parses and validates application configuration JSON
 * @param {string} config - JSON configuration string
 * @param {string} operation - Operation name for error reporting
 * @returns {Object} Parsed configuration object
 * @throws {Error} When JSON parsing fails or config is invalid
 */
function parseApplicationConfig(config, operation) {
  let parsedConfig;
  
  try {
    parsedConfig = JSON.parse(config);
  } catch (err) {
    throw new Error(`Failed to parse configuration JSON for ${operation}: ${err.message}`);
  }
  
  if (typeof parsedConfig !== 'object' || parsedConfig === null) {
    throw new Error(`Configuration must be a valid JSON object for ${operation}`);
  }
  
  return parsedConfig;
}

/**
 * Sets application configuration on a DOM element
 * @param {string} id - ID of the DOM element to configure
 * @param {string} config - JSON string containing application configuration
 * @returns {boolean} True if configuration was successfully set, false otherwise
 */
export function setApplicationConfig(id, config) {
  const operation = 'set application configuration';
  
  try {
    // Validate input parameters
    validateApplicationInputs(id, config, operation);
    
    // Get the target element
    const element = getApplicationElement(id, operation);
    
    // Parse and validate configuration
    const parsedConfig = parseApplicationConfig(config, operation);
    
    // Set the configuration on the element
    element.appSwitchConfig = parsedConfig;
    
    return true;
  } catch (error) {
    console.error(`Failed to ${operation}:`, error.message);
    return false;
  }
}

/**
 * Gets the current application configuration from a DOM element
 * @param {string} id - ID of the DOM element to get configuration from
 * @returns {Object|null} The current configuration object, or null if not found/invalid
 */
export function getApplicationConfig(id) {
  const operation = 'get application configuration';
  
  try {
    if (!id || typeof id !== 'string') {
      throw new Error(`Invalid ID provided for ${operation}`);
    }
    
    const element = getApplicationElement(id, operation);
    
    return element.appSwitchConfig || null;
  } catch (error) {
    console.error(`Failed to ${operation}:`, error.message);
    return null;
  }
}

/**
 * Clears the application configuration from a DOM element
 * @param {string} id - ID of the DOM element to clear configuration from
 * @returns {boolean} True if configuration was successfully cleared, false otherwise
 */
export function clearApplicationConfig(id) {
  const operation = 'clear application configuration';
  
  try {
    if (!id || typeof id !== 'string') {
      throw new Error(`Invalid ID provided for ${operation}`);
    }
    
    const element = getApplicationElement(id, operation);
    
    element.appSwitchConfig = null;
    
    return true;
  } catch (error) {
    console.error(`Failed to ${operation}:`, error.message);
    return false;
  }
}
