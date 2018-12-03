using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api1.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ServerController : Controller
    {
        // GET: api/Docker
        [HttpGet]
        public IActionResult Get()
        {
            return Content( Environment.MachineName);
        }


    }
}
