using System;
using System.IO;
using System.Runtime.Serialization;
using StackExchange.Redis;

namespace CachingSolutionsSamples.Infrastructure.Abstractions
{
    public abstract class RedisCacheBase<TCacheValue> : ICache<TCacheValue>
    {
        private readonly ConnectionMultiplexer _redisConnection;
        private readonly string _prefix;
        private readonly TimeSpan? _cacheExpiry;

        private readonly DataContractSerializer _serializer = new DataContractSerializer(
            typeof(TCacheValue));

        protected RedisCacheBase(string hostName, string prefix, TimeSpan? cacheExpiry)
        {
            _prefix = prefix;
            _cacheExpiry = cacheExpiry;
            _redisConnection = ConnectionMultiplexer.Connect(hostName);
        }

        public TCacheValue Get(string forUser, string identifier = "")
        {
            var db = _redisConnection.GetDatabase();
            byte[] s = db.StringGet(GetKey(forUser, identifier));
            if (s == null)
                return default;

            return (TCacheValue) _serializer
                .ReadObject(new MemoryStream(s));
        }

        public void Set(TCacheValue cacheValue, string forUser, string identifier = "")
        {
            var db = _redisConnection.GetDatabase();
            var key = GetKey(forUser, identifier);

            if (cacheValue == null)
            {
                db.StringSet(key, RedisValue.Null, _cacheExpiry);
            }
            else
            {
                var stream = new MemoryStream();
                _serializer.WriteObject(stream, cacheValue);
                db.StringSet(key, stream.ToArray(), _cacheExpiry);
            }
        }

        public void ClearCache(string forUser, string identifier = "")
        {
            var db = _redisConnection.GetDatabase();
            db.KeyDelete(GetKey(forUser, identifier));
        }

        private string GetKey(string forUser, string identifier = "")
        {
            return _prefix + identifier + forUser;
        }
    }
}