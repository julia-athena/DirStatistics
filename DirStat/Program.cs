using DirStat;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

Console.WriteLine("Enter directory path to get statistics");
var path = Console.ReadLine();
var stat = new DirStatistics(path);





