using Xunit;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

namespace DirStat.Tests
{
    public class DirStatisticsTests
    {
        private List<string> DirPattern = new() { 
            "TestDirStat\\Dir1",
            "TestDirStat\\Dir2",
            "TestDirStat\\Dir2\\Dir2_1"
        };

        [Fact]
        public void GetDirStat()
        {
            GenerateTestData();

        }

        [Fact]
        public void GetDirStatFromStatFile()
        {
            GenerateTestData();
        }

        private void GenerateTestData()
        {
            if (Directory.Exists("TestDirStat"))
                Directory.Delete("TestDirStat", true);
            GenerateTestDirs();
            GenerateTestFiles();
        }

        private void GenerateTestDirs()
        {
            foreach (var dir in DirPattern)
            {
                Directory.CreateDirectory(dir); 
            }
        }
        private void GenerateTestFiles()
        {
            for (int i = 0; i < DirPattern.Count; i++)
            {
               using var fs = File.Create($"{DirPattern[i]}\\Test{i+1}.txt");
               AddText(fs, new string('*', (i+1)*512));
            }
        }

        private void AddText(FileStream fs, string text)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(text);
            fs.Write(info, 0, info.Length);
        }
    }
}