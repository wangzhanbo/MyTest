using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProtoBufDemo.AOP;
using protos.Login;

namespace ProtoBufDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        [HttpGet]
        //[MyAspect]
        [CustomInterceptor]
        public object Get()
        {
            List<ReqLogin> list = new List<ReqLogin>();
            for (int i = 0; i < 500; i++)
            {
                ReqLogin login = new ReqLogin();
                login.account = "wangzhanbo1231231231";
                login.password = 1234567890;
                list.Add(login);
            }
            
            return list;
        }

        [HttpGet("{id}")]
        public object Get(int id)
        {
            List<ReqLogin> list = new List<ReqLogin>();
            for (int i = 0; i < 500; i++)
            {
                ReqLogin login = new ReqLogin();
                login.account = "wangzhanbo1231231231";
                login.password = 1234567890;
                list.Add(login);
            }

            return list;
        }
    }
}