using DirStat;
using ConsoleTables;
using DirStat.FileComparers;

Console.WriteLine("Enter directory path to get statistics");
var path = Console.ReadLine();
var stat = new DirStatistics(path);

var topbig = stat.GetTopNFiles(3, new BySizeComparerDesc());
var p = Console.ReadLine();
stat.FreshStatistics();
