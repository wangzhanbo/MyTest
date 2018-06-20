using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSwaggerDemo.Controllers
{
    [Route("api/[controller]")]
    public class DefaultController : Controller
    {
        [HttpGet]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            
            return Ok(id);
        }

    }
}