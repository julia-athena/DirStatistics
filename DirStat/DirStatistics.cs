using DirStat.Dao;
using DirStat.Dao.Impl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace DirStat
{
    public class DirStatistics
    {
        private IStatItemDao Dao;
        private DirWalker Walker;
        private readonly string _path;

        public DirStatistics(string path) : this(path, new FileDbStatItemDao())
        {
        }
        public DirStatistics(string path, IStatItemDao dao)
        {
            _path = path;
            Dao = dao;
            Walker = new DirWalker(path);
        }

        public void FreshStatItemsInfo()
        {
            var items = Walker.GetFilesRec();
            Dao.AddOrUpdateAll(items);
        }

        public List<StatItem> GetTopBigFiles(int n = 0)
        {
            var items = Dao.GetByDirNameRec(_path);
            var result = items.OrderByDescending(x => x.Size).ToList();
            return result;      
        }
        public List<StatItem> GetTopOldFiles(int n = 0)
        {
            var items = Dao.GetByDirNameRec(_path);
            var result = items.OrderBy(x => x.CreationTime).ToList();
            return result;
        }
        public List<ExtensionInfo> GetTopExtensions(int n = 0)
        {
            var items = Dao.GetByDirNameRec(_path);
            var dictionary = new Dictionary<string, int>();
            foreach (var item in items)
            {
                if(dictionary.ContainsKey(Path.GetExtension(item.FileName)))
                    dictionary[Path.GetExtension(item.FileName)]++; 
                else
                    dictionary.Add(Path.GetExtension(item.FileName), 1);
            }
            var result = dictionary.OrderBy(x => x.Value)
                                   .Select(x => new ExtensionInfo { 
                                       Name = x.Key, 
                                       Frequency = x.Value})
                                   .ToList();
            return result ?? new List<ExtensionInfo>();
        }
    }
}
