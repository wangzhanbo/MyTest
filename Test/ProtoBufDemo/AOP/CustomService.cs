using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtoBufDemo.AOP
{
    public class CustomService : ICustomService
    {
        [CustomInterceptor]

        public int Call(string name)
        {
            Console.WriteLine($"service calling... {name}");
            return 123;
        }

         
    }
}
