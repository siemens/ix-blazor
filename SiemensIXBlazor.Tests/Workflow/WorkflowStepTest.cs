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
            var cut = Render<WorkflowStep>(parameters => parameters
                .Add(p => p.Clickable, true)
                .Add(p => p.Disabled, true)
                .Add(p => p.Position, WorkflowPosition.First)
                .Add(p => p.Selected, true)
                .Add(p => p.Status, WorkflowStatus.Open)
                .Add(p => p.Vertical, true)
            );

            // Assert
            cut.MarkupMatches("<ix-workflow-step clickable='true' disabled='true' position=\"first\" selected='true' status=\"open\" vertical='true'></ix-workflow-step>");
        }

        [Fact]
        public void CustomIconRendersInCustomIconSlot()
        {
            var cut = Render<WorkflowStep>(parameters => parameters
                .Add(p => p.CustomIcon, (RenderFragment)(builder => builder.AddMarkupContent(0, "<ix-icon name=\"star\"></ix-icon>")))
                .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddContent(0, "Step"))));

            var iconSlot = cut.Find("[slot='custom-icon']");

            Assert.Contains("star", iconSlot.InnerHtml);
            Assert.Contains("Step", cut.Markup);
        }
    }
}
