using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirStat.Dao
{
    public interface IStatItemDao
    {
        public List<StatItem> GetAll();
        public List<StatItem> GetByDirName(string dirName);
        public List<StatItem> GetByDirNameRec(string dirName);
        public void AddOrUpdateAll(List<StatItem> data); 
    }
}
