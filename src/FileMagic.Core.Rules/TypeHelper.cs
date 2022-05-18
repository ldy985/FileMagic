using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using JetBrains.Annotations;

namespace ldy985.FileMagic.Core.Rules;

[PublicAPI]
public static class TypeHelper
{
    /// <summary>
    ///     Creates an instance of all types that implement another class/interface.
    /// </summary>
    /// <seealso>https://stackoverflow.com/a/5120722</seealso>
    /// <param name="lookInAssembly"></param>
    /// <param name="callPrivateConstructors"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">The constructor being called throws an exception.</exception>
    [RequiresUnreferencedCode("Uses reflection to get types in an assembly")]
    public static IEnumerable<T> CreateAllImplementors<T>(Assembly? lookInAssembly = null, bool callPrivateConstructors = false)
    {
        foreach (Type type in GetAllTypesThatImplementInterface<T>(lookInAssembly))
        {
            T instance = (T)(Activator.CreateInstance(type, callPrivateConstructors) ?? throw new InvalidOperationException());
            yield return instance;
        }
    }

    /// <summary>
    ///     Creates an instance of all types that implement another class/interface.
    /// </summary>
    /// <seealso>https://stackoverflow.com/a/5120722</seealso>
    /// <param name="type"></param>
    /// <param name="lookInAssembly"></param>
    /// <param name="callPrivateConstructors"></param>
    /// <returns></returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">The constructor being called throws an exception.</exception>
    [RequiresUnreferencedCode("Uses reflection to get types in an assembly")]
    public static IEnumerable<object> CreateAllImplementors(Type type, Assembly? lookInAssembly = null, bool callPrivateConstructors = false)
    {
        foreach (Type typeCreate in GetAllTypesThatImplementInterface(type, lookInAssembly))
        {
            object instance = Activator.CreateInstance(typeCreate, callPrivateConstructors) ?? throw new InvalidOperationException();
            yield return instance;
        }
    }

    /// <summary>
    ///     Gets all types that implement another class/interface.
    /// </summary>
    /// <seealso>https://stackoverflow.com/a/5120722</seealso>
    /// <param name="assembly"></param>
    /// <typeparam name="T">The interface type.</typeparam>
    /// <returns></returns>
    /// <exception cref="T:System.Reflection.ReflectionTypeLoadException">The assembly contains one or more types that cannot be loaded. The array returned by the <see cref="P:System.Reflection.ReflectionTypeLoadException.Types" /> property of this exception contains a <see cref="T:System.Type" /> object for each type that was loaded and <see langword="null" /> for each type that could not be loaded, while the <see cref="P:System.Reflection.ReflectionTypeLoadException.LoaderExceptions" /> property contains an exception for each type that could not be loaded.</exception>
    [RequiresUnreferencedCode("Uses reflection to get types in an assembly")]
    public static IEnumerable<Type> GetAllTypesThatImplementInterface<T>(Assembly? assembly = null)
    {
        Type type = typeof(T);

        foreach (Type ruleType in GetAllTypesThatImplementInterface(type, assembly))
            yield return ruleType;
    }

    /// <summary>
    ///     Gets all types that implement another class/interface.
    /// </summary>
    /// <seealso>https://stackoverflow.com/a/5120722</seealso>
    /// <param name="type">The interface type.</param>
    /// <param name="assembly"></param>
    /// <returns></returns>
    /// <exception cref="T:System.Reflection.ReflectionTypeLoadException">The assembly contains one or more types that cannot be loaded. The array returned by the <see cref="P:System.Reflection.ReflectionTypeLoadException.Types" /> property of this exception contains a <see cref="T:System.Type" /> object for each type that was loaded and <see langword="null" /> for each type that could not be loaded, while the <see cref="P:System.Reflection.ReflectionTypeLoadException.LoaderExceptions" /> property contains an exception for each type that could not be loaded.</exception>
    [RequiresUnreferencedCode("Uses reflection to get types in an assembly")]
    private static IEnumerable<Type> GetAllTypesThatImplementInterface(Type type, Assembly? assembly)
    {
        if (assembly == null)
            assembly = type.Assembly;

        foreach (Type t in assembly.GetTypes())
        {
            if (!t.IsAbstract && t.IsClass && t.IsPublic && !t.IsGenericType && type.IsAssignableFrom(t))
                yield return t;
        }
    }
}