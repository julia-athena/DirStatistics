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
            return "";
        }

        [HttpPost("extensions")]
        public string ListExtensions([FromBody] StatOptions options)
        {
            return "";
        }

    }
}
