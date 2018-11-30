using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace InMemoryImpl
{
    public class InMemoryCaching : ICache
    {
        private Dictionary<string, string> _cache = new Dictionary<string, string>();

        public InMemoryCaching()
        {
            Console.WriteLine("Initializing Memory Cache");
        }
        public string Get(string key)
        {
            _cache.TryGetValue(key,out string value);
            return value;
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
