using NetStandardCL;
using System;

namespace NetCoreConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {        
            var name = args.Length > 0 ? args[0] : string.Empty;
            Console.WriteLine($"Hello, {name}!");
            Console.WriteLine(Shared.GetMessage(name));          
        }
    }
}
