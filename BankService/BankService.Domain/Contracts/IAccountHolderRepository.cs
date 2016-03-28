using BankService.Domain.Models;
using MongoDB.Bson;
using System.Collections.Generic;

namespace BankService.Domain.Contracts
{
    public interface IAccountHolderRepository
    {
        IEnumerable<AccountHolder> GetAll();

        AccountHolder GetById(ObjectId id);

        void Add(AccountHolder accountHolder);

        void Update(AccountHolder accountHolder);

        bool Remove(ObjectId id);
    }
}
