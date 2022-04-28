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
        public List<StatItem> StatItems;
        private readonly string DbFile = "DirStat.txt";

        public DirStatistics(string path)
        {
            DirInfo = new DirectoryInfo(path);
            StatItems = new List<StatItem>();
            DbFile = Path.Combine(path, DbFile);
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
        public List<string> GetTopNExtensions(int n)
        {
            throw new NotImplementedException();    
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
            StatItems.Clear();  
            var files = GetFiles();
            foreach (var fileName in files)
            {
                var file = new FileInfo(fileName);
                StatItems.Add(new StatItem(file));
            }
            WriteStatItemsToFile();
        }

        private void ReadStatItemsFromFile()
        {
            try
            {
                var dbFile = FindDbFileOrNull();
                if (dbFile == null) return;
                using var reader = new StreamReader(dbFile);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith(DirInfo.FullName))
                    {
                        StatItems.Add(new StatItem(line));
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
            using var writer = new StreamWriter(DbFile, false);
            foreach (var item in StatItems)
            {
                writer.WriteLine(item.ToString());
            }
        }
        private string FindDbFileOrNull()
        {
            var dir = DirInfo;
            string res = default;
            while (dir != DirInfo.Root)
            {
                try
                {
                    var files = dir.GetFiles("DirStat.txt");
                    if (files != null)
                    {
                        res = files[0].FullName;
                        break;
                    }
                    dir = DirInfo.Parent;
                }
                catch (System.Security.SecurityException e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }
            return res;
        }

        private IEnumerable<string> GetFiles()
        {
            return DirWalker.GetFilesRec(DirInfo.FullName);
        }
    }
}
