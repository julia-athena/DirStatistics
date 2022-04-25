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
        private readonly string StatFileName = "DirStat.stat";

        public DirStatistics(string dirPath)
        {
            DirInfo = new DirectoryInfo(dirPath);
            StatItems = new List<StatItem>();
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
            StatItems.Clear();  
            var files = GetFiles();
            foreach (var fileName in files)
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
                using (var reader = new StreamReader(StatFileName))
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
            using (var writer = new StreamWriter(StatFileName, false))
            {
                foreach (var item in StatItems)
                {
                    writer.WriteLine(item.ToString());
                }
            }
        }

        private IEnumerable<string> GetFiles()
        {
            return DirWalker.GetFilesRec(DirInfo.FullName);
        }
    }
}
