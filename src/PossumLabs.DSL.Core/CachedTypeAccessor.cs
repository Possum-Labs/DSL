using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PossumLabs.DSL.Core
{
    public static class CachedTypeAccessor
    {
        static CachedTypeAccessor()
        {
            Properties = new ConcurrentDictionary<Type, PropertyInfo[]>();
            Fields = new ConcurrentDictionary<Type, FieldInfo[]>();
            Methods = new ConcurrentDictionary<Type, MethodInfo[]>();
            Constructors = new ConcurrentDictionary<Type, ConstructorInfo[]>();
        }

        private static ConcurrentDictionary<Type, PropertyInfo[]> Properties { get; }
        public static PropertyInfo[] CachedGetProperties(this Type t)
            =>Properties.GetOrAdd(t, (k) => k.GetProperties(BindingFlags.Public | BindingFlags.Instance));

        private static ConcurrentDictionary<Type, FieldInfo[]> Fields { get; }
        public static FieldInfo[] CachedGetFields(this Type t)
            => Fields.GetOrAdd(t, (k) => k.GetFields(BindingFlags.Public | BindingFlags.Instance));

        private static ConcurrentDictionary<Type, MethodInfo[]> Methods { get; }
        public static MethodInfo[] CachedGetMethods(this Type t)
            => Methods.GetOrAdd(t, (k) => k.GetMethods());

        private static ConcurrentDictionary<Type, ConstructorInfo[]> Constructors { get; }
        public static ConstructorInfo[] CachedGetConstructors(this Type t)
            => Constructors.GetOrAdd(t, (k) => k.GetConstructors(BindingFlags.Public | BindingFlags.Instance));
    }
}
