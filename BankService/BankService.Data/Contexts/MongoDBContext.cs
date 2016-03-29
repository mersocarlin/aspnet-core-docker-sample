using MongoDB.Driver;

namespace BankService.Data.Contexts
{
    public class MongoDBContext : IMongoDBContext
    {
        public MongoDBContext(string server, string port, string database)
        {
            this.Server = server;
            this.Port = port;
            this.Database = database;
        }

        public string Server { get; private set; }
        public string Port { get; set; }
        public string Database { get; private set; }

        public MongoDatabase CreateConnection()
        {
            var client = new MongoClient($"mongodb://{this.Server}:{this.Port}");
            var server = client.GetServer();
            var database = server.GetDatabase(this.Database);

            return database;
        }
    }
}
