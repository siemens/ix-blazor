// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

import { getElementOrThrow } from "./elementUtils.js";

export const setApplicationConfig = (id, config) => {
  const element = getElementOrThrow(id);

  try {
    element.appSwitchConfig = JSON.parse(config);
  } catch (error) {
    console.error("Failed to set application config:", error);
  }
};

export const setBreakpoints = (id, breakpoints) => {
    const element = getElementOrThrow(id);

    element.breakpoints = breakpoints;
};
