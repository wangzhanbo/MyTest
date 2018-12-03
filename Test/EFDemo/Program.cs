using EFDemo.Models;
using System;

namespace EFDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new Models.BloggingContext())
            {
                db.Set<Blog>().Add(new Blog() { Url = "ab" });
                db.SaveChanges();
            }

            Console.ReadKey();
            Console.WriteLine("Hello World!");
        }
    }
}
