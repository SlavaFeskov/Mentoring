using System.Collections.Generic;
using System.Runtime.Caching;
using CachingSolutionsSamples.Infrastructure.Abstractions;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Infrastructure.Regions
{
    internal class RegionsMemoryCache : MemoryCacheBase<IEnumerable<Region>>
    {
        private readonly IPolicyCreator _policyCreator;

        public RegionsMemoryCache(IPolicyCreator policyCreator) : base("Cache_Regions")
        {
            _policyCreator = policyCreator;
        }

        public override void Set(IEnumerable<Region> cacheValue, string forUser, string identifier = "")
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