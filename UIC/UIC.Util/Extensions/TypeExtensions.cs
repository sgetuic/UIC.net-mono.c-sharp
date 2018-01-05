using System;
using System.Collections.Generic;
using System.Linq;

namespace UIC.Util.Extensions {
    public static class TypeExtensions {

        public static string GetNameWithAssembly(this Type type) {
            return type.FullName + ", " + type.Assembly.GetName().Name;
        }

        /// <summary>
        ///     Determines whether the <paramref name="genericType" /> is assignable from
        ///     <paramref name="givenType" /> taking into account generic definitions
        /// </summary>
        public static bool IsAssignableToGenericType(this Type givenType, Type genericType) {
            if (givenType == null || genericType == null) {
                return false;
            }

            return givenType == genericType
                   || givenType.MapsToGenericTypeDefinition(genericType)
                   || givenType.HasInterfaceThatMapsToGenericTypeDefinition(genericType)
                   || givenType.BaseType.IsAssignableToGenericType(genericType);
        }

        private static bool HasInterfaceThatMapsToGenericTypeDefinition(this Type givenType, Type genericType) {
            return givenType
                .GetInterfaces()
                .Where(it => it.IsGenericType)
                .Any(it => it.GetGenericTypeDefinition() == genericType);
        }

        private static bool MapsToGenericTypeDefinition(this Type givenType, Type genericType) {
            return genericType.IsGenericTypeDefinition
                   && givenType.IsGenericType
                   && givenType.GetGenericTypeDefinition() == genericType;
        }


        private static readonly Dictionary<Tuple<Type, bool, bool>, string> _PrettyNames
            = new Dictionary<Tuple<Type, bool, bool>, string>();
        public static string PrettyName(this Type type, bool includeNamespace = false, bool includeNamespaceInGenericParameter = false) {
            var key = Tuple.Create<Type, bool, bool>(type, includeNamespace, includeNamespaceInGenericParameter);
            if (!_PrettyNames.ContainsKey(key)) {
                string name = type.Name;
                if (type.FullName != null && type.Namespace != null) {
                    int start = includeNamespace ? 0 : type.Namespace.Length + 1;
                    int stop = type.FullName.IndexOf("+<>");
                    name = stop < 0
                        ? type.FullName.Substring(start)
                        : type.FullName.Substring(start, stop);
                }

                if (type.GetGenericArguments().Length == 0)
                    return name.Replace("+", ".");

                int index = name.IndexOf("`", StringComparison.InvariantCulture);
                if (index < 0)
                    return name;

                var unmangledName = name
                    .Substring(0, index)
                    .Replace("+", ".");
                var genericArguments = type.GetGenericArguments();
                _PrettyNames[key] = unmangledName + "<" + String.Join(",", genericArguments.Select(t =>
                    PrettyName(t, includeNamespaceInGenericParameter, includeNamespaceInGenericParameter))) + ">";
            }
            return _PrettyNames[key];
        }

        public static string GetNameWithoutGenericArity(this Type t) {
            string name = t.Name;
            int index = name.IndexOf('`');
            return index == -1 ? name : name.Substring(0, index);
        }

        public static bool IsSubclassOfGenericType(this Type toCheck, Type generic) {
            if (generic.IsInterface) {
                return toCheck.GetInterfaces().Any(t => (t.IsGenericType ? t.GetGenericTypeDefinition() : t) == generic);
            }
            while (toCheck != null && toCheck != typeof(object)) {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur) {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }

        public static Type As(this Type type, Type targetType) {
            if (targetType.IsInterface) {
                return type.GetInterfaces().FirstOrDefault(t => (t.IsGenericType ? t.GetGenericTypeDefinition() : t) == targetType);
            }

            while (type != null && type != typeof(object)) {
                var cur = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
                if (targetType == cur) {
                    return type;
                }
                type = type.BaseType;
            }
            return null;
        }

        public static Type Cast(this Type type, Type targetType) {
            var ret = type.As(targetType);
            if (ret == null)
                throw new InvalidCastException();
            return ret;
        }
    }
}
