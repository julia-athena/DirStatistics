using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirStat.Service.Extensions
{
    public static class IEnumerableExtension
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
