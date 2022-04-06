using DirStat;
using ConsoleTables;
using DirStat.FileComparers;

Console.WriteLine("Enter directory path to get statistics");
var path = Console.ReadLine();
var stat = new DirStatistics(path);

var files = stat.GetTopNFiles(3, new BySizeComparerDesc());
var table = new ConsoleTable("FileName", "Size");
for (int i = 0; i < files.Count; i++)
{
    table.AddRow(files[i].FullName, files[i].Size.ToString());
}
table.Write();
stat.FreshStatistics();
