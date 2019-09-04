using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrowseSharp.Common.Extensions
{
    static class ListExtension
    {
        public static T Pop<T>(this List<T> list)
        {
            T r = list.Last();
            list.Remove(list.Last());
            return r;
        }

        public static void Push<T>(this List<T> list, T item)
        {
            list.Add(item);
        }
    }
}
