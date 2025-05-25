// Copyright (C) 2024 The exapt authors
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not
// distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System.Reflection;

namespace Exapt.Wrappers.Meta;

internal static class Utils
{
    internal static object? Get(object receiver, string fieldName)
    {
        Type type = receiver.GetType();
        FieldInfo field =
            type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            ?? throw new FindMemberException(
                $@"Failed to find field ""{fieldName}"" in type ""${type.AssemblyQualifiedName}"""
            );
        return field.GetValue(receiver);
    }

    internal static object? GetStatic(Type type, string fieldName)
    {
        FieldInfo field =
            type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
            ?? throw new FindMemberException(
                $@"Failed to find field ""{fieldName}"" in type ""${type.AssemblyQualifiedName}"""
            );
        return field.GetValue(null);
    }

    internal static void SetStatic(Type type, string fieldName, object? value)
    {
        FieldInfo field =
            type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
            ?? throw new FindMemberException(
                $@"Failed to find field ""{fieldName}"" in type ""${type.AssemblyQualifiedName}"""
            );
        field.SetValue(null, value);
    }

    internal static object? CallNonStatic(object receiver, string methodName, params object[] arguments)
    {
        return Call(receiver.GetType(), methodName, receiver, arguments);
    }

    internal static object? CallStatic(Type type, string methodName, params object[] arguments)
    {
        return Call(type, methodName, null, arguments);
    }

    internal static object? CallConstructor(Type type, params object[] arguments)
    {
        ConstructorInfo constructor =
            type.GetConstructor(arguments.Select(a => a.GetType()).ToArray())
            ?? throw new FindMemberException($@"Failed to find constructor for type ""${type.AssemblyQualifiedName}""");
        return constructor.Invoke(BindingFlags.DoNotWrapExceptions, null, arguments, null);
    }

    internal static object? Call(Type type, string methodName, object? receiver, params object[] arguments)
    {
        MethodInfo method =
            type.GetMethod(
                methodName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance
            )
            ?? throw new FindMemberException(
                $@"Failed to find method ""{methodName}"" in type ""${type.AssemblyQualifiedName}"""
            );
        return method.Invoke(receiver, BindingFlags.DoNotWrapExceptions, null, [.. arguments], null);
    }

    internal static T WithWorkingDirectory<T>(string directory, Func<T> func)
    {
        string originalDirectory = Directory.GetCurrentDirectory();
        Directory.SetCurrentDirectory(directory);
        try
        {
            T returnValue = func();
            return returnValue;
        }
        finally
        {
            Directory.SetCurrentDirectory(originalDirectory);
        }
    }
}
