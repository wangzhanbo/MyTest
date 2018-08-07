using System;
using System.Collections.Generic;
using System.Fabric;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using System.Net.Http;

//enable https
using System.Net;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace VotingWeb
{
    /// <summary>
    /// FabricRuntime 为每个服务类型实例创建此类的一个实例。 
    /// </summary>
    internal sealed class VotingWeb : StatelessService
    {
        public VotingWeb(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// 可选择性地替代以创建此服务实例的侦听器(如 TCP、http)。
        /// </summary>
        /// <returns>侦听器集合。</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[]
            {
        new ServiceInstanceListener(
            serviceContext =>
                new KestrelCommunicationListener(
                    serviceContext,
                    "ServiceEndpoint",
                    (url, listener) =>
                    {
                        ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

                        return new WebHostBuilder()
                            .UseKestrel()
                            .ConfigureServices(
                                services => services
                                    .AddSingleton<HttpClient>(new HttpClient())
                                    .AddSingleton<FabricClient>(new FabricClient())
                                    .AddSingleton<StatelessServiceContext>(serviceContext))
                            .UseContentRoot(Directory.GetCurrentDirectory())
                            .UseStartup<Startup>()
                            .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
                            .UseUrls(url)
                            .Build();
                    }))

//                    new ServiceInstanceListener(
//serviceContext =>
//    new KestrelCommunicationListener(
//        serviceContext,
//        "EndpointHttps",
//        (url, listener) =>
//        {
//            ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

//            return new WebHostBuilder()
//                .UseKestrel(opt =>
//                {
//                    int port = serviceContext.CodePackageActivationContext.GetEndpoint("EndpointHttps").Port;
//                    opt.Listen(IPAddress.IPv6Any, port, listenOptions =>
//                    {
//                        listenOptions.UseHttps(GetCertificateFromStore());
//                        listenOptions.NoDelay = true;
//                    });
//                })
//                .ConfigureAppConfiguration((builderContext, config) =>
//                {
//                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
//                })

//                .ConfigureServices(
//                    services => services
//                        .AddSingleton<HttpClient>(new HttpClient())
//                        .AddSingleton<FabricClient>(new FabricClient())
//                        .AddSingleton<StatelessServiceContext>(serviceContext))
//                .UseContentRoot(Directory.GetCurrentDirectory())
//                .UseStartup<Startup>()
//                .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
//                .UseUrls(url)
//                .Build();
//        }))
            };
        }

        private X509Certificate2 GetCertificateFromStore()
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            try
            {
                store.Open(OpenFlags.ReadOnly);
                var certCollection = store.Certificates;
                var currentCerts = certCollection.Find(X509FindType.FindBySubjectDistinguishedName, "CN=<your_CN_value>", false);
                return currentCerts.Count == 0 ? null : currentCerts[0];
            }
            finally
            {
                store.Close();
            }
        }

        internal static Uri GetVotingDataServiceName(ServiceContext context)
        {
            return new Uri($"{context.CodePackageActivationContext.ApplicationName}/VotingData");
        }

        //protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        //{
        //    return new ServiceInstanceListener[]
        //    {
        //        new ServiceInstanceListener(serviceContext =>
        //            new KestrelCommunicationListener(serviceContext, "ServiceEndpoint", (url, listener) =>
        //            {
        //                ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

        //                return new WebHostBuilder()
        //                            .UseKestrel()
        //                            .ConfigureServices(
        //                                services => services
        //                                    .AddSingleton<StatelessServiceContext>(serviceContext))
        //                            .UseContentRoot(Directory.GetCurrentDirectory())
        //                            .UseStartup<Startup>()
        //                            .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
        //                            .UseUrls(url)
        //                            .Build();
        //            }))
        //    };
        //}
    }
}
