import { defineCustomElements } from '@siemens/ix/loader/index'
import { toast } from "@siemens/ix"

defineCustomElements();

// toast
window.showMessage = (config) => {
    const toastConfig = JSON.parse(config);
    toast(toastConfig);
}
