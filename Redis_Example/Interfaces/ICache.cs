using System;
using System.Collections.Generic;
using System.Text;

namespace Redis_Example.Interfaces
{
    interface ICache
    {
        string Get(string key);
        void Set(string key, string value);
        string Ping();
    }
}
