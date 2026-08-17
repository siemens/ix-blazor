#!/bin/bash
# -----------------------------------------------------------------------
# SPDX-FileCopyrightText: 2026 Siemens AG
#
# SPDX-License-Identifier: MIT
#
# This source code is licensed under the MIT license found in the
# LICENSE file in the root directory of this source tree.
#  -----------------------------------------------------------------------

# Navigate to the SiemensIXBlazor_NpmJS directory
cd SiemensIXBlazor/SiemensIXBlazor_NpmJS

# Install and validate the exact locked dependency graph
npm ci
npm run check
npm test

# Run npm build
npm run build
