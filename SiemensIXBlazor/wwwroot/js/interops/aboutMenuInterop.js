export async function toggleAbout(id, status) {
    try {
        const element = document.getElementById(id);
        await element.toggleAbout(status)
    }
    catch {

    }

}