// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2025 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
// -----------------------------------------------------------------------

using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using SiemensIXBlazor.Components.Slider;

namespace SiemensIXBlazor.Tests;

public class SliderTests : TestContextBase
{
    [Fact]
    public void Slider_RenderedWithCorrectAttributes()
    {
        // Arrange
        var id = "slider1";
        var helperText = "Select a value";
        var label = "Slider label";
        var invalidText = "Invalid value";
        var infoText = "Additional information";
        var warningText = "Warning value";
        var validText = "Valid value";

        var cut = Render<Slider>(parameters => parameters
            .Add(p => p.Id, id)
            .Add(p => p.Value, 42)
            .Add(p => p.HelperText, helperText)
            .Add(p => p.Label, label)
            .Add(p => p.InvalidText, invalidText)
            .Add(p => p.InfoText, infoText)
            .Add(p => p.WarningText, warningText)
            .Add(p => p.ValidText, validText)
            .Add(p => p.ShowTextAsTooltip, true)
            .Add(p => p.Max, 100)
            .Add(p => p.Min, 0)
            .Add(p => p.Step, 1)
            .Add(p => p.Disabled, true)
            .Add(p => p.Trace, true)
            .Add(p => p.TraceReference, 5)
        );

        cut.MarkupMatches(@$"
            <ix-slider 
                id=""{id}"" 
                value=""42"" 
                disabled 
                helper-text=""{helperText}""
                label=""{label}""
                invalid-text=""{invalidText}""
                info-text=""{infoText}""
                warning-text=""{warningText}""
                valid-text=""{validText}""
                show-text-as-tooltip
                max=""100"" 
                min=""0"" 
                step=""1"" 
                trace 
                trace-reference=""5"">
            </ix-slider>");
    }

    [Fact]
    public void Slider_RendersTypedLabelSlots()
    {
        var cut = Render<Slider>(parameters => parameters
            .Add(p => p.Id, "slider-slots")
            .Add(p => p.LabelStart, (RenderFragment)(builder => builder.AddContent(0, "Minimum")))
            .Add(p => p.LabelEnd, (RenderFragment)(builder => builder.AddContent(0, "Maximum"))));

        cut.MarkupMatches(@"
            <ix-slider id=""slider-slots"" value=""0"" max=""100"" min=""0"" step=""1"" trace-reference=""0""><div slot=""label-start"">Minimum</div><div slot=""label-end"">Maximum</div></ix-slider>");
    }

    [Fact]
    public async Task ValueChanged_InvokesValueChangeEvent()
    {
        // Arrange
        double eventValue = 0;
        var cut = Render<Slider>(parameters => parameters
            .Add(p => p.Id, "slider2")
            .Add(p => p.ValueChangeEvent, EventCallback.Factory.Create<double>(this, v => eventValue = v))
        );

        // Act
        await cut.Instance.ValueChanged(75.5);

        // Assert
        Assert.Equal(75.5, eventValue);
    }

    [Fact]
    public void OnAfterRenderAsync_FirstRender_AttachesListenerAndSetsMarker()
    {
        // Arrange
        var jsRuntimeMock = new Mock<IJSRuntime>();
        var jsObjectReferenceMock = new Mock<IJSObjectReference>();

        jsRuntimeMock
            .Setup(js => js.InvokeAsync<IJSObjectReference>(
                "import",
                It.IsAny<object[]>()
            ))
            .ReturnsAsync(jsObjectReferenceMock.Object);

        jsObjectReferenceMock
            .Setup(js => js.InvokeAsync<string>(
                "listenEvent",
                It.IsAny<object[]>()
            ))
            .ReturnsAsync("fakeEventId");

        jsObjectReferenceMock
            .Setup(js => js.InvokeAsync<object>(
                "setMarker",
                It.IsAny<object[]>()
            ))
            .ReturnsAsync(new object());

        Services.AddSingleton(jsRuntimeMock.Object);

        var markerArray = new double[] { 0, 25, 50 };

        // Act
        var cut = Render<Slider>(parameters => parameters
            .Add(p => p.Id, "slider-js")
            .Add(p => p.Marker, markerArray)
        );

        // Assert
        jsObjectReferenceMock.Verify(js => js.InvokeAsync<string>(
            "listenEvent",
            It.Is<object[]>(args =>
                args.Length >= 4 &&
                args[1] != null &&
                args[1].ToString() == "slider-js" &&
                args[2] != null &&
                args[2].ToString() == "valueChange" &&
                args[3] != null &&
                args[3].ToString() == "ValueChanged"
            )
        ), Times.Once());

        jsObjectReferenceMock.Verify(js => js.InvokeAsync<object>(
            "setMarker",
            It.Is<object[]>(args =>
                args.Length == 2 &&
                args[0] != null &&
                args[0].ToString() == "slider-js" &&
                args[1] != null &&
                args[1].GetType() == typeof(double[]) &&
                ((double[])args[1]).SequenceEqual(markerArray)
            )
        ), Times.Once());
    }
}
