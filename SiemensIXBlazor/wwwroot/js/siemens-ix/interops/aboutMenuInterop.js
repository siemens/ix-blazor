// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

/**
 * Validates input parameters for about menu operations
 * @param {string} id - Element ID
 * @param {boolean} status - Toggle status
 * @param {string} operation - Operation name for error reporting
 * @throws {Error} When validation fails
 */
function validateAboutMenuInputs(id, status, operation) {
  if (!id || typeof id !== 'string') {
    throw new Error(`Invalid ID provided for ${operation}`);
  }
  
  if (typeof status !== 'boolean') {
    throw new Error(`Status must be a boolean value for ${operation}`);
  }
}

/**
 * Gets DOM element by ID with error handling for about menu operations
 * @param {string} id - Element ID
 * @param {string} operation - Operation name for error reporting
 * @returns {HTMLElement} The DOM element
 * @throws {Error} When element is not found
 */
function getAboutMenuElement(id, operation) {
  const element = document.getElementById(id);
  
  if (!element) {
    throw new Error(`Element with ID '${id}' not found for ${operation}`);
  }
  
  return element;
}

/**
 * Validates that the element has the required toggleAbout method
 * @param {HTMLElement} element - The DOM element
 * @param {string} operation - Operation name for error reporting
 * @throws {Error} When element doesn't have the required method
 */
function validateAboutMenuElement(element, operation) {
  if (typeof element.toggleAbout !== 'function') {
    throw new Error(`Element does not support toggleAbout method for ${operation}`);
  }
}

/**
 * Toggles the about menu state for a specific element
 * @param {string} id - ID of the DOM element containing the about menu
 * @param {boolean} status - True to show the about menu, false to hide it
 * @returns {Promise<boolean>} Promise that resolves to true if toggle was successful, false otherwise
 */
export async function toggleAbout(id, status) {
  const operation = 'toggle about menu';
  
  try {
    // Validate input parameters
    validateAboutMenuInputs(id, status, operation);
    
    // Get the target element
    const element = getAboutMenuElement(id, operation);
    
    // Validate element capabilities
    validateAboutMenuElement(element, operation);
    
    // Toggle the about menu
    await element.toggleAbout(status);
    
    return true;
  } catch (error) {
    console.error(`Failed to ${operation}:`, error.message);
    return false;
  }
}

/**
 * Gets the current about menu state for a specific element
 * @param {string} id - ID of the DOM element containing the about menu
 * @returns {Promise<boolean|null>} Promise that resolves to the current state, or null if unavailable
 */
export async function getAboutState(id) {
  const operation = 'get about menu state';
  
  try {
    if (!id || typeof id !== 'string') {
      throw new Error(`Invalid ID provided for ${operation}`);
    }
    
    const element = getAboutMenuElement(id, operation);
    
    // Check if element has a method to get about state
    if (typeof element.getAboutState === 'function') {
      return await element.getAboutState();
    }
    
    // Fallback: check for common properties that might indicate state
    if ('aboutVisible' in element) {
      return element.aboutVisible;
    }
    
    if ('isAboutOpen' in element) {
      return element.isAboutOpen;
    }
    
    // If no state method/property is available, return null
    console.warn(`Element with ID '${id}' does not provide about menu state information`);
    return null;
  } catch (error) {
    console.error(`Failed to ${operation}:`, error.message);
    return null;
  }
}
