using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirStat.Dao.Impl
{
    public class FileDbStatItemDao : IStatItemDao // классы dao создаются по каждой сущности и по каждой бд? как описывать связи?
    {
        private readonly string _filepath;
        private string _content;

        public FileDbStatItemDao(string filepath)
        {
            _filepath = filepath;

        }
        public FileDbStatItemDao() : this("DirStat.txt")
        {
        }

        public void LoadDbContent()
        {
            _content = File.ReadAllText(_filepath);
        }

        public List<StatItem> GetAll() //что лучше "возвращать" в сигнатуре интерфейс или обьект?
        {
            if (_content == null) LoadDbContent();
            var res = new List<StatItem>(); 
            var blocks = _content.Split('?', StringSplitOptions.RemoveEmptyEntries);
            foreach (var block in blocks)
            {
                var items = ParseBlock(block);
                res.AddRange(items);
            }
            return res; //что лучше "возвращать" по факту?
        }

        public List<StatItem> GetByDirName(string dirName)
        {
            if (_content == null) LoadDbContent();
            var res = new List<StatItem>();
            var blocks = _content.Split('?', StringSplitOptions.RemoveEmptyEntries);
            foreach (var block in blocks)
            {
                if (block == dirName)
                {
                    var items = ParseBlock(block);
                    res.AddRange(items);
                }
            }
            return res;
        }
        public List<StatItem> GetByDirNameRec(string dirName) 
        {
            if (_content == null) LoadDbContent();
            var res = new List<StatItem>();
            var blocks = _content.Split('?', StringSplitOptions.RemoveEmptyEntries);
            foreach (var block in blocks)
            {
                if (block.Contains(dirName))
                {
                    var items = ParseBlock(block);
                    res.AddRange(items);
                }
            }
            return res;
        }

        public void AddAll(List<StatItem> data) //что лучше принимать в качестве параметра интерфейс, объект ?
        {
            if (_content == null) LoadDbContent();
            var result = new List<StatItem>();
            var dirs = data.Select(x => x.DirName).Distinct();
            using (var sw = new StreamWriter(_filepath))
            {
                foreach (var dir in dirs)
                {
                    sw.WriteLine($"?{dir}");
                    var dirItems = data.Where(x => x.DirName == dir).ToList();
                    foreach (var item in dirItems)
                    {
                        sw.WriteLine(item.ToString());
                    }
                }
            }
        }


        public void AddOrUpdateAll(List<StatItem> data)
        {
            if (_content == null) LoadDbContent();
            var result = new List<StatItem>();
            var dirs = data.Select(x => x.DirName).Distinct();
            foreach (var dir in dirs)
            {
                var inputFiles = data.Where(x => x.DirName == dir).ToList();
                var currFiles = GetByDirName(dir);
                if (currFiles.Count() > 0)
                {
                    result = currFiles.Except(inputFiles).ToList();
                    result.AddRange(inputFiles);
                    DeleteByDirName(dir);
                    AddAll(result);
                }
                else
                {
                    AddAll(inputFiles);
                }
            }

        }

        private void DeleteByDirName(string dirName)
        {
            var startIndex = _content.IndexOf(dirName);
            var endIndex = _content.IndexOf("?", startIndex)-1;  
            var count = endIndex - startIndex;  
            var tempContent = _content.Remove(startIndex,count);
            using (var sw = new StreamWriter(_filepath))
            {
                sw.Write(tempContent);
            }
            _content = tempContent; 
        }


        private List<StatItem> ParseBlock(string block)
        {
            var res = new List<StatItem>();   
            var blockLines = block.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            var blockName = blockLines[0];
            foreach (var line in blockLines.Skip(1))
            {
                res.Add(ParseBlockLine(blockName, line));
            }
            return res;
        }

        private StatItem ParseBlockLine(string blockName, string blockLine)
        {
            var fields = blockLine.Split(',');
            var statItem = new StatItem
            {
                DirName = blockName,
                FileName = fields[0],
                FullName = Path.Combine(blockName, fields[0]),
                Size = long.Parse(fields[1]),
                CreationTime = DateTime.Parse(fields[2]),
                RegTime = DateTime.Parse(fields[3]),
            };
            return statItem;
        }
    }
}
