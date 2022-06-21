using DirStat.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebDirStat.Models;
using Newtonsoft.Json;

namespace WebDirStat.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase 
    {
        [HttpPost("files/old")]
        public IEnumerable<StatItem> GetOldFiles(StatOptions options) // какой тип возвращать
        {
            var dirStat = new DirStatisticsServer(options.DirectoryName);
            dirStat.FreshDataForStatistics();
            var files = dirStat.GetTopOldFiles(options.Limit);
            return files;
        }
        [HttpPost("files/big")]
        public IEnumerable<StatItem> GetBigFiles(StatOptions options)
        {
            var dirStat = new DirStatisticsServer(options.DirectoryName);
            dirStat.FreshDataForStatistics();
            var files = dirStat.GetTopBigFiles(options.Limit);
            return files;
        }

        [HttpPost("extensions/info")]
        public IEnumerable<ExtensionInfo> GetExtensions(ExtensionInfoOptions options) 
        {
            var dirStat = new DirStatisticsServer(options.DirectoryName);
            dirStat.FreshDataForStatistics();
            var extensions = dirStat.GetTopExtensions(options.Limit);
            return extensions;
        }

    }
}
