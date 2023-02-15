export function listenCollapsedEvent(caller, id) {
    const blindElement = document.getElementById(id);
    console.log("Id: " + id);
    console.log(blindElement)
    blindElement.addEventListener('collapsedChange', (e) => {
        console.log(e);
        caller.invokeMethodAsync("CollapsedChanged", e.detail);
        
    })
}