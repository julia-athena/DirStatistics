using System;
using System.Collections.Generic;
using System.IO;
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

        public StatItem()
        {
        }

        public StatItem(FileInfo fileInfo)
        {
            CreationTime = fileInfo.CreationTime;
            FullName = fileInfo.FullName;
            Size = fileInfo.Length;
        }
        public StatItem(string input)
        {
            var fields = input.Split(',');
            FullName = fields[0];
            Size = long.Parse(fields[1]);
            CreationTime = DateTime.Parse(fields[2]);
            RegTime = DateTime.Parse(fields[3]);
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
