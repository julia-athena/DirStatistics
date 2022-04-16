using Xunit;
using DirStat;
using DirStat.FileComparers;

namespace DirStat.Tests
{
    public class DirStatTests
    {
        [Fact]
        public void GetTop5BigFiles()
        {
            var dirStat = GenerateTestDir(10);
            var comparer = new BySizeComparerDesc();
            var res = dirStat.GetTopNFiles(5, comparer);
        }
        public void GetTop5OldFiles()
        {
            var dirStat = GenerateTestDir(10);
            var comparer = new ByCreateTimeComparer();
            var res = dirStat.GetTopNFiles(5, comparer);
        }


        private DirStatistics GenerateTestDir(int fileQtty)
        {
            var testDir = @"test\";
            var dirInfo = Directory.CreateDirectory(testDir);
            var dirName = testDir;
            for (int i = 0; i < fileQtty; i++)
            {
                GenerateFile(testDir, i);
            }
            var dirStat = new DirStatistics(testDir);
            return dirStat;
        }

        private void GenerateFile(string dir, int fileNum)
        {
            string path = Path.Combine(dir, fileNum.ToString());    
            using (StreamWriter writer = new StreamWriter(path, false))
            {
            }
        }

    }
}