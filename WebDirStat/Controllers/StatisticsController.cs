using DirStat;
using DirStat.FileComparers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebDirStat.Models;

namespace WebDirStat.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase 
    {
        [HttpPost("items")] 
        public IEnumerable<StatItem> ListStatItems([FromBody] StatOptions options)
        {
            var stat = new DirStatistics(options.Dir);
            var res = new List<StatItem>(); 
            if (options.Sort == "CreationDate")
            {
               res = stat.GetTopNStatItems(options.Top ?? 5, new ByCreateTimeComparer());
            }
            else if (options.Sort == "Size")
            {
               res = stat.GetTopNStatItems(options.Top ?? 5, new ByCreateTimeComparer());
            }
            return res; 
        }

        [HttpPost("extensions")]
        public IEnumerable<string> ListExtensions([FromBody] StatOptions options)
        {
            var stat = new DirStatistics(options.Dir);
            var res = stat.GetTopNExtensions(options.Top ?? 5);
            return res;
        }

        [HttpPost("/test/ratings")]
        public IEnumerable<StatItem> ListAndSortStatItemsTest([FromForm] string name) // null, [FromBody] - 415 ошибка при передаче текста в теле
        {
            var stat = new DirStatistics(name);
            var res = stat.GetStatItems(null);
            return res;
        }
        [HttpPost("/test/mirror")]
        public async Task<string>  MirrorTextFromBody() //есть ли более простой способ получить "просто текст" из тела?
        {
            using var reader = new StreamReader(Request.Body);
            return await reader.ReadToEndAsync();
        }  
    }
}
