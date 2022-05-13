using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirStat
{
//Всякий раз при добавлении в файл новой
//пары «ключ — значение» происходит также обновление хеш-карты для отражения
//в ней относительного адреса только что записанных данных(описанный механизм
//работает как при вставке новых ключей, так и при обновлении существующих). 
    public class HashDatabase
    {
        private readonly string FilePath;
        private readonly string HashPath;
        private readonly Dictionary<string, int> Hash;

        private HashDatabase(string path)
        {
            FilePath = path;
        }

        public static HashDatabase CheckAndInitialize(string path)
        {
            //проверяем файлы конфигурации - проверяем есть ли в директори файлы бд
            //если нет создаем в папке проекта файл бд и файл метаданных
            var filePath = Path.Combine(path, "Statistics.stat");
            return new HashDatabase(filePath);
        }

        public void CompactData()
        {

        }

        public void AddKeyValue(string key, int value)
        {

        }
    }
}
