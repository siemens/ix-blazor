export const setApplicationConfig = (id, config) => {
    const element = document.getElementById(id);
    element.appSwitchConfig = JSON.parse(config);
}