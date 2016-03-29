using StackExchange.Redis;

namespace BankService.Data.Contexts
{
    public class RedisContext : IRedisContext
    {
        public RedisContext(string server, string port)
        {
            this.Server = server;
            this.Port = port;
        }

        public string Server { get; private set; }
        public string Port { get; private set; }

        public IDatabase GetDatabase()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect($"{this.Server}:{this.Port}");
            return redis.GetDatabase();
        }
    }
}
