// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.ApplicationFrame.Content;

public partial class Content
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
        <SiemensIXBlazor.Components.Content Id=""basic-content"">
            <SiemensIXBlazor.Components.ContentHeader Id=""basic-content-header"" slot=""header"" HeaderTitle=""My Content Page""></SiemensIXBlazor.Components.ContentHeader>
            Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy
            eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam
            voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet
            clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit
            amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam
            nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed
            diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum.
            Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor
            sit amet.
        </SiemensIXBlazor.Components.Content>";
}
