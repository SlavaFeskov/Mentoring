using System.Runtime.Caching;

namespace CachingSolutionsSamples.Infrastructure.Abstractions
{
    internal interface IPolicyCreator
    {
        CacheItemPolicy Create();
    }
}