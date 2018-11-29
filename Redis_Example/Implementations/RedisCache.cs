using Microsoft.Extensions.Configuration;
using Redis_Example.Interfaces;
using StackExchange.Redis;
using System;

namespace Redis_Example.Implementations
{
    class RedisCache : ICache
    {
        private IDatabase _cache;
        private static IConfiguration _configuration;

        public RedisCache(IConfiguration configuration)
        {
            Console.WriteLine("Initializing Redis Cache");
            _configuration = configuration;
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
            string cacheConnection = _configuration.GetConnectionString("redis");
            return ConnectionMultiplexer.Connect(cacheConnection);
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
