using System.Runtime.Caching;

namespace CachingSolutionsSamples.Infrastructure.Abstractions
{
    public abstract class MemoryCacheBase<TCacheValue> : ICache<TCacheValue>
    {
        protected readonly ObjectCache Cache = MemoryCache.Default;
        protected readonly string Prefix;

        protected MemoryCacheBase(string prefix)
        {
            Prefix = prefix;
        }

        public virtual TCacheValue Get(string forUser, string identifier = "")
        {
            return (TCacheValue) Cache.Get(Prefix + identifier + forUser);
        }

        public virtual void Set(TCacheValue cacheValue, string forUser, string identifier = "")
        {
            Cache.Set(Prefix + identifier + forUser, cacheValue, ObjectCache.InfiniteAbsoluteExpiration);
        }
    }
}