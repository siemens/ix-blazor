import { defineCustomElements } from '@siemens/ix/loader/index'
import { toast } from "@siemens/ix"
import '@siemens/ix-echarts';
import { registerTheme } from '@siemens/ix-echarts';
import * as echarts from 'echarts';

defineCustomElements();

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
