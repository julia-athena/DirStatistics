using DirStat.Service;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using DirStat.Dao.Implementation.FileDb;



string path = @"C:\Users\yvgva\source\repos\DirStat\stat.txt";
var dao = new FileDbStatItemDao(path);
var items = dao.GetAll();
Console.ReadLine();



