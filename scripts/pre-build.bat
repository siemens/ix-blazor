@REM -----------------------------------------------------------------------
@REM SPDX-FileCopyrightText: 2026 Siemens AG
@REM
@REM SPDX-License-Identifier: MIT
@REM
@REM This source code is licensed under the MIT license found in the
@REM LICENSE file in the root directory of this source tree.
@REM  -----------------------------------------------------------------------

cd SiemensIXBlazor_NpmJS
npm ci && npm run check && npm test && npm run build
