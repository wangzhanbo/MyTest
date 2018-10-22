using Consul;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace ConsulDiscovery
{
    public static class Extentention
    {
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
        {
            var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>() ??
               throw new ArgumentException("Missing Dependency", nameof(IConfiguration));

            var applicationLifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>() ??
               throw new ArgumentException("Missing Dependency", nameof(IApplicationLifetime));

            Action action = () =>
            {
                string URLS = configuration.GetSection("URLS").Value;
                Console.WriteLine($"URLS:{URLS}");

                string heathCheckUrl = Path.Combine(URLS, "HeathCheck");
                Console.WriteLine($"heathCheckUrl:{heathCheckUrl}");

                string applicationName = configuration.GetSection("applicationName").Value;
                Console.WriteLine($"applicationName:{applicationName}");

                Uri uri = new Uri(heathCheckUrl);
                

                //var client = new ConsulClient();
                var client = new ConsulClient((c) => { c.Address = new Uri("http://11.11.164.197:8500"); c.Token = "xxxhelloworldxxx"; });
                //client.ACL.
                //client.Config = consulClientConfiguration;

                var httpCheck = new AgentServiceCheck()
                {
                    //健康检查出错后，在consul去掉这个服务的时间
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(1),

                    //健康检查的频率
                    Interval = TimeSpan.FromSeconds(1),

                    //健康检查的地址
                    HTTP = heathCheckUrl,

                    //TCP =""
                };

                var register = new AgentServiceRegistration()
                {
                    ID = heathCheckUrl,
                    Address = uri.Host,
                    Port = uri.Port,
                    Check = httpCheck,
                    Name = applicationName,
                    //Name = "api1",
                };
                
                client.Agent.ServiceRegister(register).ConfigureAwait(false);

                var p = new KVPair("var/config/constr") { Flags = 42, Value =  Encoding.UTF8.GetBytes("test") };
                
                var putResponse =  client.KV.Put(p);

            };

            applicationLifetime.ApplicationStarted.Register(action);
            return app;
        }
        private static string GetUrlIP()
        {
            //服务器的IP地址 + ads.ToString();
            IPAddress ads = Dns.GetHostAddresses(Environment.MachineName)[0];

            ////获取客户端ip地址您的ip地址是：" + strip;
            //string strip = Request.UserHostAddress.ToString();

            return ads.ToString();
        }
    }

    [Route("HeathCheck")]
    public class HeathCheckController : Controller
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
