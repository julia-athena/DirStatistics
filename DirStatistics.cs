using System.Collections;
using DirStat.FileComparers;


namespace DirStat
{
    internal class DirStatistics
    {
        private DirectoryInfo DirInfo;
        private List<string> DirFiles;
        private List<StatItem> StatItems;
        private string StatFilePath;
        public DirStatistics(string path)
        {
            DirInfo = new DirectoryInfo(path);
            StatItems = new List<StatItem>();
            DirFiles = new List<string>(); 
            StatFilePath = "";
        } 

        public List<StatItem> GetTopNFiles(int n, IComparer<StatItem> comparer)
        {
            if (StatItems.Count == 0)
                GetStatItemsFromFile();
            if (StatItems.Count == 0)
                FreshStatistics();
            if (StatItems.Count > 0)
                StatItems.Sort(comparer); // TODO постоянно будет сортироваться, как сохранить результат сортировки 
            return StatItems;
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

        }

        private void WriteStatItemsToFile()
        {

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
