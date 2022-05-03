using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirStat
{
    public class ExtensionStatItem: IEquatable<ExtensionStatItem>
    {
        public string Name { get; set; }
        public int Frequency { get; set; }

        public bool Equals(ExtensionStatItem other)
        {
            if (other == null)
                return false;
            return (Name == other.Name
                   && Frequency == other.Frequency);
        }
    }
}
