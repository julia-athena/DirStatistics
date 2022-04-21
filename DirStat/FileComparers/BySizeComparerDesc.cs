using System;
using System.Collections;
using System.Collections.Generic;

namespace DirStat.FileComparers
{
    public class BySizeComparerDesc : IComparer<StatItem>
    {
        public int Compare(StatItem x, StatItem y)
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
