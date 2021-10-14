using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace ldy985.FileMagic.Core.Rules
{
    [PublicAPI]
    public static class TypeHelper
    {
        /// <summary>
        /// Creates an instance of all types that implement another class/interface.
        /// </summary>
        /// <seealso>https://stackoverflow.com/a/5120722</seealso>
        /// <param name="lookInAssembly"></param>
        /// <param name="callPrivateConstructors"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [ItemNotNull]
        [Pure]
        public static IEnumerable<T> CreateAllImplementors<T>(Assembly? lookInAssembly = null, bool callPrivateConstructors = false)
        {
            foreach (Type type in GetAllTypesThatImplementInterface<T>(lookInAssembly))
            {
                T instance = (T)(Activator.CreateInstance(type, callPrivateConstructors) ?? throw new InvalidOperationException());
                yield return instance;
            }
        }

        /// <summary>
        /// Gets all types that implement another class/interface.
        /// </summary>
        /// <seealso>https://stackoverflow.com/a/5120722</seealso>
        /// <param name="assembly"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<Type> GetAllTypesThatImplementInterface<T>(Assembly? assembly = null)
        {
            Type type = typeof(T);

            if (assembly == null)
                assembly = type.Assembly;

            foreach (Type t in assembly.GetTypes())
            {
                if (!t.IsAbstract && t.IsClass && t.IsPublic && !t.IsGenericType && type.IsAssignableFrom(t))
                    yield return t;
            }
        }
    }
}