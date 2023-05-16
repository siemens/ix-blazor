export function fileUploadEventHandler(caller, elementId, eventName, funtionName) {
    const element = document.getElementById(elementId);
    element.addEventListener(eventName, (e) => {
        const files = e.detail;

        const fileDataArray = [];
        for (let i = 0; i < files.length; i++) {
            const file = files[i];
            const reader = new FileReader();
            reader.onloadend = function () {
                const base64Data = reader.result.split(',')[1];
                fileDataArray.push({ name: file.name, size: file.size, type: file.type, data: base64Data });

                if (fileDataArray.length === files.length) {
                    caller.invokeMethodAsync(funtionName, fileDataArray);
                }
            };
            reader.readAsDataURL(file);
        }
    })
}