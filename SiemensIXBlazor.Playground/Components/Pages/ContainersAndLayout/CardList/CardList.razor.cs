// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.ContainersAndLayout.CardList;

public partial class CardList
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
    <CardList Id=""carlist1"" Label=""Stack Layout"" ShowAllCount=""12"" ListStyle=""Enums.CardList.CardListStyle.Stack"" CollapseChangedEvent=""CardListCollapsedChanged""
    ShowAllClickEvent=""CardListShowAllClicked"" ShowMoreCardClickEvent=""CardListShowMoreClicked"">
        <PushCard Icon=""rocket""
                  Notification=""3""
                  Heading=""Heading content""
                  SubHeading=""Subheading""
                  Variant=""PushCardVariant.outline""></PushCard>
        <PushCard Icon=""bulb""
                  Notification=""1""
                  Heading=""Heading content""
                  SubHeading=""Subheading""
                  Variant=""PushCardVariant.warning""></PushCard>
        <PushCard Icon=""rocket""
                  Notification=""3""
                  Heading=""Heading content""
                  SubHeading=""Subheading""
                  Variant=""PushCardVariant.success""></PushCard>
    </CardList>";
}
