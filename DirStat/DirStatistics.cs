using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace DirStat
{
    public class DirStatistics
    {
        private FileStatItemDao _dao;
        private DirWalker _walker;
        private List<FileStatItem> StatItems;

        public DirStatistics(FileStatItemDao dao, DirWalker walker)
        {
            _dao = dao;
            _walker = walker;   
            StatItems = new List<FileStatItem>();
        }

        public DirStatistics(string dir) : this(new FileStatItemDao(dir), new DirWalker(dir)) 
        {
        }

        public List<FileStatItem> GetStatItems()
        {
            if (StatItems.Count == 0)
                GetStatItemsFromFile();
            if (StatItems.Count == 0)
                FreshStatistics();
            return StatItems;
        }

        private void GetStatItemsFromFile()
        {
            throw new NotImplementedException();
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
        }

        private IEnumerable<string> GetFiles()
        {
            throw new NotImplementedException();
        }
    }
}
