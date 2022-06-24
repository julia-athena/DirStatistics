using System;
using System.Collections.Generic;
using System.Linq;

namespace DirStat.Extensions
{
    public static class EnumerableExtension
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            if (source is null) 
                return true;   
            else 
                return !source.Any();
        }
    }
}
