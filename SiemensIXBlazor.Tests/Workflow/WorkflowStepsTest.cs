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
            var cut = RenderComponent<WorkflowSteps>(
                ("Id", "testId"),
                ("Clickable", true),
                ("Linear", true),
                ("SelectedIndex", 1),
                ("Vertical", true)
            );

            // Assert
            cut.MarkupMatches("<ix-workflow-steps id=\"testId\" clickable linear selected-index=\"1\" vertical></ix-workflow-steps>");
        }

        [Fact]
        public async Task StepSelectedEventWorks()
        {
            // Arrange
            var stepSelected = false;
            var cut = RenderComponent<WorkflowSteps>(
                ("Id", "workflowSteps"),
                ("StepSelectedEvent", EventCallback.Factory.Create<int>(this, newValue => { stepSelected = true; }))
            );

            // Act
            await cut.Instance.StepSelectedEvent.InvokeAsync(1);

            // Assert
            Assert.True(stepSelected);
        }
    }
}