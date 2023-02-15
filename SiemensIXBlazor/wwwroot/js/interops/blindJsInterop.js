export function listenCollapsedEvent(caller, id) {
    const blindElement = document.getElementById(id);
    blindElement.addEventListener('collapsedChange', (e) => {
        caller.invokeMethodAsync("CollapsedChanged", e.detail);
    })
}