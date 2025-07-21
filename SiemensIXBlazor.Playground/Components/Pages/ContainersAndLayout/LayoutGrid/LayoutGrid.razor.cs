// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.ContainersAndLayout.LayoutGrid;

public partial class LayoutGrid
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
    <LayoutGrid>
      <Row>
        <Col><ix-typography format=""display"">1</ix-typography></Col>
        <Col><ix-typography format=""display"">2</ix-typography></Col>
        <Col><ix-typography format=""display"">3</ix-typography></Col>
        <Col><ix-typography format=""display"">4</ix-typography></Col>
        <Col><ix-typography format=""display"">5</ix-typography></Col>
        <Col><ix-typography format=""display"">6</ix-typography></Col>
      </Row>
    </LayoutGrid>";
}
