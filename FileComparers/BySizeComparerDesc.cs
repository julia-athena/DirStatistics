using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirStat.FileComparers
{
    internal class BySizeComparerDesc : IComparer<FileInfo>
    {
        public int Compare(FileInfo? x, FileInfo? y)
        {
            if (x.Length < y.Length)
                return 1;
            else if (x.Length == y.Length)
                return 0;
            else 
                return -1;
        }
    }
}
