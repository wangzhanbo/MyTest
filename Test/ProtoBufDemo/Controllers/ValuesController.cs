using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProtoBufDemo.AOP;
using protos.Login;

namespace ProtoBufDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICustomService _customService;
        public ValuesController(IHttpClientFactory httpClientFactory, ICustomService customService)
        {
            _httpClientFactory = httpClientFactory;
            _customService = customService;
        }
        // GET api/values
        //[HttpGet]
        //[MyAspect]
        //public IActionResult Get()
        //{
        //    ReqLogin login = new ReqLogin();
        //    login.account = "w";
        //    login.password =1;
        //    MemoryStream stream = new MemoryStream();
        //    ProtoBuf.Serializer.Serialize(stream, login);
        //    var data = stream.ToArray();
        //    //_customService.Call("myname instance");
        //    return new FileContentResult(data, "application/octet-stream");

        //    //return data;
        //}
        [HttpGet]
        
        public object Get()
        {
            ReqLogin login = new ReqLogin();
            login.account = "wangzhanbo1231231231";
            login.password = 1234567890;
            return login;
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            try
            {
                var _httpclient = _httpClientFactory.CreateClient();
                var temp = _httpclient.GetAsync("http://localhost:5001/api/Tests").Result;
                var temp2 = _httpclient.GetAsync("http://localhost:5001/api/Tests/1").Result;

                int counts = 1;

                Stopwatch stopWatch = new Stopwatch();
                
                
                stopWatch.Start();

                for (int i = 0; i < counts; i++)
                {
                    var response = _httpclient.GetAsync("http://localhost:5001/api/Tests").Result;

                    var data = response.Content.ReadAsByteArrayAsync().Result;

                    MemoryStream stream = new MemoryStream(data);
                    var a = ProtoBuf.Serializer.Deserialize<ReqLogin>(stream);
                    stream.Dispose();
                }
                stopWatch.Stop();
                
                Stopwatch stopWatch2 = new Stopwatch();
                stopWatch2.Start();
                for (int i = 0; i < counts; i++)
                {
                    var response = _httpclient.GetAsync("http://localhost:5001/api/Tests/1").Result;
                    var a = response.Content.ReadAsByteArrayAsync().Result;
                }
                

                stopWatch2.Stop();
                

                return $"proto:{stopWatch.ElapsedMilliseconds}--normal:{stopWatch2.ElapsedMilliseconds}";

                //_httpclient.GetAsync<ReqLogin>("https://localhost:5001/api/values");
                //return a;
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class ProtoBuffersMessageDecoder
    {

    }

}
