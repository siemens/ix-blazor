﻿// -----------------------------------------------------------------------
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
using SiemensIXBlazor.Components.Avatar;

namespace SiemensIXBlazor.Tests
{
    public class ContentHeaderTests: TestContextBase
    {
        [Fact]
        public void ContentHeaderRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<ContentHeader>(parameters => {
                parameters.Add(p => p.Id, "testId");
                parameters.Add(p => p.HasBackButton, true);
                parameters.Add(p => p.HeaderSubTitle, "testHeaderSubTitle");
                parameters.Add(p => p.HeaderTitle, "testHeaderTitle");
                parameters.Add(p => p.Variant, Enums.ContentHeader.ContentHeaderVariant.primary);
            });

            // Assert
            cut.MarkupMatches("<ix-content-header id=\"testId\" has-back-button=\"\" header-title=\"testHeaderTitle\" header-subtitle=\"testHeaderSubTitle\" variant=\"primary\"></ix-content-header>");
        }

        [Fact]
        public void ContentHeaderRendersChildContent()
        {
            // Arrange
            var expectedContent = "Expected content";

            // Act
            var cut = RenderComponent<ContentHeader>(parameters => parameters
                .Add(p => p.ChildContent, builder =>
                {
                    builder.AddContent(0, expectedContent);
                })
                .Add(p => p.Id, "testId"));

            // Assert
            Assert.Contains(expectedContent, cut.Markup);
        }

        [Fact]
        public void BackButtonClickedEventTriggeredCorrectly()
        {
            // Arrange
            var eventTriggered = false;
            var cut = RenderComponent<ContentHeader>(parameters => parameters.Add(p => p.BackButtonClickedEvent, EventCallback.Factory.Create(this, () => eventTriggered = true))
            .Add(p => p.Id, "testId"));

            // Act
            cut.Instance.BackButtonClickedEvent.InvokeAsync(true);

            // Assert
            Assert.True(eventTriggered);
        }

    }
}
