using MongoDB.Driver;

namespace BankService.Data.Contexts
{
    public interface IMongoDBContext
    {
        string Server { get; set; }
        int Port { get; set; }
        string Database { get; set; }

        MongoDatabase CreateConnection();
    }
}
