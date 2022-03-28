using DirStat;
using ConsoleTables;

Console.WriteLine("Enter directory path to get statistics");
var path = Console.ReadLine();
var stat = new DirStatistics(path);
ShowTop10BigFiles(stat);
ShowTop10OldFiles(stat);
ShowTop10Extensions(stat);

void ShowTop10BigFiles(DirStatistics stat)
{
    var files = stat.FilesSortedBySizeDesc();
    var table = new ConsoleTable("FileName","Size");
    for(int i = 0; i < files.Count && i < 10; i++)
    {
        table.AddRow(files[i].Name, files[i].Length);
    }
    table.Write();
}
void ShowTop10OldFiles(DirStatistics stat)
{
    var files = stat.FilesSortedByCreateTime();
    var table = new ConsoleTable("FileName", "CreationTime");
    for (int i = 0; i < files.Count && i < 10; i++)
    {
        table.AddRow(files[i].Name, files[i].CreationTime.ToString());
    }
    table.Write();
}

void ShowTop10Extensions(DirStatistics stat)
{
    var data = stat.ExtensionAndFrequencyInfo();
    data.Sort();
    var table = new ConsoleTable("Extension", "Frequency");
    for (int i = 0; i < data.Count && i < 10; i++)
    {
        table.AddRow(data[i].Key, data[i].Value.ToString());
    }
    table.Write();
}
