// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

import { defineCustomElements } from "@siemens/ix/loader";
import "@siemens/ix-echarts";
import { registerTheme } from "@siemens/ix-echarts";
import * as echarts from "echarts";
import { showModalLoading, themeSwitcher } from "@siemens/ix";
import { defineCustomElements as ixIconsDefineCustomElements } from "@siemens/ix-icons/loader";

window.echarts = echarts;
const charts = new Map();

function getElement(id) {
    return document.getElementById(id);
}

function getElementOrThrow(id) {
    const element = getElement(id);
    if (!element) {
        throw new Error(`Element with id '${id}' not found`);
    }
    return element;
}

function disposeChart(id) {
    const chartState = charts.get(id);
    if (!chartState) {
        return;
    }

    chartState.themeDisposer?.dispose();
    window.removeEventListener("resize", chartState.resizeListener);
    if (!chartState.chart.isDisposed()) {
        chartState.chart.dispose();
    }
    charts.delete(id);
}

window.siemensIXInterop = {
    async initialize() {
        await ixIconsDefineCustomElements(window, {
            resourcesUrl: "./_content/Siemens.IX.Blazor/"
        });

        await defineCustomElements();
    },
    modal: {
        async show(id) {
            const el = getElement(id);
            if (el) {
                await el.showModal();
            } else {
                console.error(`[siemensIXInterop.modal.show] Element with id '${id}' not found.`);
            }
        },
        async close(id, reason) {
            const el = getElement(id);
            if (el) {
                await el.closeModal(reason);
            } else {
                console.error(`[siemensIXInterop.modal.close] Element with id '${id}' not found.`);
            }
        },
        async dismiss(id, reason) {
            const el = getElement(id);
            if (el) {
                await el.dismissModal(reason);
            } else {
                console.error(`[siemensIXInterop.modal.dismiss] Element with id '${id}' not found.`);
            }
        },
        attach(id, dotnetReference) {
            const el = getElementOrThrow(id);

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
            const el = getElement(id);
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
            const el = getElementOrThrow(id);

            this.detach(id);
            const closeClick = (event) =>
                dotnetReference.invokeMethodAsync('CloseClick', event.detail);
            el.addEventListener('closeClick', closeClick);
            el.__siemensIxModalHeaderListeners = { closeClick };
        },
        detach(id) {
            const el = getElement(id);
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
            disposeChart(id);
            const element = getElementOrThrow(id);

            registerTheme(echarts);

            const parsedOptions = JSON.parse(options);

            if (parsedOptions.series) {
                parsedOptions.series.forEach(series => {
                    if (series.equation && series.equation.z && typeof series.equation.z === 'string') {
                        series.equation.z = eval('(' + series.equation.z + ')');
                    }
                });
            }

            const chartState = {
                chart: echarts.init(element, themeSwitcher.getCurrentTheme()),
                themeDisposer: null,
                resizeListener: null,
            };
            chartState.chart.setOption(parsedOptions);

            chartState.themeDisposer = themeSwitcher.themeChanged.on((theme) => {
                if (chartState.chart.isDisposed()) {
                    return;
                }
                chartState.chart.dispose();
                chartState.chart = echarts.init(element, theme);
                chartState.chart.setOption(parsedOptions);
            });

            chartState.resizeListener = () => {
                if (!chartState.chart.isDisposed()) {
                    chartState.chart.resize();
                }
            };
            window.addEventListener("resize", chartState.resizeListener);
            charts.set(id, chartState);
        } catch (error) {
            console.error("Failed to initialize chart:", error);
        }
    },
    disposeChart,

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

};

(async () => {
    await siemensIXInterop.initialize();
})();
