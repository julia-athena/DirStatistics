using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirStat
{
    public static class DirWalker
    {
        public static IEnumerable<string> GetFilesRec(string dir)
        {
            var stack = new Stack<string>();
            var dirFiles = new List<string>();  
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
                    dirFiles.Add(currFile);
                }
            }
            return dirFiles;    
        }

        public static string[] GetSubDirs(string dir)
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
        public static string[] GetFiles(string dir)
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
