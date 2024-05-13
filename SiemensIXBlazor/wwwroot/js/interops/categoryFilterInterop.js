// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

export function setCategories(id, categories) {
    try {
        const element = document.getElementById(id);
        element.categories = JSON.parse(categories);
    }
    catch {

    }
    
}

export function setFilterState(id, filterState) {
    try {
        const element = document.getElementById(id);
        element.filterState = JSON.parse(filterState);
    }
    catch {

    }
    
}

export function setNonSelectableCategories(id, nonSelectableCategories) {
    try {
        const element = document.getElementById(id);
        element.nonSelectableCategories = JSON.parse(nonSelectableCategories);
    }
    catch {

    }
    
}