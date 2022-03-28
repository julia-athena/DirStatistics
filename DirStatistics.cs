using System.Collections;
using DirStat.FileComparers;


namespace DirStat
{
    internal class DirStatistics
    {
        private string DirPath;
        public DirStatistics(string path)
        {
            DirPath = path;
        }
        public List<FileInfo> FilesSortedBySizeDesc()
        {
            var files = new List<FileInfo>();
            var paths = Directory.GetFiles(DirPath);
            foreach (var path in paths)
            {
                files.Add(new FileInfo(path));  
            }
            var comparer = new BySizeComparerDesc();
            files.Sort(comparer);
            return files;
        }
        public List<FileInfo> FilesSortedByCreateTime()
        {
            var files = new List<FileInfo>();
            var paths = Directory.GetFiles(DirPath);
            foreach (var path in paths)
            {
                files.Add(new FileInfo(path));
            }
            var comparer = new ByCreateTimeComparer();
            files.Sort(comparer);
            return files;
        }
        public List<KeyValuePair<string, int>> ExtensionAndFrequencyInfo()
        {
            var paths = Directory.GetFiles(DirPath);
            var extensionInfos = new Dictionary<string, int>();
            foreach (var path in paths)
            {
                var currExt = Path.GetExtension(path);
                if (!extensionInfos.ContainsKey(currExt))
                {
                    extensionInfos.Add(currExt, 1);
                }
                else
                {
                    extensionInfos[currExt] = extensionInfos[currExt] + 1;
                }
            }
            var res = extensionInfos.ToList();
            return res;
        }
    }
}
