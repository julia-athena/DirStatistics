using System;
using System.Collections;
using System.Collections.Generic;

namespace DirStat.FileComparers
{
    public class ByCreateTimeComparer: IComparer<StatItem>
    {
        public int Compare(StatItem x, StatItem y)
        {
            if (x.CreationTime > y.CreationTime)
                return 1;
            else if (x.CreationTime == y.CreationTime)
                return 0;
            else
                return -1;
        }
    }
}
