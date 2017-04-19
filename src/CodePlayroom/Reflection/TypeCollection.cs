using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CodePlayroom.Reflection
{
    public class TypeCollection : IEnumerable<Type>
    {
        private List<Type> _types = new List<Type>();

        public int Count => _types.Count;

        public Type this[int index] => _types[index];

        /// <summary>
        /// Returns a <see cref="TypeCollection"/> containing all of the types in <paramref name="assembly"/>
        /// that have a base type of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The base type from which the returned types must be derived.</typeparam>
        /// <param name="assemblies">The assemblies from which types should be loaded.</param>
        /// <returns></returns>
        public static TypeCollection GetAllTypesDerivedFrom<T>(params Assembly[] assemblies)
        {
            var matches = new List<Type>();

            var typeOfT = typeof(T);
            foreach (var assembly in assemblies)
            {
                Func<Type, bool> predicate = (t) => t.GetTypeInfo().IsSubclassOf(typeOfT);
                if (typeOfT.GetTypeInfo().IsInterface)
                    predicate = (t) => typeOfT.IsAssignableFrom(t) && typeOfT != t;
                var types = assembly.GetTypes().Where(predicate).ToArray();
                if (types.Length > 0)
                    matches.AddRange(types);
            }

            return new TypeCollection { _types = matches };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Type> GetEnumerator()
        {
            return _types.GetEnumerator();
        }
    }
}