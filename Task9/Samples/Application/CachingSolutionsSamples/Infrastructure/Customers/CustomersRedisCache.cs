using System;
using System.Collections.Generic;
using CachingSolutionsSamples.Infrastructure.Abstractions;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Infrastructure.Customers
{
    internal class CustomersRedisCache : RedisCacheBase<IEnumerable<Customer>>
    {
        public CustomersRedisCache(string hostName = "localhost", TimeSpan? cacheExpiry = null) : base(hostName,
            "Cache_Customer", cacheExpiry)
        {
        }
    }
}