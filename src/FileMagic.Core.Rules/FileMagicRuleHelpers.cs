using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FastGenericNew;
using JetBrains.Annotations;
using ldy985.FileMagic.Abstracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ldy985.FileMagic.Core.Rules;

[SuppressMessage("Roslynator", "RCS1043:Remove \'partial\' modifier from type with a single part.")]
[SuppressMessage("ReSharper", "PartialTypeWithSinglePart")]
public static partial class FileMagicRuleHelpers
{
    /// <exception cref="T:System.Reflection.ReflectionTypeLoadException">The assembly contains one or more types that cannot be loaded. The array returned by the <see cref="P:System.Reflection.ReflectionTypeLoadException.Types" /> property of this exception contains a <see cref="T:System.Type" /> object for each type that was loaded and <see langword="null" /> for each type that could not be loaded, while the <see cref="P:System.Reflection.ReflectionTypeLoadException.LoaderExceptions" /> property contains an exception for each type that could not be loaded.</exception>
    /// <exception cref="T:System.MissingMemberException">Unable to find the type.</exception>
    /// <exception cref="T:System.TypeLoadException"><paramref name="type" /> is not a valid type.</exception>
    /// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor.
    /// Note: In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.</exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">The constructor being called throws an exception.</exception>
    /// <exception cref="T:System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
    /// <exception cref="T:System.MissingMethodException">No matching public constructor was found.
    /// Note: In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MissingMemberException" />, instead.</exception>
    [Pure]
    [RequiresUnreferencedCode("Uses reflection to instantiate rules derived from TRule")]
    public static IEnumerable<TRule> CreateRules<TRule>(ILoggerFactory? loggerFactory = null, Assembly? lookInAssembly = null) where TRule : IRule
    {
        loggerFactory ??= NullLoggerFactory.Instance;

        foreach (Type type in TypeHelper.GetAllTypesThatImplementInterface<TRule>(lookInAssembly))
        {
            if (type.ContainsGenericParameters)
                continue;

            Type log = typeof(Logger<>);
            Type genericLogger = log.MakeGenericType(type);
            object logger = Activator.CreateInstance(genericLogger, loggerFactory) ?? throw new MissingMemberException();
            yield return (TRule)(Activator.CreateInstance(type, logger) ?? throw new MissingMemberException());
        }
    }

    [Pure]
    public static TRule CreateRule<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] TRule>(
        ILoggerFactory? loggerFactory = null) where TRule : class, IRule
    {
        loggerFactory ??= NullLoggerFactory.Instance;
        ILogger<TRule> logger = loggerFactory.CreateLogger<TRule>();
        TRule rule = FastNew.CreateInstance<TRule, ILogger<TRule>>(logger);
        return rule;
    }
}