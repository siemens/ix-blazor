// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

namespace SiemensIXBlazor.Playground.Components.Pages.SystemFeedbackAndStatus.Chip;

public partial class Chip
{
    private int activeTab = 0;

    public string ContentForBasic { get; private set; } = @"
        <div class=""chip"">
            <div class=""chip-column"">
                <SiemensIXBlazor.Components.Chip Id=""primary-outline"" icon=""print"" Variant=""Enums.Chip.ChipVariant.primary"" Outline=""true"" Closable=""true"">
                    Primary
                </SiemensIXBlazor.Components.Chip>

                <SiemensIXBlazor.Components.Chip Id=""alarm-outline"" icon=""print"" Variant=""Enums.Chip.ChipVariant.alarm"" Outline=""true"" Closable=""true"">
                    Alarm
                </SiemensIXBlazor.Components.Chip>

                <SiemensIXBlazor.Components.Chip Id=""critical-outline"" icon=""print"" Variant=""Enums.Chip.ChipVariant.critical"" Outline=""true"">
                    Critical
                </SiemensIXBlazor.Components.Chip>

                <SiemensIXBlazor.Components.Chip Id=""warning-outline"" icon=""print"" Variant=""Enums.Chip.ChipVariant.warning"" Outline=""true"">
                    Warning
                </SiemensIXBlazor.Components.Chip>

                <SiemensIXBlazor.Components.Chip Id=""info-outline"" icon=""print"" Variant=""Enums.Chip.ChipVariant.info"" Outline=""true"">
                    Info
                </SiemensIXBlazor.Components.Chip>

                <SiemensIXBlazor.Components.Chip Id=""success-outline"" icon=""print"" Variant=""Enums.Chip.ChipVariant.success"" Outline=""true"">
                    Success
                </SiemensIXBlazor.Components.Chip>

                <SiemensIXBlazor.Components.Chip Id=""neutral-outline"" icon=""print"" Variant=""Enums.Chip.ChipVariant.neutral"" Outline=""true"">
                    Neutral
                </SiemensIXBlazor.Components.Chip>

                <SiemensIXBlazor.Components.Chip Id=""custom-outline"" icon=""print"" Variant=""Enums.Chip.ChipVariant.custom"" Background=""#FF00FF"" Outline=""true"" Closable=""true"">
                    Custom
                </SiemensIXBlazor.Components.Chip>

                <SiemensIXBlazor.Components.Chip Id=""chip-with-icon-outline"" icon=""print"" Variant=""Enums.Chip.ChipVariant.primary"" Outline=""true"" Closable=""true"">
                    Chip with icon
                </SiemensIXBlazor.Components.Chip>
            </div>

            <div class=""chip-column"">
                <SiemensIXBlazor.Components.Chip Id=""primary"" icon=""print"" Variant=""Enums.Chip.ChipVariant.primary"" Closable=""true"">
                    Primary
                </SiemensIXBlazor.Components.Chip>

                <SiemensIXBlazor.Components.Chip Id=""alarm"" icon=""print"" Variant=""Enums.Chip.ChipVariant.alarm"" Closable=""true"">
                    Alarm
                </SiemensIXBlazor.Components.Chip>

                <SiemensIXBlazor.Components.Chip Id=""critical"" icon=""print"" Variant=""Enums.Chip.ChipVariant.critical"">
                    Critical
                </SiemensIXBlazor.Components.Chip>

                <SiemensIXBlazor.Components.Chip Id=""warning"" icon=""print"" Variant=""Enums.Chip.ChipVariant.warning"">
                    Warning
                </SiemensIXBlazor.Components.Chip>

                <SiemensIXBlazor.Components.Chip Id=""info"" icon=""print"" Variant=""Enums.Chip.ChipVariant.info"">
                    Info
                </SiemensIXBlazor.Components.Chip>

                <SiemensIXBlazor.Components.Chip Id=""success"" icon=""print"" Variant=""Enums.Chip.ChipVariant.success"">
                    Success
                </SiemensIXBlazor.Components.Chip>

                <SiemensIXBlazor.Components.Chip Id=""neutral"" icon=""print"" Variant=""Enums.Chip.ChipVariant.neutral"">
                    Neutral
                </SiemensIXBlazor.Components.Chip>

                <SiemensIXBlazor.Components.Chip Id=""custom"" Variant=""Enums.Chip.ChipVariant.custom"" Background=""#FF00FF"" ChipColor=""black"" Closable=""true"">
                    Custom
                </SiemensIXBlazor.Components.Chip>

                <SiemensIXBlazor.Components.Chip Id=""chip-without-icon-primary"" Variant=""Enums.Chip.ChipVariant.primary"" Closable=""true"" Outline=""true"">
                    Chip without icon
                </SiemensIXBlazor.Components.Chip>
            </div>
        </div>";
}
