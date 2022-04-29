using DirStat;
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
        [HttpPost("items")]
        public string ListStatItems([FromBody] StatOptions options)
        {
            var items = new DirStatistics(options.Dir).GetStatItems();
            var res = new List<StatItem>();
            //todo где должна быть логика внизу
            if (options.Sort == "+CreationTime" || options.Sort == "CreationTime")
                res = items.OrderBy(x => x.CreationTime).ToList();
            else if (options.Sort == "-CreationTime")
                res = items.OrderByDescending(x => x.CreationTime).ToList();
            else if (options.Sort == "+Size" || options.Sort == "Size")
                res = items.OrderBy(x => x.Size).ToList();
            else if (options.Sort == "-Size")
                res = items.OrderByDescending(x => x.Size).ToList();
            else res = items;
            
            var top = options.Top <= 0 ? items.Count : options.Top;            
            res = res.GetRange(0, Math.Min(top, items.Count));
            return JsonConvert.SerializeObject(res); 
        }

        [HttpPost("extensions")]
        public IEnumerable<string> ListExtensions([FromBody] StatOptions options)
        {
            var stat = new DirStatistics(options.Dir);
            var res = stat.GetExtensions();
            return res;
        }

        [HttpPost("/test/ratings")]
        public IEnumerable<StatItem> ListAndSortStatItemsTest([FromForm] string name) // todo при передаче текста в теле name = null, c атр. [FromBody] - 415 ошибка
        {
            var stat = new DirStatistics(name);
            var res = stat.GetStatItems();
            return res;
        }
        [HttpPost("/test/mirror")]
        public async Task<string>  MirrorTextFromBody()
        {
            using var reader = new StreamReader(Request.Body); 
            return await reader.ReadToEndAsync(); //todo почему просто нельзя получить текст из тела?
        }  
    }
}
