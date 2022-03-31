using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirStat
{
    internal class StatItem: IComparable<StatItem>
    {
        public string FullName;
        public long Size;
        public DateTime CreationTime;
        public readonly DateTime RegTime;
        public override string ToString()
        {
            return $"{FullName},{Size},{CreationTime},{RegTime}";
        }

        public int CompareTo(StatItem other)
        {
            if (other == null)
                return -1;
            return this.FullName.CompareTo(other.FullName);
        }
    }
}
