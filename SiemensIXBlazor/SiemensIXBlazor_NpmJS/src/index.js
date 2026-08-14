import { defineCustomElements } from "@siemens/ix/loader";
import "@siemens/ix-echarts";
import { registerTheme } from "@siemens/ix-echarts";
import * as echarts from "echarts";
import { showModalLoading, themeSwitcher } from "@siemens/ix";
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
        async show(id) {
            const el = document.getElementById(id);
            if (el) {
                await el.showModal();
            } else {
                console.error(`[siemensIXInterop.modal.show] Element with id '${id}' not found.`);
            }
        },
        async close(id, reason) {
            const el = document.getElementById(id);
            if (el) {
                await el.closeModal(reason);
            } else {
                console.error(`[siemensIXInterop.modal.close] Element with id '${id}' not found.`);
            }
        },
        async dismiss(id, reason) {
            const el = document.getElementById(id);
            if (el) {
                await el.dismissModal(reason);
            } else {
                console.error(`[siemensIXInterop.modal.dismiss] Element with id '${id}' not found.`);
            }
        },
        attach(id, dotnetReference) {
            const el = document.getElementById(id);
            if (!el) {
                throw new Error(`Element with id '${id}' not found`);
            }

            this.detach(id);

            const beforeDismiss = (reason) =>
                dotnetReference.invokeMethodAsync('BeforeDismiss', reason);
            const dialogClose = (event) =>
                dotnetReference.invokeMethodAsync('DialogClose', event.detail);
            const dialogDismiss = (event) =>
                dotnetReference.invokeMethodAsync('DialogDismiss', event.detail);

            el.beforeDismiss = beforeDismiss;
            el.addEventListener('dialogClose', dialogClose);
            el.addEventListener('dialogDismiss', dialogDismiss);
            el.__siemensIxModalListeners = { beforeDismiss, dialogClose, dialogDismiss, dotnetReference };
        },
        detach(id) {
            const el = document.getElementById(id);
            const listeners = el?.__siemensIxModalListeners;
            if (!el || !listeners) {
                return;
            }

            el.removeEventListener('dialogClose', listeners.dialogClose);
            el.removeEventListener('dialogDismiss', listeners.dialogDismiss);
            el.beforeDismiss = undefined;
            delete el.__siemensIxModalListeners;
        },
        showLoading(options) {
            return showModalLoading(options);
        },
    },

    modalHeader: {
        attach(id, dotnetReference) {
            const el = document.getElementById(id);
            if (!el) {
                throw new Error(`Element with id '${id}' not found`);
            }

            this.detach(id);
            const closeClick = (event) =>
                dotnetReference.invokeMethodAsync('CloseClick', event.detail);
            el.addEventListener('closeClick', closeClick);
            el.__siemensIxModalHeaderListeners = { closeClick };
        },
        detach(id) {
            const el = document.getElementById(id);
            const listeners = el?.__siemensIxModalHeaderListeners;
            if (!el || !listeners) {
                return;
            }

            el.removeEventListener('closeClick', listeners.closeClick);
            delete el.__siemensIxModalHeaderListeners;
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
