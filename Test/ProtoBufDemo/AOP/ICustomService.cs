using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtoBufDemo.AOP
{

    [ServiceInterceptor(typeof(CustomInterceptorAttribute))]
    public interface ICustomService
    {
        int Call(string name);
        
    }
}
