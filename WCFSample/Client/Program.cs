using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            MyService.IService1 proxy = new MyService.Service1Client("NetTcp");
            Console.WriteLine(proxy.MyOperation1("Srihari"));
            Console.ReadLine();
        }
    }
}