export function listenItemClickedEvent(caller, id) {
    const breadcrumbElement = document.getElementById(id);
    console.log(id, breadcrumbElement)
    breadcrumbElement.addEventListener('itemClick', (e) => {
        console.log(e);
        caller.invokeMethodAsync("BreadcrumbItemClicked", e.detail);
    })
}