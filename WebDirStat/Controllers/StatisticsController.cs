using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebDirStat.Controllers
{
    [ApiController] //Контроллеры веб-API обычно используют атрибут зачем?
    [Route("api/statistics")] 
    public class StatisticsController : ControllerBase // обязательно ли наследовать класc, в чем отличие от класса Controller
    {
        [HttpPost] 
        public IEnumerable<string> Statistics([FromBody] int dirName)
        {
            return Array.Empty<string>();
        }
    }
}
