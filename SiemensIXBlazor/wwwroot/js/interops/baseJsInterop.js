export function listenEvent(caller, elementId, eventName, funtionName) {
    const element = document.getElementById(elementId);
    element.addEventListener(eventName, (e) => {
        caller.invokeMethodAsync(funtionName, e.detail);
    })
}