using System.Reflection;

namespace Exapt.Wrappers;

internal static class Utils
{
    internal static object? Get(object receiver, string fieldName)
    {
        Type type = receiver.GetType();
        FieldInfo field =
            type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            ?? throw new FindMemberException(
                $"Failed to find field \"{fieldName}\" in type \"${type.AssemblyQualifiedName}\""
            );
        return field.GetValue(receiver);
    }

    internal static void SetStatic(Type type, string fieldName, object? value)
    {
        FieldInfo field =
            type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
            ?? throw new FindMemberException(
                $"Failed to find field \"{fieldName}\" in type \"${type.AssemblyQualifiedName}\""
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
            ?? throw new FindMemberException($"Failed to find constructor for type \"${type.AssemblyQualifiedName}\"");
        return constructor.Invoke(arguments);
    }

    internal static object? Call(Type type, string methodName, object? receiver, params object[] arguments)
    {
        MethodInfo method =
            type.GetMethod(methodName)
            ?? throw new FindMemberException(
                $"Failed to find method \"{methodName}\" in type \"${type.AssemblyQualifiedName}\""
            );
        return method.Invoke(receiver, [.. arguments]);
    }

    internal static void WithWorkingDirectory(string directory, Action action)
    {
        string originalDirectory = Directory.GetCurrentDirectory();
        Directory.SetCurrentDirectory(directory);
        action();
        Directory.SetCurrentDirectory(originalDirectory);
    }
}
