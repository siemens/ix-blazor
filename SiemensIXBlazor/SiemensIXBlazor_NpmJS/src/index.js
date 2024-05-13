// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------
import { defineCustomElements, applyPolyfills } from '@siemens/ix/loader/index'
import { toast } from "@siemens/ix"
import '@siemens/ix-echarts';
import { registerTheme } from '@siemens/ix-echarts';
import * as echarts from 'echarts';
import { themeSwitcher } from '@siemens/ix';
import { Grid } from 'ag-grid-community';
import { defineCustomElements as ixIconsDefineCustomElements } from '@siemens/ix-icons/loader';

(async () => {
    await applyPolyfills();
    await ixIconsDefineCustomElements();
    await defineCustomElements();
})();

// toast
window.showMessage = (config) => {
    const toastConfig = JSON.parse(config);
    toast(toastConfig);
}
// chart
window.initializeChart = (id, options) => {
    registerTheme(echarts);
    var myChart = echarts.init(
        document.getElementById(id),
        window.demoTheme // brand-dark, brand-light, classic-dark or classic-light
    );
    // Draw the chart
    myChart.setOption(JSON.parse(options));
}
// set theme
window.setTheme = (theme) => {
    themeSwitcher.setTheme(theme);
}
// toggle theme
window.toggleTheme = () => {
    themeSwitcher.toggleMode();
}
// toggle system theme
window.toggleSystemTheme = (useSystemTheme) => {
    if (useSystemTheme === true) {
        themeSwitcher.setVariant();
    }
}

// AGGrid
window.agGridInterop = {
    dotnetReference: null,
    createGrid: function (dotnetRef, elementId, gridOptions) {
        let parsedOption = JSON.parse(gridOptions);

        window.agGridInterop.dotnetReference = dotnetRef;
        parsedOption.onCellClicked = function (event) {
            dotnetRef.invokeMethodAsync('OnCellClickedCallback');
        };

        return new Grid(document.getElementById(elementId), parsedOption);
    },
    setData: function (grid, data) {
        grid.gridOptions.api.setRowData(data);
    },
    getSelectedRows: function (grid) {
        return grid.gridOptions.api.getSelectedRows();
    },
}
