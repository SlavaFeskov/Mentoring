using System;
using System.Collections.Generic;
using CachingSolutionsSamples.Infrastructure.Abstractions;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Infrastructure.Categories
{
    internal class CategoriesRedisCache : RedisCacheBase<IEnumerable<Category>>
    {
        public CategoriesRedisCache(string hostName = "localhost", TimeSpan? cacheExpiry = null) : base(hostName,
            "Cache_Categories", cacheExpiry)
        {
        }
    }
}