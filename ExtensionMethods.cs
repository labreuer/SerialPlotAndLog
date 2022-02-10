using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPlotAndLog
{
    internal class DuplicateKeyException : Exception
    {
        public object Object { get; protected set; }

        public DuplicateKeyException(object duplicate) : this(duplicate, null) { }
        public DuplicateKeyException(object duplicate, Exception inner)
            : base("Duplicate key encountered.", inner)
        {
            Object = duplicate;
        }
    }
    
    static internal class ExtensionMethods
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> list)
        {
            HashSet<T> h = new HashSet<T>();

            foreach (T el in list)
                if (!h.Add(el))
                    throw new DuplicateKeyException(el);

            return h;
        }

        public static string Nullify(this string s)
        {
            return string.IsNullOrEmpty(s) ? null : s;
        }

        public static string Join(this IEnumerable<string> strings, string delimiter)
        {
            return string.Join(delimiter, strings as string[] ?? strings.ToArray()).Nullify();
        }
    }
}
