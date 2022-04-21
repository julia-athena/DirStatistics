using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DirStat.FileComparers;


namespace DirStat
{
    public class DirStatistics
    {
        private DirectoryInfo DirInfo;
        public List<string> DirFiles;
        public List<StatItem> StatItems;
        private readonly string StatFileName = "DirStat.stat";

        public DirStatistics(string dirPath)
        {
            DirInfo = new DirectoryInfo(dirPath);
            StatItems = new List<StatItem>();
            DirFiles = new List<string>();
            StatFileName = Path.Combine(dirPath, StatFileName);
        }
        public List<StatItem> GetTopNStatItems(int n, IComparer<StatItem> comparer)
        {
            if (StatItems.Count == 0)
                ReadStatItemsFromFile();
            if (StatItems.Count == 0)
                FreshStatistics();
            if (StatItems.Count > 0)
                StatItems.Sort(comparer);
            return StatItems.GetRange(0, Math.Min(n, StatItems.Count));
        }

        public List<StatItem> GetStatItems(IComparer<StatItem> comparer)
        {
            if (StatItems.Count == 0)
                ReadStatItemsFromFile();
            if (StatItems.Count == 0)
                FreshStatistics();
            if (StatItems.Count > 0)
                StatItems.Sort(comparer);
            return StatItems;
        }

        public void FreshStatistics()
        {
            DirFiles.Clear();
            StatItems.Clear();  
            WalkDir();
            foreach (var fileName in DirFiles)
            {
                var file = new FileInfo(fileName);  
                StatItems.Add(new StatItem { 
                    CreationTime = file.CreationTime, 
                    FullName = file.FullName, 
                    Size = file.Length
                });
            }
            WriteStatItemsToFile();
        }

        private void ReadStatItemsFromFile()
        {

            try
            {
                using (StreamReader reader = new StreamReader(StatFileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        StatItems.Add(StatItem.GetInstanceFromStr(line));
                    }
                }
            }
            catch (FileNotFoundException e)
            {

                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private void WriteStatItemsToFile()
        {
            StatItems.Sort();
            using (StreamWriter writer = new StreamWriter(StatFileName, false))
            {
                foreach (var item in StatItems)
                {
                    writer.WriteLine(item.ToString());
                }
            }
        }

        private void WalkDir()
        {
            var stack = new Stack<string>();
            stack.Push(DirInfo.FullName);
            while(stack.Count > 0)
            {
                var currDir = stack.Pop();
                var subDirs = GetSubDirs(currDir); 
                foreach(var subDir in subDirs)
                {
                    stack.Push(subDir);
                }
                var currFiles = GetFiles(currDir);    
                foreach(var currFile in currFiles)
                {
                    DirFiles.Add(currFile);
                }
            }
        }
        private string[] GetSubDirs(string dir)
        {
            try
            {
                return Directory.GetDirectories(dir);
            }
            catch
            {
                return Array.Empty<string>();
            }
        }
        private string[] GetFiles(string dir)
        {
            try
            {
                return Directory.GetFiles(dir);
            }
            catch
            {
                return Array.Empty<string>();
            }
        }
    }
}
