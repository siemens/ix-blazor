export function setDateRangeOptions(id, dateRangeOptions) {
    try {
        const element = document.getElementById(id);
        element.dateRangeOptions = JSON.parse(dateRangeOptions);
    }
    catch (err) {
        console.error(err)
    }
}