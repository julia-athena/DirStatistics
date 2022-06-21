using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirStat.Service;
using DirStat.Service.Dao;

namespace DirStat.Dao.Implementation.LiteDb
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

        public IList<StatItem> GetAll()
        {
            using var db = new LiteDatabase(_filepath); // открывает или создает бд
            var collection = db.GetCollection<StatItem>("StatItems");  //получить коллекцию или создать 
            var result = collection.FindAll().ToList();
            return result; 
        }

        public IList<StatItem> GetByDirName(string dirName)
        {
            var db = new LiteDatabase(_filepath); // открывает или создает бд
            var collection = db.GetCollection<StatItem>("StatItems");  //получить коллекцию или создать 
            collection.EnsureIndex(x => x.DirName); // создает индекс если его еще нет
            var result = collection.Find(x => x.DirName == dirName).ToList();
            return result;
        }

        public IList<StatItem> GetByDirNameRec(string dirName)
        {
            using var db = new LiteDatabase(_filepath); // открывает или создает бд
            var collection = db.GetCollection<StatItem>("StatItems");  //получить коллекцию или создать 
            collection.EnsureIndex(x => x.DirName); // создает индекс если его еще нет
            var result = collection.Find(x => x.DirName.StartsWith(dirName)).ToList();
            return result;
        }
        public void AddOrUpdateAll(List<StatItem> data)
        {
            using var db = new LiteDatabase(_filepath); // открывает или создает бд
            var collection = db.GetCollection<StatItem>("StatItems");  //получить коллекцию или создать 
            collection.Upsert(data); //делает вставку или обновление по полю с атрибутом BsonId
        }
    }
}
