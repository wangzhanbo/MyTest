using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            MyClass myClass = new MyClass();
            object i = myClass.MyMehtod("hello,china");
            Console.WriteLine(i);
            Console.ReadKey();
        }
    }
}
