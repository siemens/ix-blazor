// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

/**
 * Validates input parameters for settings menu operations
 * @param {string} id - Element ID
 * @param {boolean} status - Toggle status
 * @param {string} operation - Operation name for error reporting
 * @throws {Error} When validation fails
 */
function validateSettingsMenuInputs(id, status, operation) {
  if (!id || typeof id !== 'string') {
    throw new Error(`Invalid ID provided for ${operation}`);
  }
  
  if (typeof status !== 'boolean') {
    throw new Error(`Status must be a boolean value for ${operation}`);
  }
}

/**
 * Gets DOM element by ID with error handling for settings menu operations
 * @param {string} id - Element ID
 * @param {string} operation - Operation name for error reporting
 * @returns {HTMLElement} The DOM element
 * @throws {Error} When element is not found
 */
function getSettingsMenuElement(id, operation) {
  const element = document.getElementById(id);
  
  if (!element) {
    throw new Error(`Element with ID '${id}' not found for ${operation}`);
  }
  
  return element;
}

/**
 * Validates that the element has the required toggleSettings method
 * @param {HTMLElement} element - The DOM element
 * @param {string} operation - Operation name for error reporting
 * @throws {Error} When element doesn't have the required method
 */
function validateSettingsMenuElement(element, operation) {
  if (typeof element.toggleSettings !== 'function') {
    throw new Error(`Element does not support toggleSettings method for ${operation}`);
  }
}

/**
 * Toggles the settings menu state for a specific element
 * @param {string} id - ID of the DOM element containing the settings menu
 * @param {boolean} status - True to show the settings menu, false to hide it
 * @returns {Promise<boolean>} Promise that resolves to true if toggle was successful, false otherwise
 */
export async function toggleSettings(id, status) {
  const operation = 'toggle settings menu';
  
  try {
    // Validate input parameters
    validateSettingsMenuInputs(id, status, operation);
    
    // Get the target element
    const element = getSettingsMenuElement(id, operation);
    
    // Validate element capabilities
    validateSettingsMenuElement(element, operation);
    
    // Toggle the settings menu
    await element.toggleSettings(status);
    
    return true;
  } catch (error) {
    console.error(`Failed to ${operation}:`, error.message);
    return false;
  }
}
