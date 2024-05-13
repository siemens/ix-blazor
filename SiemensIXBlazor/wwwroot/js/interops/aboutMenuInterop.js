// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------
export async function toggleAbout(id, status) {
    try {
        const element = document.getElementById(id);
        await element.toggleAbout(status)
    }
    catch {

    }

}