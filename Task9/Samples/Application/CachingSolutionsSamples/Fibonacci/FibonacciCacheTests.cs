using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CachingSolutionsSamples.Fibonacci
{
    [TestClass]
    public class FibonacciCacheTests
    {
        [TestMethod]
        public void MemoryCache()
        {
            var calculator = new FibonacciCalculator(new FibonacciMemoryCache());

            for (var i = 10; i >= 1; i--)
            {
                Console.WriteLine(calculator.Calculate(i));
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void RedisCache()
        {
            var calculator = new FibonacciCalculator(new FibonacciRedisCache("localhost"));

            for (var i = 10; i >= 1; i--)
            {
                Console.WriteLine(calculator.Calculate(i));
                Thread.Sleep(100);
            }
        }
    }
}
