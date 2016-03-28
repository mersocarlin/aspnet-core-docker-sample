using System.Collections.Generic;

namespace BankService.Domain.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> GetWithLimit(int limit);
        TEntity GetById(int id);
        TEntity GetById(string id);
        void Create(TEntity entity);
        void Update(TEntity entity, int id);
        void Update(TEntity entity, string id);
        bool Delete(int id);
        bool Delete(string id);
    }
}
