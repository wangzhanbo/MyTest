using System;
using System.Threading;
using Polly;
using Polly.Timeout;

namespace PollyDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //simple();
            //wrap();
            //Timeout();
            CircuitBreaker();

            Console.ReadLine();
        }

        public static void testmethod()
        {
            //ISyncPolicy policy = Policy.Handle<Exception>().r
        }

        public static void simple()
        {
            try
            {
                ISyncPolicy policy = Policy.Handle<ArgumentException>(ex => ex.Message == "年龄参数错误")
                    .Fallback(() =>
                    {
                        Console.WriteLine("出错了");
                    });
                policy.Execute(() =>
                {
                    //这里是可能会产生问题的业务系统代码
                    Console.WriteLine("开始任务");
                    throw new ArgumentException("年龄参数错误");
                    //throw new Exception("haha");
                    //Console.WriteLine("完成任务");
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"未处理异常:{ex}");
            }
        }
        public static void wrap()
        {
            //ISyncPolicy policy = Policy.Handle<Exception>().Retry(3).Fallback(() => { Console.WriteLine("执行出错"); });//这样不行
            ISyncPolicy policy = Policy.Handle<Exception>().Retry(3);
            policy.Wrap(Policy.Handle<Exception>().Fallback(() => { Console.WriteLine("执行出错"); }));

            policy.Execute(() =>
            {
                Console.WriteLine("开始任务");
                throw new ArgumentException("Hello world!");
                Console.WriteLine("完成任务");
            });
        }

        public static void CircuitBreaker()
        {
            ISyncPolicy policy = Policy.Handle<Exception>()
    .CircuitBreaker(6, TimeSpan.FromSeconds(5));//连续出错6次之后熔断5秒(不会再去尝试执行业务代码）。
            while (true)
            {
                Console.WriteLine("开始Execute");
                try
                {
                    policy.Execute(() =>
                    {
                        Console.WriteLine("开始任务");
                        throw new Exception("出错");
                        Console.WriteLine("完成任务");
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine("execute出错" + ex.GetType() + ":" + ex.Message);
                }
                Thread.Sleep(500);
            }
        }

        public static void Timeout()
        {
            ISyncPolicy policy = Policy.Handle<Exception>()
            .Fallback(() =>
            {
                Console.WriteLine("执行出错");
            });

            policy = policy.Wrap(Policy.Timeout(2, TimeoutStrategy.Pessimistic));


            policy.Execute(() =>
            {
                Console.WriteLine("开始任务");
                Thread.Sleep(5000);
                Console.WriteLine("完成任务");
            });
        }
    }
}
