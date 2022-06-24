using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirStat.Models;

namespace DirStat.Providers
{
    public class DirWalkerProvider
    {
        private readonly string _path;   
        public DirWalkerProvider(string path)
        {
            _path = path ?? throw new ArgumentNullException(nameof(path));
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
                var statItems = currFiles.Select(x => new StatItem(new FileInfo(x)));
                dirFiles.AddRange(statItems);
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
