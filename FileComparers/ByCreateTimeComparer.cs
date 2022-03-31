using System;
using System.Collections;


namespace DirStat.FileComparers
{
    internal class ByCreateTimeComparer: IComparer<StatItem>
    {
        public int Compare(StatItem? x, StatItem? y)
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
