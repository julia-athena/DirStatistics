﻿using DirStat;
using ConsoleTables;
using DirStat.FileComparers;
using System;

Console.WriteLine("Enter directory path to get statistics");
var path = Console.ReadLine();
var stat = new DirStatistics(path);