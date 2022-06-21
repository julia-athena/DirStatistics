using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirStat.Service;
using DirStat.Service.Dao;

namespace DirStat.Dao.Implementation.FileDb
{
    public class FileDbStatItemDao : IStatItemDao 
    {
        private readonly string _filePath;
        private FileInfo _fileDb;

        public FileDbStatItemDao(string filePath)
        {
            _filePath = Path.GetFullPath(filePath);
            _fileDb = new FileInfo(_filePath);
            InitFileDb();
        }
        public FileDbStatItemDao() : this("FileDbStatItem.txt")
        {
        }

        private void InitFileDb()
        {
            if (!_fileDb.Exists)
                _fileDb.Create().Close();
        }

        public IList<StatItem> GetAll() 
        {
            var content = File.ReadAllText(_filePath);
            var result = new List<StatItem>(); 
            var blocks = content.Split('?', StringSplitOptions.RemoveEmptyEntries);
            foreach (var block in blocks)
            {
                var items = ParseBlock(block);
                result.AddRange(items);
            }
            return result;  
        }

        public IList<StatItem> GetByDirName(string dirName)
        {
            var content = File.ReadAllText(_filePath);
            var result = new List<StatItem>();
            var blocks = content.Split('?', StringSplitOptions.RemoveEmptyEntries);
            foreach (var block in blocks)
            {
                if (dirName == ParseBlockName(block))
                {
                    var items = ParseBlock(block);
                    result.AddRange(items);
                }
            }
            return result;
        }
        public IList<StatItem> GetByDirNameRec(string dirName) 
        {
            var content = File.ReadAllText(_filePath);
            var result = new List<StatItem>();
            var blocks = content.Split('?', StringSplitOptions.RemoveEmptyEntries);
            foreach (var block in blocks)
            {
                if (block.Contains(dirName))
                {
                    var items = ParseBlock(block);
                    result.AddRange(items);
                }
            }
            return result;
        }

        public void AddOrUpdateAll(List<StatItem> data)
        {
            var content = File.ReadAllText(_filePath);
            var result = new List<StatItem>();
            var dirs = data.Select(x => x.DirName).Distinct();
            foreach (var dir in dirs)
            {
                var inputFiles = data.Where(x => x.DirName == dir).ToList();
                var currFiles = GetByDirName(dir);
                if (currFiles.Count() > 0)
                {
                    result = currFiles.ExceptBy(inputFiles.Select(x => x.FullName), x => x.FullName).ToList();
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

        private void AddAll(List<StatItem> data) 
        {
            var content = File.ReadAllText(_filePath);
            var result = new List<StatItem>();
            var dirs = data.Select(x => x.DirName).Distinct();
            using (var sw = new StreamWriter(_filePath, true))
            {
                foreach (var dir in dirs)
                {
                    sw.WriteLine($"?{dir}");
                    var dirItems = data.Where(x => x.DirName == dir);
                    foreach (var item in dirItems)
                    {
                        sw.WriteLine(item.ToString());
                    }
                }
            }
        }

        private void DeleteByDirName(string dirName)
        {
            var content = File.ReadAllText(_filePath);
            var startIndex = content.IndexOf("?" + dirName+"\r\n");
            var endIndex = content.IndexOf("?", startIndex+1);
            if (endIndex == -1) endIndex = content.Length;
            var count = endIndex - startIndex;
            var tempContent = content.Remove(startIndex, count);
            using (var sw = new StreamWriter(_filePath))
            {
                sw.Write(tempContent);
            }
        }

        private string ParseBlockName(string block)
        {
            var blockLines = block.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            return blockLines[0];
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
