using System;
using IOCContainer.Attributes;

namespace IOCContainer.Sample
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