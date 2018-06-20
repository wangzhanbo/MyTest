using System;
using System.Net;
using DnsClient;
namespace Clients
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                IDnsQuery dnsQuery = new LookupClient(IPAddress.Parse("127.0.0.1"), 8600);
                var result = dnsQuery.ResolveService("service.consul", "api1");
                Console.WriteLine(result[0].Port);
            }

            Console.ReadLine();
        }
    }
}
