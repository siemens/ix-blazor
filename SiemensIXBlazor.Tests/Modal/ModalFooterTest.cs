// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2026 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using Microsoft.Extensions.DependencyInjection;
using SiemensIXBlazor.Components.Modal;
using SiemensIXBlazor.Tests;
using Xunit;

namespace SiemensIXBlazor.Tests.Modal
{
    public class ModalFooterTest : TestContextBase
    {
        [Fact]
        public void ModalFooter_ShouldRenderBasicProperties()
        {
            // Arrange & Act
            var component = RenderComponent<ModalFooter>(parameters => parameters
                .Add(p => p.Class, "custom-class")
                .Add(p => p.Style, "color: red;")
            );

            // Assert
            var element = component.Find("ix-modal-footer");
            Assert.NotNull(element);
            Assert.Contains("custom-class", element.GetAttribute("class") ?? "");
            Assert.Equal("color: red;", element.GetAttribute("style"));
        }

        [Fact]
        public void ModalFooter_ShouldRenderChildContent()
        {
            // Arrange & Act
            var component = RenderComponent<ModalFooter>(parameters => parameters
                .AddChildContent("<button>Close</button><button>Save</button>")
            );

            // Assert
            var element = component.Find("ix-modal-footer");
            Assert.Contains("Close", element.InnerHtml);
            Assert.Contains("Save", element.InnerHtml);
        }

        [Fact]
        public void ModalFooter_ShouldRenderActionButtons()
        {
            // Arrange & Act
            var component = RenderComponent<ModalFooter>(parameters => parameters
                .AddChildContent(
                    @"<ix-button>Cancel</ix-button>
                      <ix-button variant=""primary"">Confirm</ix-button>"
                )
            );

            // Assert
            var element = component.Find("ix-modal-footer");
            Assert.Contains("Cancel", element.InnerHtml);
            Assert.Contains("Confirm", element.InnerHtml);
            Assert.Contains("ix-button", element.InnerHtml);
        }

        [Fact]
        public void ModalFooter_ShouldRenderWithoutProperties()
        {
            // Arrange & Act
            var component = RenderComponent<ModalFooter>();

            // Assert
            var element = component.Find("ix-modal-footer");
            Assert.NotNull(element);
        }

        [Fact]
        public void ModalFooter_ShouldApplyCssClass()
        {
            // Arrange & Act
            var component = RenderComponent<ModalFooter>(parameters => parameters
                .Add(p => p.Class, "test-class footer-class")
            );

            // Assert
            var element = component.Find("ix-modal-footer");
            Assert.Contains("test-class", element.GetAttribute("class") ?? "");
            Assert.Contains("footer-class", element.GetAttribute("class") ?? "");
        }

        [Fact]
        public void ModalFooter_ShouldApplyStyleAttribute()
        {
            // Arrange & Act
            var component = RenderComponent<ModalFooter>(parameters => parameters
                .Add(p => p.Style, "text-align: right; padding: 20px;")
            );

            // Assert
            var element = component.Find("ix-modal-footer");
            Assert.Equal("text-align: right; padding: 20px;", element.GetAttribute("style"));
        }
    }
}
