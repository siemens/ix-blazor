// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
// 
// SPDX-License-Identifier: MIT
// 
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;

namespace SiemensIXBlazor.Tests;

public class TestContextBase : TestContext
{
    public TestContextBase()
    {
        Mock<IJSRuntime> jsRuntimeMock = new();
        Mock<IJSObjectReference> jsObjectReferenceMock = new();

        // Mock of module import for JSRuntime
        jsRuntimeMock.Setup(x => x.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]>()))
            .Returns(new ValueTask<IJSObjectReference>(jsObjectReferenceMock.Object));
        Services.AddSingleton(jsRuntimeMock.Object);
    }
}