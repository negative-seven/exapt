// Copyright (C) 2024 negative_seven
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace Exapt.Wrappers;

public abstract class NonStaticWrapper<T>(object inner) : Wrapper<T>
{
    protected internal object Inner { get; private set; } = inner;

    protected object? Get(string fieldName)
    {
        return Utils.Get(Inner, fieldName);
    }

    protected object? Call(string methodName, params object[] arguments)
    {
        return Utils.CallNonStatic(Inner, methodName, arguments);
    }

    protected static object? CallConstructor(params object[] arguments)
    {
        return Utils.CallConstructor(WrappedType, arguments);
    }
}

public abstract class StaticWrapper<T> : Wrapper<T>
{
    internal StaticWrapper()
    {
        throw new MethodAccessException("Types inhering from StaticWrapper should not be instantiated.");
    }
}

public class Wrapper<T>
{
    private static Type? _wrappedType;
    protected static Type WrappedType => _wrappedType!;

    protected static void SetWrappedType(string typeName)
    {
        _wrappedType = Type.GetType($"{typeName}, Burbank")!;
    }

    protected static void SetStatic(string fieldName, object? value)
    {
        Utils.SetStatic(WrappedType, fieldName, value);
    }

    protected static object? CallStatic(string methodName, params object[] arguments)
    {
        return Utils.CallStatic(WrappedType, methodName, arguments);
    }
}
