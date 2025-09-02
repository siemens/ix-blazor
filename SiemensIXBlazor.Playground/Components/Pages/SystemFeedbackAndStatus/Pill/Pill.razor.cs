// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.SystemFeedbackAndStatus.Pill;

public partial class Pill
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
         <SiemensIXBlazor.Components.Pill id=""basic"" Variant=""Enums.Pill.PillVariant.custom"" PillColor=""white"" Background=""purple"">
            Label
        </SiemensIXBlazor.Components.Pill>

        <SiemensIXBlazor.Components.Pill id=""basic-background-tooltip"" PillColor=""black"" Background=""blue"" TooltipText=""Custom tooltip text"">
            Label
        </SiemensIXBlazor.Components.Pill>

        <SiemensIXBlazor.Components.Pill id=""basic-tooltip"" PillColor=""white"" Background=""black"" Outline=""true"" TooltipText=""Label"">
            Label
        </SiemensIXBlazor.Components.Pill>

        <SiemensIXBlazor.Components.Pill id=""basic-styled"" PillColor=""white"" Background=""blue"" class=""styled"">
            Label
        </SiemensIXBlazor.Components.Pill>

        <SiemensIXBlazor.Components.Pill id=""basic-icon-label"" PillColor=""black"" Background=""blue"" icon=""star"">
            Label
        </SiemensIXBlazor.Components.Pill>

        <SiemensIXBlazor.Components.Pill id=""basic-icon"" PillColor=""black"" Background=""blue"" icon=""star"">
        </SiemensIXBlazor.Components.Pill>

        <SiemensIXBlazor.Components.Pill id=""basic-icon-label-styled"" PillColor=""black"" Background=""blue"" icon=""star"" style=""styled"">
            Label
        </SiemensIXBlazor.Components.Pill>

        <SiemensIXBlazor.Components.Pill id=""basic-icon-label-left"" PillColor=""white"" Outline=""true"" icon=""star"" style=""styled"" AlignLeft=""true"">
            Label
        </SiemensIXBlazor.Components.Pill>

        <SiemensIXBlazor.Components.Pill id=""alarm"" Variant=""Enums.Pill.PillVariant.alarm"" PillColor=""black"" Background=""red"">
            Label
        </SiemensIXBlazor.Components.Pill>

        <SiemensIXBlazor.Components.Pill id=""alarm-no-background"" Variant=""Enums.Pill.PillVariant.alarm"" PillColor=""white"" Outline=""true"">
            Label
        </SiemensIXBlazor.Components.Pill>

        <SiemensIXBlazor.Components.Pill id=""alarm-style"" Variant=""Enums.Pill.PillVariant.alarm"" PillColor=""white"" style=""styled"">
            Label
        </SiemensIXBlazor.Components.Pill>

        <SiemensIXBlazor.Components.Pill id=""alarm-icon-label"" Variant=""Enums.Pill.PillVariant.alarm"" PillColor=""black"" icon=""star"">
            Label
        </SiemensIXBlazor.Components.Pill>

        <SiemensIXBlazor.Components.Pill id=""alarm-icon-label-styled"" Variant=""Enums.Pill.PillVariant.alarm"" PillColor=""black"" icon=""star"" style=""styled"">
            Label
        </SiemensIXBlazor.Components.Pill>

        <SiemensIXBlazor.Components.Pill id=""alarm-icon-label-left"" Variant=""Enums.Pill.PillVariant.alarm"" PillColor=""black"" Outline=""true"" icon=""star"" style=""styled"" AlignLeft=""true"">
            Label
        </SiemensIXBlazor.Components.Pill>

        <SiemensIXBlazor.Components.Pill id=""ellipsis4-icon"" style=""styled-ellipsis-4"" icon=""star"">
            Label
        </SiemensIXBlazor.Components.Pill>

        <SiemensIXBlazor.Components.Pill id=""ellipsis4-icon-no-background"" Outline=""true"" style=""styled-ellipsis-4"" icon=""star"">
            Label
        </SiemensIXBlazor.Components.Pill>

        <SiemensIXBlazor.Components.Pill id=""ellipsis3-icon"" style=""styled-ellipsis-3"">
            Label
        </SiemensIXBlazor.Components.Pill>

        <SiemensIXBlazor.Components.Pill id=""ellipsis3-icon-no-background"" Outline=""true"" style=""styled-ellipsis-3"">
            Label
        </SiemensIXBlazor.Components.Pill>";

    public string ContentForVariant { get; private set; } = @"
        <div class =""ix-layout-grid"">
            <div class=""ix-row"">
                <div class=""ix-col"">
                    <SiemensIXBlazor.Components.Pill id=""background-primary"" Variant=""Enums.Pill.PillVariant.info"" PillColor=""blue"" Icon=""info"">
                        Primary
                    </SiemensIXBlazor.Components.Pill>
                </div>
                <div class=""ix-col"">
                    <SiemensIXBlazor.Components.Pill id=""primary"" Variant=""Enums.Pill.PillVariant.info"" PillColor=""blue"" Icon=""info"" Outline=""true"">
                        Primary
                    </SiemensIXBlazor.Components.Pill>
                </div>
            </div>
        </div>
        <div class=""ix-layout-grid"">
            <div class=""ix-row"">
                <div class=""ix-col"">
                    <SiemensIXBlazor.Components.Pill id=""background-alarm"" Variant=""Enums.Pill.PillVariant.alarm"" Icon=""info"">
                        Alarm
                    </SiemensIXBlazor.Components.Pill>
                </div>
                <div class=""ix-col"">
                    <SiemensIXBlazor.Components.Pill id=""alarm"" Variant=""Enums.Pill.PillVariant.alarm"" Icon=""info"" Outline=""true"">
                        Alarm
                    </SiemensIXBlazor.Components.Pill>
                </div>
            </div>
        </div>
        <div class=""ix-layout-grid"">
            <div class=""ix-row"">
                <div class=""ix-col"">
                    <SiemensIXBlazor.Components.Pill id=""background-critical"" Variant=""Enums.Pill.PillVariant.critical"" Icon=""info"">
                        Critical
                    </SiemensIXBlazor.Components.Pill>
                </div>
                <div class=""ix-col"">
                    <SiemensIXBlazor.Components.Pill id=""critical"" Variant=""Enums.Pill.PillVariant.critical"" Icon=""info"" Outline=""true"">
                        Critical
                    </SiemensIXBlazor.Components.Pill>
                </div>
            </div>
        </div>
        <div class=""ix-layout-grid"">
            <div class=""ix-row"">
                <div class=""ix-col"">
                    <SiemensIXBlazor.Components.Pill id=""background-warning"" Variant=""Enums.Pill.PillVariant.warning"" Icon=""info"">
                        Warning
                    </SiemensIXBlazor.Components.Pill>
                </div>
                <div class=""ix-col"">
                    <SiemensIXBlazor.Components.Pill id=""warning"" Variant=""Enums.Pill.PillVariant.warning"" Icon=""info"" Outline=""true"">
                        Warning
                    </SiemensIXBlazor.Components.Pill>
                </div>
            </div>
        </div>
        <div class=""ix-layout-grid"">
            <div class=""ix-row"">
                <div class=""ix-col"">
                    <SiemensIXBlazor.Components.Pill id=""background-success"" Variant=""Enums.Pill.PillVariant.success"" Icon=""info"">
                        Success
                    </SiemensIXBlazor.Components.Pill>
                </div>
                <div class=""ix-col"">
                    <SiemensIXBlazor.Components.Pill id=""success"" Variant=""Enums.Pill.PillVariant.success"" Icon=""info"" Outline=""true"">
                        Success
                    </SiemensIXBlazor.Components.Pill>
                </div>
            </div>
        </div>
        <div class=""ix-layout-grid"">
            <div class=""ix-row"">
                <div class=""ix-col"">
                    <SiemensIXBlazor.Components.Pill id=""background-info"" Variant=""Enums.Pill.PillVariant.info"" Icon=""info"">
                        Info
                    </SiemensIXBlazor.Components.Pill>
                </div>
                <div class=""ix-col"">
                    <SiemensIXBlazor.Components.Pill id=""info"" Variant=""Enums.Pill.PillVariant.info"" Icon=""info"" Outline=""true"">
                        Info
                    </SiemensIXBlazor.Components.Pill>
                </div>
            </div>
        </div>
        <div class=""ix-layout-grid"">
            <div class=""ix-row"">
                <div class=""ix-col"">
                    <SiemensIXBlazor.Components.Pill id=""background-neutral"" Variant=""Enums.Pill.PillVariant.neutral"" Icon=""info"">
                        Neutral
                    </SiemensIXBlazor.Components.Pill>
                </div>
                <div class=""ix-col"">
                    <SiemensIXBlazor.Components.Pill id=""neutral"" Variant=""Enums.Pill.PillVariant.neutral"" Icon=""info"" Outline=""true"">
                        Neutral
                    </SiemensIXBlazor.Components.Pill>
                </div>
            </div>
        </div>
        <div class=""ix-layout-grid"">
            <div class=""ix-row"">
                <div class=""ix-col"">
                    <SiemensIXBlazor.Components.Pill id=""background-custom"" Variant=""Enums.Pill.PillVariant.custom"" PillColor=""color-soft-text"" Background=""purple"" Icon=""info"">
                        Custom
                    </SiemensIXBlazor.Components.Pill>
                </div>
                <div class=""ix-col"">
                    <SiemensIXBlazor.Components.Pill id=""custom"" Variant=""Enums.Pill.PillVariant.custom"" PillColor=""color-soft-text"" Background=""purple"" Icon=""info"" Outline=""true"">
                        Custom
                    </SiemensIXBlazor.Components.Pill>
                </div>
            </div>
        </div>";

}
