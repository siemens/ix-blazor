export function setTreeModel(id, treeModel) {
    try {
        const element = document.getElementById(id);
        element.model = JSON.parse(treeModel);
        console.log(element, JSON.parse(treeModel));
    }
    catch {

    }
}

export function setTreeContext(id, treeContext) {
    try {
        const element = document.getElementById(id);
        element.context = JSON.parse(treeContext);
    }
    catch {

    }
}