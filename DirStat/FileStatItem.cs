﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DirStat
{
    public class FileStatItem: IComparable<FileStatItem>
    {
        public string FullName;
        public long Size;
        public DateTime CreationTime;
        public DateTime RegTime = DateTime.Now;

        public FileStatItem()
        {
        }

        public FileStatItem(FileInfo fileInfo)
        {
            CreationTime = fileInfo.CreationTime;
            FullName = fileInfo.FullName;
            Size = fileInfo.Length;
        }
        public FileStatItem(string input)
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

        public int CompareTo(FileStatItem other)
        {
            if (other == null)
                return -1;
            return this.RegTime.CompareTo(other.RegTime);
        }
    }
}