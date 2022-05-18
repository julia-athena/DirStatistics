using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirStat.Dao.Impl
{
    public class LiteDbStatItemDao : IStatItemDao
    {
        private readonly string _filepath;
        public LiteDbStatItemDao(string filepath)
        {
            _filepath = filepath;
        }
        public LiteDbStatItemDao() : this("StatItem.db")
        {
        }

        public List<StatItem> GetAll() // какой тип возвращать в сигнатуре?
        {
            using(var db = new LiteDatabase(_filepath)) // открывает или создает бд
            {
                var collection = db.GetCollection<StatItem>("StatItems");  //получить коллекцию или создать 
                var result = collection.FindAll().ToList();
                return result; // какой тип передовать по факту?
            }
        }

        public List<StatItem> GetByDirName(string dirName)
        {
            using (var db = new LiteDatabase(_filepath)) // открывает или создает бд
            {
                var collection = db.GetCollection<StatItem>("StatItems");  //получить коллекцию или создать 
                collection.EnsureIndex(x => x.DirName); // создает индекс если его еще нет
                var result = collection.Find(x => x.DirName == dirName).ToList();
                return result;
            }
        }

        public List<StatItem> GetByDirNameRec(string dirName)
        {
            using (var db = new LiteDatabase(_filepath)) // открывает или создает бд
            {
                var collection = db.GetCollection<StatItem>("StatItems");  //получить коллекцию или создать 
                collection.EnsureIndex(x => x.DirName); // создает индекс если его еще нет
                var result = collection.Find(x => x.DirName.StartsWith(dirName)).ToList();
                return result;
            }
        }
        public void AddOrUpdateAll(List<StatItem> data) // что возвращать при модификации данных? bool? какие данные лучше принимать
        {
            using (var db = new LiteDatabase(_filepath)) // открывает или создает бд
            {
                var collection = db.GetCollection<StatItem>("StatItems");  //получить коллекцию или создать 
                collection.Upsert(data); //делает вставку или обновление по полю с атрибутом BsonId
            }
        }
    }
}
