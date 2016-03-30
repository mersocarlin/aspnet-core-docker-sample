using ServiceStack.Redis;

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

        public RedisClient GetClient()
        {
            var client = new RedisClient(this.Server, this.Port);
            return client;
        }
    }
}
