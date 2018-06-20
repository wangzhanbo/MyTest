using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConsulDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //https://www.cnblogs.com/myzony/p/9168851.html#_label0_0
            services.AddMvc();
            //return services.AddAbp<AbpGrpcServerDemoModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            lifetime.ApplicationStarted.Register(OnStarted);
            lifetime.ApplicationStopped.Register(OnStoped);
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //app.UseAbp();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void OnStoped()
        {
            throw new NotImplementedException();
        }

        private void OnStarted()
        {
            var client = new ConsulClient();
            var httpCheck = new AgentServiceCheck()
            {
                //健康检查出错后，在consul去掉这个服务的时间
                DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
                
                //健康检查的频率
                Interval = TimeSpan.FromSeconds(10),

                //健康检查的地址
                HTTP = "http://localhost:60693/Health/check",

                //TCP =""
            };

            var register = new AgentServiceRegistration()
            {
                ID = "myservices",
                Check = httpCheck,
                Address = "localhost",
                Name = "myservices",
                Port = 60693
            };

            client.Agent.ServiceRegister(register).ConfigureAwait(false);
        }
    }
}
