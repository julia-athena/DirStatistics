using DirStat;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using DirStat.Dao.Impl;



string path = @"C:\Users\yvgva\source\repos\DirStat\stat.txt";
var dao = new FileDbStatItemDao(path);
var items = dao.GetAll();
dao.AddOrUpdateAll(items);
items = dao.GetAll();   

Console.ReadLine();



