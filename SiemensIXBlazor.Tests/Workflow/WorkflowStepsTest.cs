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
using Xunit;

namespace SiemensIXBlazor.Tests.Workflow
{
    public class WorkflowStepsTests : TestContextBase
    {
        [Fact]
        public void WorkflowStepsRendersCorrectly()
        {
            // Arrange
            var cut = Render<WorkflowSteps>(parameters => parameters
                .Add(p => p.Id, "testId")
                .Add(p => p.Clickable, true)
                .Add(p => p.SelectedIndex, 1)
                .Add(p => p.Vertical, true)
            );

            // Assert
            cut.MarkupMatches("<ix-workflow-steps id=\"testId\" clickable='true' selected-index=\"1\" vertical='true'></ix-workflow-steps>");
        }

        [Fact]
        public async Task StepSelectedEventWorks()
        {
            // Arrange
            var stepSelected = false;
            var cut = Render<WorkflowSteps>(parameters => parameters
                .Add(p => p.Id, "workflowSteps")
                .Add(p => p.StepSelectedEvent, EventCallback.Factory.Create<int>(this, newValue => { stepSelected = true; }))
            );

            // Act
            await cut.Instance.StepSelectedEvent.InvokeAsync(1);

            // Assert
            Assert.True(stepSelected);
        }
    }
}
