// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

import { defineCustomElements } from "@siemens/ix/loader";
import { toast } from "@siemens/ix";
import "@siemens/ix-echarts";
import { registerTheme } from "@siemens/ix-echarts";
import * as echarts from "echarts";
import { themeSwitcher } from "@siemens/ix";
import { Grid } from "ag-grid-community";
import { defineCustomElements as ixIconsDefineCustomElements } from "@siemens/ix-icons/loader";

/**
 * Validates input parameters for chart initialization
 * @param {string} id - Element ID
 * @param {string} options - Chart options JSON string
 * @param {string} operation - Operation name for error reporting
 * @throws {Error} When validation fails
 */
function validateChartInputs(id, options, operation) {
  if (!id || typeof id !== 'string') {
    throw new Error(`Invalid ID provided for ${operation}`);
  }
  
  if (!options || typeof options !== 'string') {
    throw new Error(`Chart options must be a JSON string for ${operation}`);
  }
}

/**
 * Validates input parameters for AG Grid operations
 * @param {Object} dotnetRef - .NET reference object
 * @param {string} elementId - Element ID
 * @param {string} gridOptions - Grid options JSON string
 * @param {string} operation - Operation name for error reporting
 * @throws {Error} When validation fails
 */
function validateGridInputs(dotnetRef, elementId, gridOptions, operation) {
  if (!dotnetRef) {
    throw new Error(`DotNet reference is required for ${operation}`);
  }
  
  if (!elementId || typeof elementId !== 'string') {
    throw new Error(`Invalid element ID provided for ${operation}`);
  }
  
  if (!gridOptions || typeof gridOptions !== 'string') {
    throw new Error(`Grid options must be a JSON string for ${operation}`);
  }
  
  if (typeof dotnetRef.invokeMethodAsync !== 'function') {
    throw new Error(`DotNet reference must have invokeMethodAsync method for ${operation}`);
  }
}

/**
 * Gets DOM element by ID with error handling
 * @param {string} id - Element ID
 * @param {string} operation - Operation name for error reporting
 * @returns {HTMLElement} The DOM element
 * @throws {Error} When element is not found
 */
