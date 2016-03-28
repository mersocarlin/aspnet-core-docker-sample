using BankService.Domain.Contracts;
using BankService.Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Collections.Generic;
using Microsoft.Extensions.OptionsModel;
using BankService.Data.Setup;

namespace BankService.Data.Repositories
{
    public class AccountHolderRepository : IAccountHolderRepository
    {
        private const string COLLECTION_NAME = "bank_data";

        private readonly MongoDatabase database;
        private readonly Settings settings;

        public AccountHolderRepository(IOptions<Settings> settings)
        {
            this.settings = settings.Value;
            this.database = this.CreateConnection();
        }

        private MongoDatabase CreateConnection()
        {
            var client = new MongoClient(settings.MongoConnection);
            var server = client.GetServer();
            var database = server.GetDatabase(settings.Database);

            return database;
        }

        public void Add(AccountHolder accountHolder)
        {
            this.database
                .GetCollection<AccountHolder>(COLLECTION_NAME)
                .Save(accountHolder);
        }

        public IEnumerable<AccountHolder> GetAll()
        {
            return this.database
                .GetCollection<AccountHolder>(COLLECTION_NAME)
                .Find(null)
                .SetLimit(100);
        }

        public AccountHolder GetById(ObjectId id)
        {
            var query = Query.EQ("_id", id);

            return this.database
                .GetCollection<AccountHolder>(COLLECTION_NAME)
                .FindOne(query);
        }

        public bool Remove(ObjectId id)
        {
            var query = Query.EQ("_id", id);

            var result = this.database
                .GetCollection<AccountHolder>(COLLECTION_NAME)
                .Remove(query);

            return this.GetById(id) == null;
        }

        public void Update(AccountHolder accountHolder)
        {
            var query = Query.EQ("_id", accountHolder.Id);

            var update = Update<AccountHolder>.Replace(accountHolder);

            this.database
                .GetCollection<AccountHolder>(COLLECTION_NAME)
                .Update(query, update);
        }
    }
}
