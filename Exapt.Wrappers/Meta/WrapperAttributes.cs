// Copyright (C) 2024 The exapt authors
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Diagnostics;

namespace Exapt.Wrappers.Meta;

[AttributeUsage(AttributeTargets.Class)]
internal sealed class ClassWrapperAttribute(string innerClassName) : Attribute
{
    public string InnerClassName { get; private set; } = innerClassName;
}

[AttributeUsage(AttributeTargets.Method)]
internal sealed class MethodWrapperAttribute(string innerMethodName) : Attribute
{
    public string InnerMethodName { get; private set; } = innerMethodName;

    public static object Stub()
    {
        throw new UnreachableException("Method intended to be reverse patched");
    }
}
