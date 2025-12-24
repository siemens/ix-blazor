import { defineCustomElements } from "@siemens/ix/loader";
import { toast, setToastPosition } from "@siemens/ix";
import "@siemens/ix-echarts";
import { registerTheme } from "@siemens/ix-echarts";
import * as echarts from "echarts";
import { themeSwitcher } from "@siemens/ix";
import { Grid } from "ag-grid-community";
import { defineCustomElements as ixIconsDefineCustomElements } from "@siemens/ix-icons/loader";

window.echarts = echarts;

window.siemensIXInterop = {
  async initialize() {
    await ixIconsDefineCustomElements(window, {
      resourcesUrl: "/_content/Siemens.IX.Blazor/"
    });

    await defineCustomElements();
  },

  showMessage(config) {
    try {
      const toastConfig = JSON.parse(config);
      if (toastConfig.messageHtml) {
        const msgEl = document.createElement('div');
        msgEl.innerHTML = toastConfig.messageHtml;
        toastConfig.message = msgEl;
      }

      if (toastConfig.action) {
        const actionEl = document.createElement('div');
        actionEl.innerHTML = toastConfig.action;
        actionEl.slot = 'action';
        toastConfig.action = actionEl;
      }

      if (toastConfig.position) {
        setToastPosition(toastConfig.position);
        delete toastConfig.position;
      }

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
      
      const parsedOptions = JSON.parse(options);
      
      if (parsedOptions.series) {
        parsedOptions.series.forEach(series => {
          if (series.equation && series.equation.z && typeof series.equation.z === 'string') {
            series.equation.z = eval('(' + series.equation.z + ')');
          }
        });
      }
      
      let myChart = echarts.init(element, themeSwitcher.getCurrentTheme());
      myChart.setOption(parsedOptions);

      themeSwitcher.themeChanged.on((theme) => {
        myChart.dispose();
        myChart = echarts.init(element, theme);
        myChart.setOption(parsedOptions);
      });
      
      window.addEventListener('resize', () => {
        if (myChart && !myChart.isDisposed()) {
          myChart.resize();
        }
      });
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
