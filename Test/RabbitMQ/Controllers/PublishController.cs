using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Models;

namespace RabbitMQ.Controllers
{
    public class PublishController : Controller
    {
        private readonly ICapPublisher _publisher;
        public PublishController(ICapPublisher publisher)
        {
            _publisher = publisher;
        }

        //[Route("~/checkAccountWithTrans")]
        //public async Task<string> PublishMessageWithTransactionAsync()
        //{

        //    await _publisher.PublishAsync("xxx.services.account.check", new Person { Name = "Foo", Age = 11 });// 你的业务代码。
        //    //await _publisher.PublishAsync("xxx.services.account.check", new Person { Name = "Foo", Age = 11 });// 你的业务代码。
        //    //var d = "";// GetString().Result + GetString2().Result;

        //    Console.WriteLine(DateTime.Now);

        //    //string all = GetString().Result + GetString2().Result;
        //    string a =await GetString();
        //    string b = await GetString2();

        //    //var a = GetString();
        //    //var b = GetString2();

        //    string all = a + b ;

        //    Console.WriteLine(DateTime.Now);

        //    return all;
        //    //return d;
        //}

        public async Task<string> GetString()
        {
            //System.Threading.Thread.Sleep(4000);
            await Task.Delay(3000);

            return "ad1";
        }
        public async Task<string> GetString2()
        {
            //System.Threading.Thread.Sleep(3000);
            await Task.Delay(3000);
            return "ad2";
        }



        //[NoAction]
        [CapSubscribe("xxx.services.account.check")]
        public async Task CheckReceivedMessage(Person person)
        {
            Console.WriteLine(person.Name);
            Console.WriteLine(person.Age);
            //throw new Exception("");
            //  return Task. ;
        }

        //[NoAction]
        [CapSubscribe("abc.services.account.check")]
        public async Task CheckReceivedMessage2(Person person)
        {
            Console.WriteLine(person.Name);
            Console.WriteLine(person.Age);
            //throw new Exception("");
            //  return Task. ;
        }
    }
}