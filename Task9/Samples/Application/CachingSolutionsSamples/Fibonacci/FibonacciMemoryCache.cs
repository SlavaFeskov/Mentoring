using CachingSolutionsSamples.Infrastructure.Abstractions;

namespace CachingSolutionsSamples.Fibonacci
{
    internal class FibonacciMemoryCache : MemoryCacheBase<long?>
    {
        public FibonacciMemoryCache() : base("Cache_Fibonacci")
        {
        }
    }
}