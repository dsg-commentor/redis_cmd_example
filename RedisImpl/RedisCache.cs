using System;
using StackExchange.Redis;
using ServiceInterfaces;

namespace RedisImpl
{
    public class RedisCache : ICache
    {
        private IDatabase _cache;
        private static string _connectionString;

        public RedisCache(string connectionString)
        {
            Console.WriteLine("Initializing Redis Cache");
            _connectionString = connectionString;
            _cache = lazyConnection.Value.GetDatabase();
        }
        public string Get(string key)
        {
            return _cache.StringGet(key);
        }

        public string Ping()
        {
            string cacheCommand = "PING";
            Console.WriteLine("\nCache command  : " + cacheCommand);
            return _cache.Execute(cacheCommand).ToString();
        }

        public void Set(string key, string value)
        {
            _cache.StringSet(key,value);
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(_connectionString);
        });

        private ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
