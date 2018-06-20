using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace CommandLineSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new Dictionary<string,string>();
            settings.Add("name", "wangzhanbo");
            settings.Add("age", "12");


            //如果在运行 dotnet run name=ww age=22就会覆盖settings
            //因为AddCommandLine在最后添加，会覆盖AddInMemoryCollection，
            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .AddCommandLine(args);

            var config = builder.Build();
            

            Console.WriteLine(config["name"]);
            Console.WriteLine(config["age"]);
        }
    }
}
