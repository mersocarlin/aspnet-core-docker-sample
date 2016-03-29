using StackExchange.Redis;

namespace BankService.Data.Contexts
{
    public interface IRedisContext
    {
        IDatabase GetDatabase();
    }
}
