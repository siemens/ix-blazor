import { defineCustomElements } from '@siemens/ix/loader/index'
import { toast } from "@siemens/ix"

defineCustomElements();

// toast
window.showMessage = (message, type) => {
    toast({
        message: message,
        type: type
    });
}
