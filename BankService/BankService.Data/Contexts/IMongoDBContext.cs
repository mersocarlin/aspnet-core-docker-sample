using MongoDB.Driver;

namespace BankService.Data.Contexts
{
    public interface IMongoDBContext
    {
        MongoDatabase CreateConnection();
    }
}
