// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.ContainersAndLayout.Panes;

public partial class Panes
{
    private int activeTab = 0;

    public string ContentForPaneLayout { get; private set; } = @"
    <PaneLayout Id=""pane-layout""
                    Variant=""@PaneVariant.floating""
                    Layout=""full-horizontal""
                    Borderless=""true"">
        <Pane Id=""pane1"" Heading=""Pane Left"" Slot=""left"" Size=""33%"">
            <p>This is the left pane.</p>
        </Pane>
    
        <Pane Id=""pane2""  Heading=""Pane Top"" Slot=""top"" Size=""33%"">
            <p>This is the top pane.</p>
        </Pane>
    
        <Pane Id=""pane3"" Heading=""Pane Right"" Slot=""right"" Size=""33%"">
            <p>This is the right pane.</p>
        </Pane>
    
        <Pane Id=""pane4"" Heading=""Pane Bottom"" Slot=""bottom"" Size=""33%"">
            <p>This is the bottom pane.</p>
        <Pane>
    </PaneLayout>";
}
