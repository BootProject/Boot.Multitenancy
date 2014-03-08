using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy.Extensions
{
    public static class ListExtensions
    {
        public static List<T> CollectionToList<T>(this System.Collections.ICollection collection)
        {
            var output = new List<T>(collection.Count);
            output.AddRange(collection.Cast<T>());

            return output;
        }
    }
}
