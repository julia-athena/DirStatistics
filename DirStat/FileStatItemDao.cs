using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirStat
{
    public class FileStatItemDao // 1. В чем отличие Dao от Репозитория
    {
        private readonly HashDatabase _database;
        private readonly Dictionary<string, int> _metadata;
        public FileStatItemDao(HashDatabase database)
        {
            _database = database;
            _metadata = new Dictionary<string, int>();
        }
        public List<FileStatItem> GetAll()
        {
            var res = new List<FileStatItem>();    
            return res;
        }
        public List<FileStatItem> GetByDir(string dir)
        {
            throw new NotImplementedException();
        }
        public void Add(List<FileStatItem> data)
        {

        }
    }
}
