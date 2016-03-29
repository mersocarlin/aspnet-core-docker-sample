using StackExchange.Redis;

namespace BankService.Data.Contexts
{
    public class RedisContext : IRedisContext
    {
        public const int PORT = 6379;
        public const int KEY_TIMEOUT = 10;

        public RedisContext(string server, int port, int keyTimeout)
        {
            this.Server = server;
            this.Port = port == 0 ? PORT : port;
            this.KeyTimeout = keyTimeout == 0 ? KEY_TIMEOUT : keyTimeout;
        }

        public string Server { get; set; }
        public int Port { get; set; }
        public int KeyTimeout { get; set; }

        public IDatabase GetDatabase()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer
                .Connect($"{this.Server}:{this.Port},abortConnect=false");
            return redis.GetDatabase();
        }
    }
}
