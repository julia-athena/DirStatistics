using System;
using System.IO;
using LiteDB;

namespace DirStat.Models
{
    public class StatItem: IEquatable<StatItem>
    {
        [BsonId]
        public string FullName { get; set; }
        public string DirName { get; set; }
        public string FileName { get; set; }
        public long Size { get; set; }
        public DateTime CreationTime { get; set; }
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
                    && DirName.GetHashCode() == other.DirName.GetHashCode()
                    && FileName.GetHashCode() == other.FileName.GetHashCode());  
        }
    }
}
