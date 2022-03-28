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
            var extFrequency = new Dictionary<string, int>();
            foreach (var path in paths)
            {
                var currExt = Path.GetExtension(path);
                if (!extFrequency.ContainsKey(currExt))
                    extFrequency.Add(currExt, 1);
                else
                    extFrequency[currExt] = extFrequency[currExt] + 1;
            }
            var sortedList = extFrequency.ToList();
            sortedList.Sort(CompareByValueDesc);  
            return sortedList;
        }

        private int CompareByValueDesc(KeyValuePair<string, int> x, KeyValuePair<string, int> y)
        {
            if (x.Value < y.Value)
                return 1;
            else if (x.Value == y.Value)
                return 0;
            else
                return -1;
        }
    }
}
