import { defineCustomElements, applyPolyfills } from "@siemens/ix/loader/index";
import { toast } from "@siemens/ix";
import "@siemens/ix-echarts";
import { registerTheme } from "@siemens/ix-echarts";
import * as echarts from "echarts";
import { themeSwitcher } from "@siemens/ix";
import { Grid } from "ag-grid-community";
import { defineCustomElements as ixIconsDefineCustomElements } from "@siemens/ix-icons/loader";

window.siemensIXInterop = {
  async initialize() {
    await applyPolyfills();
    await ixIconsDefineCustomElements();
    await defineCustomElements();
  },

  showMessage(config) {
    try {
      const toastConfig = JSON.parse(config);
      toast(toastConfig);
    } catch (error) {
      console.error("Failed to display toast message:", error);
    }
  },

  initializeChart(id, options) {
    try {
      const element = document.getElementById(id);
      if (!element) throw new Error(`Element with ID ${id} not found`);

      registerTheme(echarts);
      const myChart = echarts.init(element, window.demoTheme);
      myChart.setOption(JSON.parse(options));
    } catch (error) {
      console.error("Failed to initialize chart:", error);
    }
  },

  setTheme(theme) {
    themeSwitcher.setTheme(theme);
  },

  toggleTheme() {
    themeSwitcher.toggleMode();
  },

  toggleSystemTheme(useSystemTheme) {
    if (useSystemTheme) {
      themeSwitcher.setVariant();
    } else {
      console.warn("System theme switching is disabled.");
    }
  },

  agGridInterop: {
    dotnetReference: null,

    createGrid(dotnetRef, elementId, gridOptions) {
      const parsedOption = JSON.parse(gridOptions);
      this.dotnetReference = dotnetRef;

      parsedOption.onCellClicked = (event) => {
        dotnetRef.invokeMethodAsync("OnCellClickedCallback", event.data);
      };

      return new Grid(document.getElementById(elementId), parsedOption);
    },

    setData(grid, data) {
      grid.gridOptions.api.setRowData(data);
    },

    getSelectedRows(grid) {
      return grid.gridOptions.api.getSelectedRows();
    },

    dispose() {
      this.dotnetReference = null;
    },
  },
};

(async () => {
  await siemensIXInterop.initialize();
})();
