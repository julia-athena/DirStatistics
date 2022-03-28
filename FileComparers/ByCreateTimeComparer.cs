using System;
using System.Collections;


namespace DirStat.FileComparers
{
    internal class ByCreateTimeComparer: IComparer<FileInfo>
    {
        public int Compare(FileInfo? x, FileInfo? y)
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
