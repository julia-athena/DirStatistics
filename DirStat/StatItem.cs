using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LiteDB;
using Newtonsoft.Json;

namespace DirStat
{
    public class StatItem: IEquatable<StatItem>
    {
        [BsonId]
        public string FullName;
        public string DirName;
        public string FileName;
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
            DirName = fileInfo.DirectoryName;
            FileName = fileInfo.Name;

        }

        public override string ToString()
        {
            return $"{FileName},{Size},{CreationTime},{RegTime}";
        }

        public bool Equals(StatItem other)
        {
            if (other == null)
                return false;
            return (FullName.ToString().GetHashCode() == other.FullName.ToString().GetHashCode()
                    && Size == other.Size
                    && CreationTime.ToString().GetHashCode() == other.CreationTime.ToString().GetHashCode()
                    && RegTime.ToString().GetHashCode() == other.RegTime.ToString().GetHashCode()
                    && DirName.GetHashCode() == other.GetHashCode()
                    && FileName.GetHashCode() == other.FileName.GetHashCode());  
        }
    }
}
