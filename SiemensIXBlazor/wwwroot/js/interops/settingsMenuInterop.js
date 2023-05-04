export async function toggleSettings(id, status) {
    try {
        const element = document.getElementById(id);
        await element.toggleSettings(status)
    }
    catch {

    }

}