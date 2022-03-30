using System.Collections;
using DirStat.FileComparers;


namespace DirStat
{
    internal class DirStatistics
    {
        private DirectoryInfo DirInfo;
        private List<FileStatItem> FileStatItems;
        private string StatFilePath;
        public DirStatistics(string path)
        {
            DirInfo = new DirectoryInfo(path);
            FileStatItems = new List<FileStatItem>();
            StatFilePath = "";
        }

        public List<FileStatItem> GetFileStatItems()
        {
            if (FileStatItems.Count == 0) {
                FillStatistics();
            }
            return FileStatItems;   
        }
        private void FillStatistics()
        {
            FillStatisticsFromFile();
            if (FileStatItems.Count == 0) {
                RecursiveWalkDir();
            }
        }
        private void FillStatisticsFromFile()
        {

        }
        private void RecursiveWalkDir()
        {

        }
    }
}
