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
            var res = new List<FileStatItem>();
            var items = new DirStatistics(options.Dir).GetStatItems();
            //todo где должна быть логика внизу
            if (options.Sort == "+creationTime" || options.Sort == "creationTime")
                res = items.OrderBy(x => x.CreationTime).ToList();
            else if (options.Sort == "-creationTime")
                res = items.OrderByDescending(x => x.CreationTime).ToList();
            else if (options.Sort == "+size" || options.Sort == "size")
                res = items.OrderBy(x => x.Size).ToList();
            else if (options.Sort == "-size")
                res = items.OrderByDescending(x => x.Size).ToList();
            else res = items;
            
            var top = options.Top <= 0 ? items.Count : options.Top;            
            res = res.GetRange(0, Math.Min(top, items.Count));
            return JsonConvert.SerializeObject(res); 
        }

        [HttpPost("extensions")]
        public string ListExtensions([FromBody] StatOptions options)
        {
            var res = new List<ExtensionStatItem>();
            var items = new DirStatistics(options.Dir).GetExtensions();         
            if (options.Sort == "+freguency" || options.Sort == "freguency")
                res = items.OrderBy(x => x.Frequency).ToList();
            else if (options.Sort == "-freguency")
                res = items.OrderByDescending(x => x.Frequency).ToList();
            return JsonConvert.SerializeObject(res);

        }

        [HttpPost("/test/ratings")]
        public IEnumerable<FileStatItem> ListAndSortStatItemsTest([FromForm] string name) // todo при передаче текста в теле name = null, c атр. [FromBody] - 415 ошибка
        {
            var stat = new DirStatistics(name);
            var res = stat.GetStatItems();
            return res;
        }
        [HttpPost("/test/mirror")]
        public async Task<string>  MirrorTextFromBody()
        {
            using var reader = new StreamReader(Request.Body); 
            return await reader.ReadToEndAsync(); 
        }  
    }
}
