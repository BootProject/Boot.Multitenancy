using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy.Extensions
{
    public static class ListExtensions
    {

        /// <summary>
        /// Converts an Collection to a generic List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection to convert.</param>
        /// <returns>Converted Collection to a List of T</returns>
        public static List<T> CollectionToList<T>(this System.Collections.ICollection collection)
        {
            var output = new List<T>(collection.Count);
            output.AddRange(collection.Cast<T>());

            return output;
        }
    }
}
