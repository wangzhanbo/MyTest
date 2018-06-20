using System;
using Microsoft.Extensions.Configuration;

namespace JsonFile
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("my.json",true,true);

            var config = builder.Build();

            //Console.WriteLine(config["name"]);
            //Console.WriteLine(config["age"]);
            //Console.WriteLine(config["books:0:title"]);
            //Console.WriteLine(config["books:0:publish"]);
            //Console.WriteLine(config["books:1:title"]);
            //Console.WriteLine(config["books:1:publish"]);

            var cls = new Class1();
            config.Bind(cls);
            
            Console.WriteLine(cls.name);
            Console.WriteLine(cls.age);
            Console.WriteLine(cls.id);
        }
    }
}
