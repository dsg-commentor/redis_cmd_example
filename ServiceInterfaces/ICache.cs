
namespace ServiceInterfaces
{
    public interface ICache
    {
        string Get(string key);
        void Set(string key, string value);
        string Ping();
    }
}
