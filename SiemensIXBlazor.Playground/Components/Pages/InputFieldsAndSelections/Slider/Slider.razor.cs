// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.InputFieldsAndSelections.Slider;

public partial class Slider
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
    <Slider Id=""slider-demo"" Value=""0"">
    </Slider>
    <Slider Id=""slider-demo"" Min=""0"" Max=""50"" Step=""5"" Value=""0"">
    </Slider>";
    public string ContentForMarker { get; private set; } = @"
    <Slider Id=""slider-demo"" Value=""0"" Marker=""[0, 25, 50, 75, 100]"">
    </Slider>
    <Slider Id=""slider-demo"" Min=""0"" Max=""50"" Step=""5"" Value=""0"" Marker=""[0, 25, 50, 75, 100]"">
    </Slider>";
    public string ContentForTrace { get; private set; } = @"
    <Slider Id=""slider-demo"" Trace Value=""0"" Marker=""[0, 25, 50, 75, 100]"">
    </Slider>
    <Slider Id=""slider-demo"" Trace Min=""0"" Max=""50"" Step=""5"" Value=""0"" Marker=""[0, 25, 50, 75, 100]"">
    </Slider>";
}