function getElementByIdSafe(id, operation) {
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
 * @returns {Object} Parsed JSON object
 * @throws {Error} When JSON parsing fails
 */
function parseJsonSafe(data, operation) {
  try {
    return JSON.parse(data);
  } catch (err) {
    throw new Error(`Failed to parse JSON data for ${operation}: ${err.message}`);
  }
}

/**
 * Validates theme parameter
 * @param {string} theme - Theme name
 * @param {string} operation - Operation name for error reporting
 * @throws {Error} When theme is invalid
 */
function validateTheme(theme, operation) {
  if (!theme || typeof theme !== 'string') {
    throw new Error(`Valid theme name is required for ${operation}`);
  }
}

/**
 * Creates a safe cell click event handler for AG Grid
 * @param {Object} dotnetRef - .NET reference object
 * @returns {Function} Event handler function
 */
function createSafeCellClickHandler(dotnetRef) {
  return async (event) => {
    try {
      await dotnetRef.invokeMethodAsync("OnCellClickedCallback", event.data);
    } catch (error) {
      console.error("Failed to invoke cell click callback:", error.message);
    }
  };
}

window.siemensIXInterop = {
  /**
   * Initializes the Siemens IX components and icons
   * @returns {Promise<void>} Promise that resolves when initialization is complete
   */
  async initialize() {
    const operation = 'initialize Siemens IX components';
    
    try {
      // Initialize IX Icons with error handling
      await ixIconsDefineCustomElements(window, {
        resourcesUrl: "/_content/Siemens.IX.Blazor/"
      });

      // Initialize IX Custom Elements
      await defineCustomElements();
      
    } catch (error) {
      console.error(`Failed to ${operation}:`, error.message);
      throw error; // Re-throw to allow caller to handle initialization failure
    }
  },

  /**
   * Shows a toast message with the provided configuration
   * @param {string} config - JSON string containing toast configuration
   */
  showMessage(config) {
    const operation = 'show toast message';
    
    try {
      if (!config || typeof config !== 'string') {
        throw new Error('Toast configuration must be a JSON string');
      }
      
      const toastConfig = parseJsonSafe(config, operation);
      
      if (typeof toastConfig !== 'object' || toastConfig === null) {
        throw new Error('Toast configuration must be a valid object');
      }
      
      toast(toastConfig);
      
    } catch (error) {
      console.error(`Failed to ${operation}:`, error.message);
    }
  },

  /**
   * Initializes an ECharts chart with the provided options
   * @param {string} id - ID of the element to render the chart in
   * @param {string} options - JSON string containing chart options
   */
  initializeChart(id, options) {
    const operation = 'initialize chart';
    
    try {
      // Validate input parameters
      validateChartInputs(id, options, operation);
      
      // Get the target element
      const element = getElementByIdSafe(id, operation);
      
      // Parse chart options
      const chartOptions = parseJsonSafe(options, operation);
      
      if (typeof chartOptions !== 'object' || chartOptions === null) {
        throw new Error('Chart options must be a valid object');
      }
      
      // Register theme and initialize chart
      registerTheme(echarts);
      const myChart = echarts.init(element, window.demoTheme);
      myChart.setOption(chartOptions);
      
    } catch (error) {
      console.error(`Failed to ${operation}:`, error.message);
    }
  },

  /**
   * Sets the application theme
   * @param {string} theme - Theme name to set
   */
  setTheme(theme) {
    const operation = 'set theme';
    
    try {
      validateTheme(theme, operation);
      themeSwitcher.setTheme(theme);
      
    } catch (error) {
      console.error(`Failed to ${operation}:`, error.message);
    }
  },

  /**
   * Toggles between light and dark theme modes
   */
  toggleTheme() {
    const operation = 'toggle theme';
    
    try {
      themeSwitcher.toggleMode();
      
    } catch (error) {
      console.error(`Failed to ${operation}:`, error.message);
    }
  },

  /**
   * Toggles system theme usage
   * @param {boolean} useSystemTheme - Whether to use system theme
   */
  toggleSystemTheme(useSystemTheme) {
    const operation = 'toggle system theme';
    
    try {
      if (typeof useSystemTheme !== 'boolean') {
        throw new Error('useSystemTheme must be a boolean value');
      }
      
      if (useSystemTheme) {
        themeSwitcher.setVariant();
      } else {
        console.warn("System theme switching is disabled.");
      }
      
    } catch (error) {
      console.error(`Failed to ${operation}:`, error.message);
    }
  },

  /**
   * AG Grid interop functionality
   */
  agGridInterop: {
    dotnetReference: null,

    /**
     * Creates a new AG Grid instance
     * @param {Object} dotnetRef - .NET reference object
     * @param {string} elementId - ID of the element to render the grid in
     * @param {string} gridOptions - JSON string containing grid options
     * @returns {Grid|null} AG Grid instance or null if creation failed
     */
    createGrid(dotnetRef, elementId, gridOptions) {
      const operation = 'create AG Grid';
      
      try {
        // Validate input parameters
        validateGridInputs(dotnetRef, elementId, gridOptions, operation);
        
        // Get the target element
        const element = getElementByIdSafe(elementId, operation);
        
        // Parse grid options
        const parsedOptions = parseJsonSafe(gridOptions, operation);
        
        if (typeof parsedOptions !== 'object' || parsedOptions === null) {
          throw new Error('Grid options must be a valid object');
        }
        
        // Store .NET reference
        this.dotnetReference = dotnetRef;
        
        // Add safe cell click handler
        parsedOptions.onCellClicked = createSafeCellClickHandler(dotnetRef);
        
        // Create and return grid instance
        return new Grid(element, parsedOptions);
        
      } catch (error) {
        console.error(`Failed to ${operation}:`, error.message);
        return null;
      }
    },

    /**
     * Sets data for an existing AG Grid instance
     * @param {Grid} grid - AG Grid instance
     * @param {Array} data - Data to set in the grid
     */
    setData(grid, data) {
      const operation = 'set AG Grid data';
      
      try {
        if (!grid) {
          throw new Error('Grid instance is required');
        }
        
        if (!grid.gridOptions || !grid.gridOptions.api) {
          throw new Error('Grid instance does not have a valid API');
        }
        
        if (!Array.isArray(data)) {
          throw new Error('Data must be an array');
        }
        
        grid.gridOptions.api.setRowData(data);
        
      } catch (error) {
        console.error(`Failed to ${operation}:`, error.message);
      }
    },

    /**
     * Gets selected rows from an AG Grid instance
     * @param {Grid} grid - AG Grid instance
     * @returns {Array|null} Array of selected rows or null if operation failed
     */
    getSelectedRows(grid) {
      const operation = 'get AG Grid selected rows';
      
      try {
        if (!grid) {
          throw new Error('Grid instance is required');
        }
        
        if (!grid.gridOptions || !grid.gridOptions.api) {
          throw new Error('Grid instance does not have a valid API');
        }
        
        return grid.gridOptions.api.getSelectedRows();
        
      } catch (error) {
        console.error(`Failed to ${operation}:`, error.message);
        return null;
      }
    },

    /**
     * Disposes of AG Grid resources and cleans up references
     */
    dispose() {
      const operation = 'dispose AG Grid resources';
      
      try {
        this.dotnetReference = null;
        
      } catch (error) {
        console.error(`Failed to ${operation}:`, error.message);
      }
    },
  },
};

// Initialize immediately with error handling
(async () => {
  try {
    await siemensIXInterop.initialize();
  } catch (error) {
    console.error('Failed to initialize Siemens IX Interop:', error.message);
  }
})();
