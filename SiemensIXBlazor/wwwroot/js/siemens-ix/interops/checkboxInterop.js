export function setChecked(elementId, value) {
    const element = document.getElementById(elementId);
    if (element) {
        element.checked = JSON.parse(value);
    }
}

export function setIndeterminate(elementId, value) {
    const element = document.getElementById(elementId);
    if (element) {
        element.indeterminate = JSON.parse(value);
    }
}

export function setValue(elementId, value) {
    const element = document.getElementById(elementId);
    if (element) {
        element.value = JSON.parse(value);
    }
}