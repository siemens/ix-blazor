export function listenClosedEvent(caller, id) {
    const chipElement = document.getElementById(id);
    chipElement.addEventListener('close', () => {
        caller.invokeMethodAsync("Closed");
    })
}