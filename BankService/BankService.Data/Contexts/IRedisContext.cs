using StackExchange.Redis;

namespace BankService.Data.Contexts
{
    public interface IRedisContext
    {
        string Server { get; set; }
        int Port { get; set; }
        int KeyTimeout { get; set; }

        IDatabase GetDatabase();
    }
}
