using System.Collections;
using DirStat.FileComparers;


namespace DirStat
{
    internal class DirStatistics
    {
        private DirectoryInfo DirInfo;
        private List<string> DirFiles;
        private List<StatItem> StatItems;
        private string WorkPath = "";
        private string FileName = "";
        public DirStatistics(string dirPath, string workPath): this(dirPath)
        {
            WorkPath = workPath;
        }
        public DirStatistics(string dirPath)
        {
            DirInfo = new DirectoryInfo(dirPath);
            StatItems = new List<StatItem>();
            DirFiles = new List<string>();
        }


        public List<StatItem> GetTopNFiles(int n, IComparer<StatItem> comparer)
        {
            if (StatItems.Count == 0)
                GetStatItemsFromFile();
            if (StatItems.Count == 0)
                FreshStatistics();
            if (StatItems.Count > 0)
                StatItems.Sort(comparer);
            return StatItems.GetRange(1,n);
        }
        public void FreshStatistics()
        {
            DirFiles.Clear();
            StatItems.Clear();  
            WalkDir();
            foreach (var fileName in DirFiles)
            {
                var file = new FileInfo(fileName);  
                StatItems.Add(new StatItem { CreationTime = file.CreationTime, FullName = file.FullName, Size = file.Length});
            }
            WriteStatItemsToFile();
        }

        private void GetStatItemsFromFile()
        {
            using (StreamReader reader = new StreamReader(FileName))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    StatItems.Add(new StatItem(line));
                }
            }
        }

        private void WriteStatItemsToFile()
        {
            StatItems.Sort();
            using (StreamWriter writer = new StreamWriter(FileName, false))
            {
                foreach (var item in StatItems)
                {
                    writer.WriteLine(item.ToString());
                }
            }
        }

        private void WalkDir()
        {
            var stack = new Stack<string>();
            stack.Push(DirInfo.FullName);
            while(stack.Count > 0)
            {
                var currDir = stack.Pop();
                var subDirs = GetSubDirs(currDir); 
                foreach(var subDir in subDirs)
                {
                    stack.Push(subDir);
                }
                var currFiles = GetFiles(currDir);    
                foreach(var currFile in currFiles)
                {
                    DirFiles.Add(currFile);
                }
            }
        }
        private string[] GetSubDirs(string dir)
        {
            try
            {
                return Directory.GetDirectories(dir);
            }
            catch
            {
                return new string[0];
            }
        }
        private string[] GetFiles(string dir)
        {
            try
            {
                return Directory.GetFiles(dir);
            }
            catch
            {
                return new string[0];
            }
        }
    }
}
