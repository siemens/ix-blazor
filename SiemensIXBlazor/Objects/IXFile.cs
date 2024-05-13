// -----------------------------------------------------------------------
// SPDX-FileCopyrightText: 2024 Siemens AG
//
// SPDX-License-Identifier: MIT
//
// This source code is licensed under the MIT license found in the
// LICENSE file in the root directory of this source tree.
//  -----------------------------------------------------------------------

namespace SiemensIXBlazor.Objects
{
    public class IXFile
    {
        public string Name { get; }
        public long Size { get; }
        public string Type { get; }
        public string Base64Data { get; }

        public IXFile(string name, long size, string type, string base64Data)
        {
            Name = name;
            Size = size;
            Type = type;
            Base64Data = base64Data;
        }
    }
}
