// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.ContainersAndLayout.EventList;

public partial class EventList
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
    <EventList>
        <EventListItem Id=""event-list-item-1"" Label=""Item 1"" ItemCLickEvent=""(label) => DropdownButtonClicked(label)""></EventListItem>
        <EventListItem Id=""event-list-item-2"" Label=""Item 2"" ItemCLickEvent=""(label) => DropdownButtonClicked(label)""></EventListItem>
        <EventListItem Id=""event-list-item-3"" Label=""Item 3"" ItemCLickEvent=""(label) => DropdownButtonClicked(label)""></EventListItem>
    </EventList>";
}
