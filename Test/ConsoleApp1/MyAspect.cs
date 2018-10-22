using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleApp1
{
    [Serializable]
    public class MyAspect : MethodInterceptionAspect
    {
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            Console.WriteLine("【拦截器：】，方法执行前拦截到的信息是：" + args.Arguments.First());//打印出拦截的方法第一个实参
            Console.WriteLine("执行前"+args.ReturnValue);

            args.Proceed();//Proceed()方法表示继续执行拦截的方法
            Console.WriteLine("修改前"+args.ReturnValue);
            object s = "abc";

           args.ReturnValue = s;

            Console.WriteLine("修改后"+args.ReturnValue);

            Console.WriteLine("【拦截器：】，方法已在成功{0}执行", DateTime.Now);//被拦截方法执行完成之后执行
        }
         
    }
   [Serializable]
    public class MyAspect3 : OnMethodBoundaryAspect
    {

        public override void OnEntry(MethodExecutionArgs args)
        {
            Console.WriteLine("方法执行前");
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            Console.WriteLine("方法执行后");
        }
    }
     

 

}
