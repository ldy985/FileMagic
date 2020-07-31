using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;

namespace ldy985.FileMagic.Core.Rules
{
    [PublicAPI]
    public static class TypeHelper
    {
        [NotNull]
        [ItemNotNull]
        [Pure]
        public static IEnumerable<T> CreateInstanceOfAll<T>([CanBeNull]Assembly lookInAssembly = null, bool callPrivateConstructors = false)
        {
            foreach (Type type in GetInstanceTypesInheritedFrom<T>(lookInAssembly))
            {
                if (type.ContainsGenericParameters)
                    continue;

                yield return (T)Activator.CreateInstance(type, callPrivateConstructors);
            }
        }

        [NotNull]
        [ItemNotNull]
        [Pure]
        public static IEnumerable<Type> GetTypesInheritedFrom<T>([CanBeNull]Assembly assembly = null)
        {
            return GetTypesInheritedFrom(typeof(T), assembly);
        }

        [NotNull]
        [ItemNotNull]
        [Pure]
        public static IEnumerable<Type> GetTypesInheritedFrom(Type type, Assembly assembly = null)
        {
            if (assembly == null)
                assembly = type.Assembly;

            foreach (Type exportedType in assembly.GetTypes())
            {
                if (exportedType == type)
                    continue;

                if (type.IsAssignableFrom(exportedType))
                    yield return exportedType;
            }
        }

        [NotNull]
        [ItemNotNull]
        [Pure]
        public static IEnumerable<Type> GetInstanceTypesInheritedFrom<T>([CanBeNull]Assembly assembly = null)
        {
            return GetInstanceTypesInheritedFrom(typeof(T), assembly);
        }

        [NotNull]
        [ItemNotNull]
        [Pure]
        public static IEnumerable<Type> GetInstanceTypesInheritedFrom(Type type, Assembly assembly = null)
        {
            if (assembly == null)
                assembly = type.Assembly;

            foreach (Type exportedType in GetTypesInheritedFrom(type, assembly))
            {
                if (!exportedType.IsClass)
                    continue;

                if (exportedType.IsAbstract)
                    continue;

                if (exportedType.IsInterface)
                    continue;

                yield return exportedType;
            }
        }
    }
}