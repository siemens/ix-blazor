// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

const path = require("path");

const outputRoot = path.resolve(__dirname, "../wwwroot/js/siemens-ix");

module.exports = [
  {
    name: "siemens-ix",
    entry: "./src/index.js",
    output: {
      path: outputRoot,
      filename: "index.bundle.js",
      chunkFilename: "[name].[contenthash].index.bundle.js",
      publicPath: "auto",
      clean: {
        keep: /^(aggrid|interops)\//,
      },
    },
    performance: {
      hints: false,
    },
  },
  {
    name: "ag-grid",
    entry: "./src/ag-grid-interop.js",
    experiments: {
      outputModule: true,
    },
    output: {
      path: path.join(outputRoot, "aggrid"),
      filename: "ag-grid.bundle.js",
      chunkFilename: "[name].[contenthash].ag-grid.js",
      library: {
        type: "module",
      },
      module: true,
      clean: true,
      publicPath: "auto",
    },
    performance: {
      hints: false,
    },
  },
];
