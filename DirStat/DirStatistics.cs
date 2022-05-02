using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace DirStat
{
    public class DirStatistics
    {
        private DirectoryInfo DirInfo;
        private List<FileStatItem> StatItems;
        private readonly string DbFile = "DirStat.txt";

        public DirStatistics(string path)
        {
            DirInfo = new DirectoryInfo(path);
            StatItems = new List<FileStatItem>();
            DbFile = Path.Combine(path, DbFile);
        }
        public List<FileStatItem> GetStatItems()
        {
            if (StatItems.Count == 0)
                GetStatItemsFromFile();
            if (StatItems.Count == 0)
                FreshStatistics();
            return StatItems;
        }
        public List<ExtensionStatItem> GetExtensions()
        {
            if (StatItems.Count == 0)
                FreshStatistics();
            var extensionStat = new Dictionary<string, int>();
            foreach (var item in StatItems)
            {
                var curr = Path.GetExtension(item.FullName);
                if (!extensionStat.ContainsKey(curr))
                {
                    extensionStat.Add(curr, 1);
                }
                else
                {
                    extensionStat[curr]++;
                }
            }
            var res = extensionStat.Select(x => new ExtensionStatItem { Name = x.Key, Frequency = x.Value}).ToList();
            return res;
        }
        public void FreshStatistics()
        {
            StatItems.Clear();  
            var files = GetFiles();
            foreach (var fileName in files)
            {
                var file = new FileInfo(fileName);
                StatItems.Add(new FileStatItem(file));
            }
            WriteStatItemsToFile();
        }

        private void GetStatItemsFromFile()
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
                        StatItems.Add(new FileStatItem(line));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading DbFile: {e.Message}");
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
