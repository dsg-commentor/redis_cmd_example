using Redis_Example.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redis_Example.Implementations
{
    class InMemoryCaching : ICache
    {
        private Dictionary<string, string> _cache = new Dictionary<string, string>();

        public InMemoryCaching()
        {
            Console.WriteLine("Initializing Memory Cache");
        }
        public string Get(string key)
        {
            return _cache.GetValue(key,null);
        }

        public string Ping()
        {
            return "PONG";
        }

        public void Set(string key, string value)
        {
            _cache.Add(key, value);
        }
    }
}
