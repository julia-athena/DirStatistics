using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DirStat
{
    public class StatItem: IComparable<StatItem>
    {
        public string FullName;
        public long Size;
        public DateTime CreationTime;
        public readonly DateTime RegTime;

        public StatItem(string input)
        {
            FullName = ParseValue(nameof(FullName), input);
            Size = long.Parse(ParseValue(nameof(Size), input));
            CreationTime = DateTime.Parse(ParseValue(nameof(CreationTime), input));
            RegTime = DateTime.Parse(ParseValue(nameof(RegTime), input));
        }

        public StatItem()
        {
        }

        public override string ToString()
        {
            return $"FullName:{FullName},Size:{Size},CreationTime:{CreationTime},RegTime:{RegTime}";
        }

        public int CompareTo(StatItem? other)
        {
            if (other == null)
                return -1;
            return this.RegTime.CompareTo(other.RegTime);
        }
        private static string ParseValue(string key, string input)
        {
            string pattern = $@"{key}:\w*";
            var match = Regex.Match(input, pattern).Value;
            var value = match.Replace($"{key}:","");
            return value;
        }
    }
}
