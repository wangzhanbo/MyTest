using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ConsulDemo.Models;
using Abp.AspNetCore.Mvc.Controllers;

namespace ConsulDemo.Controllers
{
    public class HealthController : Controller
    {
        public IActionResult Check() => Ok("OJBK");

    }
}
