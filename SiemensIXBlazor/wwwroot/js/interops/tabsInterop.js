// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

export const initalTable = async (id) => {
    await window.customElements.whenDefined('ix-tabs');
    const tabsElement = document.getElementById(id);
    const tabs = tabsElement.querySelectorAll('ix-tab-item[data-tab-id]');

    function registerClickListener(tab) {
        tab.addEventListener('click', (tabContent) => {
            const contentList = tabsElement.parentElement.querySelectorAll('[data-tab-content]');
            contentList.forEach((content) => {
                if (content.dataset.tabContent === tab.dataset.tabId) {
                    content.classList.add('show');
                } else {
                    content.classList.remove('show');
                }
            });
        });
    }

    tabs.forEach(registerClickListener);
};

export const subscribeEvents = (caller, id, eventName, functionName) => {
    const element = document.getElementById(id);
    element.addEventListener(eventName, (e) => {
        caller.invokeMethodAsync(functionName, e.detail);
    })
}