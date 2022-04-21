using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DirStat
{
    public class StatItem: IComparable<StatItem>
    {
        public string FullName;
        public long Size;
        public DateTime CreationTime;
        public DateTime RegTime = DateTime.Now;

        public static StatItem GetInstanceFromStr(string input)
        {
            var fields = input.Split(',');
            return new StatItem()
            {
                FullName = fields[0],
                Size = long.Parse(fields[1]),
                CreationTime = DateTime.Parse(fields[2]),
                RegTime = DateTime.Parse(fields[3])
            };
        }
        public StatItem()
        {
        }

        public override string ToString()
        {
            return $"{FullName},{Size},{CreationTime},{RegTime}";
        }

        public int CompareTo(StatItem other)
        {
            if (other == null)
                return -1;
            return this.RegTime.CompareTo(other.RegTime);
        }
    }
}
