using BankService.Data.Contexts;
using BankService.Domain.Contracts;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Collections.Generic;

namespace BankService.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected MongoDatabase database;
        protected IMongoDBContext context;
        protected string collectionName;

        public Repository(IMongoDBContext context, string collectionName)
        {
            this.context = context;
            this.database = this.context.CreateConnection();
            this.collectionName = collectionName;
        }

        public IEnumerable<TEntity> Get()
        {
            return this.GetWithLimit(int.MaxValue);
        }

        public IEnumerable<TEntity> GetWithLimit(int limit)
        {
            return this.database
               .GetCollection<TEntity>(collectionName)
               .Find(null)
               .SetLimit(limit);
        }

        public virtual TEntity GetById(int id)
        {
            return this.GetById(id.ToString());
        }

        public virtual TEntity GetById(string id)
        {
            var query = Query.EQ("_id", new ObjectId(id));

            return this.database
                .GetCollection<TEntity>(collectionName)
                .FindOne(query);
        }

        public void Create(TEntity entity)
        {
            this.database
                .GetCollection<TEntity>(collectionName)
                .Save(entity);
        }

        public void Update(TEntity entity, int id)
        {
            this.Update(entity, id.ToString());
        }

        public void Update(TEntity entity, string id)
        {
            var query = Query.EQ("_id", new ObjectId(id));

            var update = Update<TEntity>.Replace(entity);

            this.database
                .GetCollection<TEntity>(collectionName)
                .Update(query, update);
        }

        public bool Delete(int id)
        {
            return this.Delete(id.ToString());
        }

        public bool Delete(string id)
        {
            var query = Query.EQ("_id", new ObjectId(id));

            var result = this.database
                .GetCollection<TEntity>(collectionName)
                .Remove(query);

            return this.GetById(id) == null;
        }
    }
}
