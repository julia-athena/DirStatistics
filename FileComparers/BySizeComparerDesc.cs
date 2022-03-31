using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirStat.FileComparers
{
    internal class BySizeComparerDesc : IComparer<StatItem>
    {
        public int Compare(StatItem? x, StatItem? y)
        {
            if (x.Size < y.Size)
                return 1;
            else if (x.Size == y.Size)
                return 0;
            else 
                return -1;
        }
    }
}
