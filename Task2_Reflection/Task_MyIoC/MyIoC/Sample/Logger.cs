using System;
using MyIoC.Attributes;

namespace MyIoC.Sample
{
    [Export]
    public class Logger
    {
        public void Log()
        {
            Console.WriteLine("LOG");
        }
    }
}