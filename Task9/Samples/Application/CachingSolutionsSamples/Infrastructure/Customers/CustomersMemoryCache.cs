using System.Collections.Generic;
using System.Runtime.Caching;
using CachingSolutionsSamples.Infrastructure.Abstractions;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Infrastructure.Customers
{
    internal class CustomersMemoryCache : MemoryCacheBase<IEnumerable<Customer>>
    {
        private readonly IPolicyCreator _policyCreator;

        public CustomersMemoryCache(IPolicyCreator policyCreator) : base("Cache_Customers")
        {
            _policyCreator = policyCreator;
        }

        public override void Set(IEnumerable<Customer> cacheValue, string forUser, string identifier = "")
        {
            if (_policyCreator != null)
            {
                var policy = _policyCreator.Create();
                Cache.Set(Prefix + identifier + forUser, cacheValue, policy);
            }
            else
            {
                Cache.Set(Prefix + identifier + forUser, cacheValue, ObjectCache.InfiniteAbsoluteExpiration);
            }
        }
    }
}