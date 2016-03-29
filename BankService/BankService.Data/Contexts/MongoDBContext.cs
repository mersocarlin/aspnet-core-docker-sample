using MongoDB.Driver;

namespace BankService.Data.Contexts
{
    public class MongoDBContext : IMongoDBContext
    {
        public const int PORT = 27017;

        public MongoDBContext(string server, int port, string database)
        {
            this.Server = server;
            this.Port = port == 0 ? PORT : port;
            this.Database = database;
        }

        public string Server { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }

        public MongoDatabase CreateConnection()
        {
            var client = new MongoClient($"mongodb://{this.Server}:{this.Port}");
            var server = client.GetServer();
            var database = server.GetDatabase(this.Database);

            return database;
        }
    }
}
