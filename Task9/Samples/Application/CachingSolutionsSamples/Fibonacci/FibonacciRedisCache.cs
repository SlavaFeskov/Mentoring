using CachingSolutionsSamples.Infrastructure.Abstractions;
using StackExchange.Redis;

namespace CachingSolutionsSamples.Fibonacci
{
    public class FibonacciRedisCache : ICache<long?>
    {
        private readonly ConnectionMultiplexer _redisConnection;
        private string _prefix = "Cache_Categories";

        public FibonacciRedisCache(string hostName)
        {
            _redisConnection = ConnectionMultiplexer.Connect(hostName);
        }

        public long? Get(string forUser, string identifier = "")
        {
            var db = _redisConnection.GetDatabase();
            return (long?) db.StringGet(_prefix + identifier + forUser);
        }

        public void Set(long? cacheValue, string forUser, string identifier = "")
        {
            var db = _redisConnection.GetDatabase();
            var key = _prefix + identifier + forUser;
            db.StringSet(key, cacheValue);
        }
    }
}