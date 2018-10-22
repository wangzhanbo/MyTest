using AspectCore.DynamicProxy;
using System;
using System.Threading.Tasks;

namespace ConsoleAppAspect
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "asdf";
            Console.WriteLine($"{name}");
            ProxyGeneratorBuilder proxyGeneratorBuilder = new ProxyGeneratorBuilder();
            IProxyGenerator proxyGenerator = proxyGeneratorBuilder.Build();
            SampleInterface sampleInterface = proxyGenerator.CreateInterfaceProxy<SampleInterface, SampleClass>();
            Console.WriteLine(sampleInterface);
            int i = sampleInterface.Foo("myname");
            Console.WriteLine(i);
            Console.ReadKey();
        }
    }

    public class SampleInterceptor : AbstractInterceptorAttribute
    {
        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            Console.WriteLine("call interceptor");
            //            return
            context.Invoke(next);
            context.ReturnValue = 456;
            Console.WriteLine("after interceptor");

            return Task.CompletedTask;
        }
    }

    public class SampleClass : SampleInterface
    {


        int SampleInterface.Foo(string name)
        {
            Task.Delay(12);
            Console.WriteLine(name);
            return 123;
        }
    }

    public interface SampleInterface
    {
        [SampleInterceptor]

        int Foo(string name);
    }
}
