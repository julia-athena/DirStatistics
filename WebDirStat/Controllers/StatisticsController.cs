using DirStat;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WebDirStat.Controllers
{
    [ApiController] //Контроллеры веб-API обычно используют атрибут зачем?
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase // обязательно ли наследовать класc, в чем отличие от класса Controller
    {
        [HttpPost("data")] 
        public IEnumerable<StatItem> ListStatItems([FromBody] string name) //почему не работает с атрибутом [FromBody] - 415 ошибка
        {
            var stat = new DirStatistics(name);
            var res = stat.GetStatItems(null);
            return res; 
        }
        [HttpPost("test/ratings")]
        public IEnumerable<StatItem> ListAndSortStatItems([FromForm] string name) // null 
        {
            var stat = new DirStatistics(name);
            var res = stat.GetStatItems(null);
            return res;
        }
        [HttpPost("test/ratings1")]
        public IEnumerable<StatItem> ListAndSortStatItems1(string name) // null 
        {
            var stat = new DirStatistics(name);
            var res = stat.GetStatItems(null);
            return res;
        }
        [HttpPost("mirror")]
        public async Task<string>  MirrorTextFromBody() //есть ли более простой способ получить "просто текст" из тела?
        {
            using var reader = new StreamReader(Request.Body);
            return await reader.ReadToEndAsync();
        }
    }
}
