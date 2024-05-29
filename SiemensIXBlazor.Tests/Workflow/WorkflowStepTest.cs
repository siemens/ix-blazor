// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using Microsoft.AspNetCore.Components;
using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.Workflow;
using Xunit;

namespace SiemensIXBlazor.Tests.Workflow
{
    public class WorkflowStepTests : TestContextBase
    {
        [Fact]
        public void WorkflowStepRendersCorrectly()
        {
            // Arrange
            var cut = RenderComponent<WorkflowStep>(
                ("Clickable", true),
                ("Disabled", true),
                ("Position", WorkflowPosition.First),
                ("Selected", true),
                ("Status", WorkflowStatus.Open),
                ("Vertical", true)
            );

            // Assert
            cut.MarkupMatches("<ix-workflow-step clickable disabled position=\"first\" selected status=\"open\" vertical></ix-workflow-step>");
        }
    }
}