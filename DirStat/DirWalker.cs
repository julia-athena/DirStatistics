using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirStat
{
    public class DirWalker
    {
        private readonly string _path;
        public DirWalker(string path)
        {
            _path = path;
        }

        public List<FileStatItem> GetFilesRec()
        {
            return GetFilesRec(_path);   
        }
        public static List<FileStatItem> GetFilesRec(string dir)
        {
            var stack = new Stack<string>();
            var fileStatItems = new List<FileStatItem>();  
            stack.Push(dir);
            while (stack.Count > 0)
            {
                var currDir = stack.Pop();
                var subDirs = GetSubDirs(currDir);
                foreach (var subDir in subDirs)
                {
                    stack.Push(subDir);
                }
                var fileNames = GetFiles(currDir);
                foreach (var fileName in fileNames)
                {
                    var fileStatItem = GetFileStatItem(fileName);
                    fileStatItems.Add(fileStatItem);
                }
            }
            return fileStatItems;    
        }

        private static FileStatItem GetFileStatItem(string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            return new FileStatItem(fileInfo);
        }

        private static string[] GetSubDirs(string dir)
        {
            try
            {
                return Directory.GetDirectories(dir);
            }
            catch
            {
                return Array.Empty<string>();
            }
        }
        private static string[] GetFiles(string dir)
        {
            try
            {
                return Directory.GetFiles(dir);
            }
            catch
            {
                return Array.Empty<string>();
            }
        }
    }
}
