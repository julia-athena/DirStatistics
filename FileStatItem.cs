using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirStat
{
    internal class FileStatItem
    {
        public string FileName;
        public int Size;
        public DateTime CreateTime;
        public DateTime RegTime;
        public override string ToString()
        {
            return $"{FileName},{Size},{CreateTime},{RegTime}";
        }
    }
}
