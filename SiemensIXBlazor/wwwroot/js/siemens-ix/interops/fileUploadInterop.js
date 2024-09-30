// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

export function fileUploadEventHandler(
  caller,
  elementId,
  eventName,
  functionName
) {
  const element = document.getElementById(elementId);

  if (!element) {
    console.error(`Element with id ${elementId} not found.`);
    return;
  }

  element.addEventListener(eventName, (e) => {
    const files = e.detail;
    if (!files || files.length === 0) return; // Early exit if no files selected

    const filePromises = Array.from(files).map((file) => {
      return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onloadend = () => {
          try {
            const base64Data = reader.result.split(",")[1];
            resolve({
              name: file.name,
              size: file.size,
              type: file.type,
              data: base64Data,
            });
          } catch (error) {
            reject(error);
          }
        };
        reader.onerror = (error) => {
          console.error(`Error reading file ${file.name}:`, error);
          reject(error);
        };
        reader.readAsDataURL(file);
      });
    });

    Promise.all(filePromises)
      .then((fileDataArray) => {
        caller.invokeMethodAsync(functionName, fileDataArray);
      })
      .catch((error) => {
        console.error("Error processing files:", error);
      });
  });
}
