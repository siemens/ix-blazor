export function fileUploadEventHandler(caller, elementId, eventName, funtionName) {
    const element = document.getElementById(elementId);
    element.addEventListener(eventName, (e) => {
        e.detail[0].arrayBuffer().then(data => {
            caller.invokeMethodAsync(funtionName, new Uint8Array(data));
        })
    })
}