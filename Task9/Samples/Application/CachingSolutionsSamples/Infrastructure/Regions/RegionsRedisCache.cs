using System;
using System.Collections.Generic;
using CachingSolutionsSamples.Infrastructure.Abstractions;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Infrastructure.Regions
{
    internal class RegionsRedisCache : RedisCacheBase<IEnumerable<Region>>
    {
        public RegionsRedisCache(string hostName = "localhost", TimeSpan? cacheExpiry = null) : base(hostName,
            "Cache_Regions", cacheExpiry)
        {
        }
    }
}