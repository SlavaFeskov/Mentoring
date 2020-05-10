using System.Collections.Generic;
using System.Runtime.Caching;
using CachingSolutionsSamples.Infrastructure.Abstractions;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Infrastructure.Categories
{
    internal class CategoriesMemoryCache : MemoryCacheBase<IEnumerable<Category>>
    {
        private readonly IPolicyCreator _policyCreator;

        public CategoriesMemoryCache(IPolicyCreator policyCreator) : base("Cache_Categories")
        {
            _policyCreator = policyCreator;
        }

        public override void Set(IEnumerable<Category> cacheValue, string forUser, string identifier = "")
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