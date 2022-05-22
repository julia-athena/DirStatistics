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
            _path = path ?? throw new ArgumentNullException();
        }

        public List<StatItem> GetFilesRec()
        {
            return GetFilesRec(_path);
        }
        public static List<StatItem> GetFilesRec(string dir)
        {
            var stack = new Stack<string>();
            var dirFiles = new List<StatItem>();  
            stack.Push(dir);
            while (stack.Count > 0)
            {
                var currDir = stack.Pop();
                var subDirs = GetSubDirs(currDir);
                foreach (var subDir in subDirs)
                {
                    stack.Push(subDir);
                }
                var currFiles = GetFiles(currDir);
                foreach (var currFile in currFiles)
                {
                    dirFiles.Add(new StatItem(new FileInfo(currFile)));
                }
            }
            return dirFiles;    
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
