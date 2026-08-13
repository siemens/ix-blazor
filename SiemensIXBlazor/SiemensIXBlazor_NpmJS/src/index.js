import { defineCustomElements } from "@siemens/ix/loader";
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
            resourcesUrl: "./_content/Siemens.IX.Blazor/"
        });

        await defineCustomElements();
    },
    modal: {
        show(id) {
            const el = document.getElementById(id);
            if (el) {
                el.showModal();
            } else {
                console.error(`[siemensIXInterop.modal.show] Element with id '${id}' not found.`);
            }
        },
        hide(id) {
            const el = document.getElementById(id);
            if (el) {
                el.hideModal();
            } else {
                console.error(`[siemensIXInterop.modal.hide] Element with id '${id}' not found.`);
            }
        },
        toggle(id) {
            const el = document.getElementById(id);
            if (el) {
                if (el.hasAttribute('open')) {
                    el.hideModal();
                } else {
                    el.showModal();
                }
            } else {
                console.error(`[siemensIXInterop.modal.toggle] Element with id '${id}' not found.`);
            }
        },
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
