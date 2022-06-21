using DirStat.Service.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DirStat.Service.Dao;
using DirStat.Dao.Implementation.FileDb;


namespace DirStat.Service
{
    public class DirStatisticsServer
    {
        private IStatItemDao Dao;
        private DirWalker Walker;
        private readonly string _path;
        public DirStatisticsServer(string path) : this(path, new FileDbStatItemDao())
        {
        }
        public DirStatisticsServer(string path, IStatItemDao dao)
        {
            _path = path ?? throw new ArgumentNullException();
            Dao = dao ?? throw new ArgumentNullException();
            Walker = new DirWalker(path);
        }

        public void FreshDataForStatistics()
        {
            var files = Walker.GetFilesRec();
            Dao.AddOrUpdateAll(files);
        }

        public IEnumerable<StatItem> GetTopBigFiles(int? n) 
        {
            var items = Dao.GetByDirNameRec(_path);
            var result = items.OrderByDescending(x => x.Size)
                              .ToList();
            var count = Math.Min(result.Count, n is null ? result.Count : n ?? default); 
            return result.Take(count);      
        }
        public IEnumerable<StatItem> GetTopOldFiles(int? n)
        {
            var items = Dao.GetByDirNameRec(_path);
            var result = items.OrderBy(x => x.CreationTime)
                              .ToList();
            var count = Math.Min(result.Count, n is null ? result.Count : n ?? default);
            return result.Take(count);
        }
        public IEnumerable<ExtensionInfo> GetTopExtensions(int? n)
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
            var result = dictionary.OrderByDescending(x => x.Value)
                                   .Select(x => new ExtensionInfo { 
                                       Name = x.Key, 
                                       Frequency = x.Value})
                                   .ToList();
            var count = Math.Min(result.Count, n is null ? result.Count : n ?? default);
            return result.Take(count);
        }
    }
}
