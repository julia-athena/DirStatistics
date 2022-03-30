using DirStat;
using ConsoleTables;

Console.WriteLine("Enter directory path to get statistics");
var path = Console.ReadLine();
var stat = new DirStatistics(path);
