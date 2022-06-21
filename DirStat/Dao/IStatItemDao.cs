using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirStat.Service.Dao
{
    public interface IStatItemDao
    {
        public IList<StatItem> GetAll();
        public IList<StatItem> GetByDirName(string dirName);
        public IList<StatItem> GetByDirNameRec(string dirName);
        public void AddOrUpdateAll(List<StatItem> data); 
    }
}
